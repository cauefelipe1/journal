using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Localization;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Journal.Infrastructure.Features.VehicleBrand;

public abstract class  VehicleBrandMediator
{
    public class GetVehicleBrandByIdQuery : IRequest<VehicleBrandModel>
    {
        public int? BrandId { get; }

        public Guid? BrandSecondaryId { get; }

        public GetVehicleBrandByIdQuery(int brandId) => BrandId = brandId;

        public GetVehicleBrandByIdQuery(Guid brandSecondaryId) => BrandSecondaryId = brandSecondaryId;
    }

    [UsedImplicitly]
    public class GetVehicleEventByVehicleHandler : IRequestHandler<GetVehicleBrandByIdQuery, VehicleBrandModel>
    {
        private readonly IVehicleBrandRepository _repo;
        private readonly IStringLocalizer<Translations> _l10n;

        public GetVehicleEventByVehicleHandler(IVehicleBrandRepository repository, IStringLocalizer<Translations> l10n)
        {
            _repo = repository;
            _l10n = l10n;
        }

        public Task<VehicleBrandModel> Handle(GetVehicleBrandByIdQuery request, CancellationToken cancellationToken)
        {
            VehicleBrandDTO? dto;

            if (request.BrandId.HasValue)
                dto = _repo.GetVehicleBrandById(request.BrandId.Value);

            else if (request.BrandSecondaryId.HasValue)
                dto = _repo.GetVehicleBrandBySecondaryId(request.BrandSecondaryId.Value);

            else
                throw new ArgumentException("A valid main Vehicle Id or Secondary Id must be informed.");


            if (dto is null)
            {
                string errorMessage = _l10n["VehicleBrandNotFound"];
                throw new Exception(errorMessage);
            }

            var brand = dto.Adapt<VehicleBrandModel>();

            return Task.FromResult(brand);
        }
    }

    public class AllVehicleBrandQuery : IRequest<IList<VehicleBrandModel>> { }

    [UsedImplicitly]
    public class AllVehicleBrandHandler : IRequestHandler<AllVehicleBrandQuery, IList<VehicleBrandModel>>
    {
        private readonly IVehicleBrandRepository _repo;

        public AllVehicleBrandHandler(IVehicleBrandRepository repository) => _repo = repository;

        public Task<IList<VehicleBrandModel>> Handle(AllVehicleBrandQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var brandsDTOs = _repo.GetAllBrands();

            var brands = brandsDTOs.Adapt<IList<VehicleBrandModel>>();

            return brands;
        }, cancellationToken);
    }
}