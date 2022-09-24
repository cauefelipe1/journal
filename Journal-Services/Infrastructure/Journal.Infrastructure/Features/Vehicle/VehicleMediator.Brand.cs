using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

public partial class VehicleMediator
{
    public class AllVehicleBrandQuery : IRequest<IList<VehicleBrand>> { }

    [UsedImplicitly]
    public class AllVehicleBrandHandler : IRequestHandler<AllVehicleBrandQuery, IList<VehicleBrand>>
    {
        private readonly IVehicleRepository _repo;

        public AllVehicleBrandHandler(IVehicleRepository repository) => _repo = repository;

        public Task<IList<VehicleBrand>> Handle(AllVehicleBrandQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var brandsDTOs = _repo.GetAllBrands();

            var brands = brandsDTOs.Adapt<IList<VehicleBrand>>();

            return brands;
        }, cancellationToken);
    }
}