using Journal.Domain.Models.Vehicle;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

public partial class VehicleMediator
{
    public class AllVehicleBrandQuery : IRequest<IList<VehicleBrand>> { }

    public class AllVehicleBrandHandler : IRequestHandler<AllVehicleBrandQuery, IList<VehicleBrand>>
    {
        private readonly IVehicleRepository _repo;

        public AllVehicleBrandHandler(IVehicleRepository repository) => _repo = repository;

        public Task<IList<VehicleBrand>> Handle(AllVehicleBrandQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var brands = _repo.GetAllBrands();

            return brands;
        }, cancellationToken);
    }
}