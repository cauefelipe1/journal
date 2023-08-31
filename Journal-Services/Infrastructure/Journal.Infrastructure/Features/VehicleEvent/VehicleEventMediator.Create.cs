using JetBrains.Annotations;
using Journal.Domain.Base;
using Journal.Domain.Models.Driver;
using Journal.Domain.Models.Vehicle;
using Journal.Domain.Models.VehicleEvent;
using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Localization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Journal.Infrastructure.Features.VehicleEvent;

public abstract partial class VehicleEventMediator
{

    public class CreateVehicleEventQuery : IRequest<ModelDoublePK>
    {
        public VehicleEventModel Model { get; }

        public CreateVehicleEventQuery(VehicleEventModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateVehicleEventHandler : IRequestHandler<CreateVehicleEventQuery, ModelDoublePK>
    {
        private readonly record struct ModelsDependenciesResult(VehicleModel Vehicle, DriverModel Driver, DriverModel OwnerDriver);

        private readonly IVehicleEventRepository _repo;
        private readonly ISender _sender;
        private readonly IStringLocalizer<Translations> _l10n;

        public CreateVehicleEventHandler(
            IVehicleEventRepository repo,
            ISender sender,
            IStringLocalizer<Translations> l10n)
        {
            _repo = repo;
            _sender = sender;
            _l10n = l10n;
        }

        public async Task<ModelDoublePK> Handle(CreateVehicleEventQuery request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var dependenciesModels = await InternalGetModelsDependencies(model);

            PopulateIds(model, dependenciesModels);

            ValidateModel(model, dependenciesModels.Vehicle);

            var dto = BuildDTO(request.Model);
            dto.SecondaryId = Guid.NewGuid();

            var ids = _repo.InsertVehicleEvent(dto);

            return ids;
        }

        private void PopulateIds(VehicleEventModel model, ModelsDependenciesResult dependencies)
        {
            model.VehicleId = dependencies.Vehicle.Id;
            model.DriverId = dependencies.Driver.DriverId;
            model.OwnerDriverId = dependencies.OwnerDriver.DriverId;
        }

        private async Task<ModelsDependenciesResult> InternalGetModelsDependencies(VehicleEventModel eventModel)
        {
            var vehicleTask = _sender.Send(new VehicleMediator.GetVehicleByIdQuery(eventModel.VehicleSecondaryId));
            var driverTask = _sender.Send(new DriverMediator.GetDriverByIdQuery(eventModel.DriverSecondaryId));

            //TODO: After refactoring the GetDriverById and GetDriverBySecondaryId to use Dapper rather than EF Core,
            // add this line back and fetch all dependencies at once.
            //var ownerDriverTask = _mediator.Send(new DriverMediator.GetDriverByIdQuery(eventModel.OwnerDriverSecondaryId));

            await Task.WhenAll(vehicleTask, driverTask);

            var vehicle = vehicleTask.Result;
            var driver = driverTask.Result;
            var ownerDriver = await _sender.Send(new DriverMediator.GetDriverByIdQuery(eventModel.OwnerDriverSecondaryId));

            if (vehicle is null)
                throw new Exception("Vehicle not found.");

            if (driver is null)
                throw new Exception("Driver not found.");

            if (ownerDriver is null)
                throw new Exception("Owner not found.");

            return new ModelsDependenciesResult(vehicle, driver, ownerDriver);
        }

        private VehicleEventDTO BuildDTO(VehicleEventModel model)
        {
            var dto = new VehicleEventDTO
            {
                VehicleId = model.VehicleId,
                VehicleSecondaryId = model.VehicleSecondaryId,
                DriverId = model.DriverId,
                DriverSecondaryId = model.SecondaryId,
                OwnerDriverId = model.OwnerDriverId,
                OwnerDriverSecondaryId = model.SecondaryId,
                EventDate = model.Date,
                EventDescription = model.Description,
                EventNote = model.Note,
                VehicleEventTypeId = (int)model.Type,
                VehicleOdometer = model.VehicleOdometer
            };

            return dto;
        }

        private void ValidateModel(VehicleEventModel model, VehicleModel vehicle)
        {
            if (vehicle.MainDriverId != model.OwnerDriverId)
                throw new Exception("Vehicle does not belong to the driver");

            model.Date = model.Date.ToUniversalTime();

            ValidateOdometerSequence(model);
        }

        private void ValidateOdometerSequence(VehicleEventModel model)
        {
            var events = _repo.GetVehicleEventsByVehicleIdWithDate(model.VehicleId, model.Date);

            if (events.Count <= 0)
                return;

            var first = events.FirstOrDefault();

            if (model.VehicleOdometer < first!.VehicleOdometer)
                throw new Exception(_l10n["VehicleOdometerBehindPreviousRecord"]);
        }
    }
}