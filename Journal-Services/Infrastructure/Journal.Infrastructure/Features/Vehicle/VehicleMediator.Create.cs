using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

public abstract partial class VehicleMediator
{
    public class CreateVehicleQuery : IRequest<long>
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
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleQuery, long>
    {
        private readonly IVehicleRepository _repo;
        private readonly ISender _sender;

        public CreateVehicleHandler(IVehicleRepository repository, ISender sender)
        {
            _repo = repository;
            _sender = sender;
        }

        public async Task<long> Handle(CreateVehicleQuery request, CancellationToken cancellationToken)
        {
            var driver = await _sender.Send(new DriverMediator.GetDriverByIdQuery(request.MainDriverSecondaryId), cancellationToken);

            if (driver is null)
                throw new Exception("Driver not found");

            var dto = BuildDTO(request.Model);
            dto.SecondaryId = Guid.NewGuid();

            dto.MainDriverId = driver.DriverId;

            long id = _repo.InsertVehicle(dto);

            return id;
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