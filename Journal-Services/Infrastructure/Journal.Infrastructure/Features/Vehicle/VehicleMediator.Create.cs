using JetBrains.Annotations;
using Journal.Domain.Base;
using Journal.Domain.Models.Driver;
using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.VehicleBrand;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

public abstract partial class VehicleMediator
{
    public class CreateVehicleQuery : IRequest<ModelDoublePK>
    {
        public VehicleModel Model { get; }

        public Guid MainDriverSecondaryId { get; }

        public CreateVehicleQuery(VehicleModel model, Guid mainDriverSecondaryId)
        {
            Model = model;
            MainDriverSecondaryId = mainDriverSecondaryId;
        }
    }

    [UsedImplicitly]
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleQuery, ModelDoublePK>
    {
        private readonly IVehicleRepository _repo;
        private readonly ISender _sender;

        public CreateVehicleHandler(IVehicleRepository repository, ISender sender)
        {
            _repo = repository;
            _sender = sender;
        }

        public async Task<ModelDoublePK> Handle(CreateVehicleQuery request, CancellationToken cancellationToken)
        {
            var (driver, brand) = await InternalGetModelsDependencies(request.Model);

            var dto = BuildDTO(request.Model);
            dto.SecondaryId = Guid.NewGuid();

            dto.MainDriverId = driver.DriverId;
            dto.VehicleBrandId = brand.Id;

            var doublePk = _repo.InsertVehicle(dto);

            return doublePk;
        }

        private async Task<(DriverModel Driver, VehicleBrandModel Brand)> InternalGetModelsDependencies(VehicleModel vehicleModel)
        {
            var vehicleTask = _sender.Send(new VehicleBrandMediator.GetVehicleBrandByIdQuery(vehicleModel.BrandSecondaryId.Value));
            var driverTask = _sender.Send(new DriverMediator.GetDriverByIdQuery(vehicleModel.MainDriverSecondaryId.Value));

            await Task.WhenAll(vehicleTask, driverTask);

            var brand = vehicleTask.Result;
            var driver = driverTask.Result;

            if (brand is null)
                throw new Exception("Vehicle not found.");

            if (driver is null)
                throw new Exception("Driver not found.");

            return new (driver, brand);
        }

        private VehicleDTO BuildDTO(VehicleModel model)
        {
            var dto = new VehicleDTO
            {
                VehicleId = model.Id,
                SecondaryId = model.SecondaryId,
                Model = model.ModelName,
                Nickname = model.Nickname,
                ModelYear = model.ModelYear,
                VehicleTypeId = (int)model.Type,
                VehicleBrandId = model.BrandId,
                MainDriverId = model.MainDriverId
            };

            return dto;
        }
    }
}