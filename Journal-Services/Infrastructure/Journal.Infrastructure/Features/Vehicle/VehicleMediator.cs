using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public abstract partial class VehicleMediator
{

    public class GetVehicleByMainDriverQuery : IRequest<IList<VehicleModel>>
    {
        public int MainDriverId { get; }

        public GetVehicleByMainDriverQuery(int mainDriverId) => MainDriverId = mainDriverId;
    }

    [UsedImplicitly]
    public class GetVehicleByMainDriverHandler : IRequestHandler<GetVehicleByMainDriverQuery, IList<VehicleModel>>
    {
        private readonly IVehicleRepository _repo;

        public GetVehicleByMainDriverHandler(IVehicleRepository repository) => _repo = repository;

        public Task<IList<VehicleModel>> Handle(GetVehicleByMainDriverQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var vehicleDTOs = _repo.GetVehicleByMainDriverId(request.MainDriverId);

            var vehicles = vehicleDTOs.Adapt<IList<VehicleModel>>();

            return vehicles;
        }, cancellationToken);
    }

}