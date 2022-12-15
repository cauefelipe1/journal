using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

public abstract partial class VehicleMediator
{
    public class CreateVehicleQuery : IRequest<int>
    {
        public VehicleModel Model { get; }

        public CreateVehicleQuery(VehicleModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleQuery, int>
    {
        private readonly IVehicleRepository _repo;

        public CreateVehicleHandler(IVehicleRepository repository) => _repo = repository;

        public Task<int> Handle(CreateVehicleQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {

            var dto = BuildDTO(request.Model);

            int id = _repo.InsertVehicle(dto);

            return id;

        }, cancellationToken);

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