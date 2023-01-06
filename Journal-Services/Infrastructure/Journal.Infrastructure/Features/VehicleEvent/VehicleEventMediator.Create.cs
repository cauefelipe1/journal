using JetBrains.Annotations;
using Journal.Domain.Models.VehicleEvent;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Localization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Journal.Infrastructure.Features.VehicleEvent;

public abstract partial class VehicleEventMediator
{
    public class CreateVehicleEventQuery : IRequest<int>
    {
        public VehicleEventModel Model { get; }

        public CreateVehicleEventQuery(VehicleEventModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateVehicleEventHandler : IRequestHandler<CreateVehicleEventQuery, int>
    {
        private readonly IVehicleEventRepository _repo;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Translations> _l10n;

        public CreateVehicleEventHandler(
            IVehicleEventRepository repo,
            IMediator mediator,
            IStringLocalizer<Translations> l10n)
        {
            _repo = repo;
            _mediator = mediator;
            _l10n = l10n;
        }

        public async Task<int> Handle(CreateVehicleEventQuery request, CancellationToken cancellationToken)
        {

            await ValidateModel(request.Model);

            var dto = BuildDTO(request.Model);
            int id = _repo.InsertVehicleEvent(dto);

            return id;
        }

        private VehicleEventDTO BuildDTO(VehicleEventModel model)
        {
            var dto = new VehicleEventDTO
            {
                VehicleId = model.VehicleId,
                DriverId = model.DriverId,
                OwnerDriverId = model.OwnerDriverId,
                EventDate = model.Date,
                EventDescription = model.Description,
                EventNote = model.Note,
                VehicleEventTypeId = (int)model.Type,
                VehicleOdometer = model.VehicleOdometer
            };

            return dto;
        }

        private async Task ValidateModel(VehicleEventModel model)
        {
            var vehicle = await _mediator.Send(new VehicleMediator.GetVehicleByIdQuery(model.VehicleId));

            if (vehicle is null)
                throw new Exception("Vehicle not found.");

            if (vehicle.MainDriverId != model.OwnerDriverId)
                throw new Exception("Vehicle does not belong to the driver");

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