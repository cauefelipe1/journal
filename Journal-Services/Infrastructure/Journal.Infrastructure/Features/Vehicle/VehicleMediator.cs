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

            IList<VehicleModel> vehicles = vehicleDTOs.Select(dto => BuildModel(dto)).ToList();

            return vehicles;
        }, cancellationToken);

        private VehicleModel BuildModel(VehicleDTO dto)
        {
            var model = new VehicleModel
            {
                Id = dto.VehicleId,
                SecondaryId = dto.SecondaryId,
                BrandId = dto.VehicleBrandId,
                MainDriverId = dto.MainDriverId,
                ModelName = dto.Model,
                ModelYear = dto.ModelYear,
                Nickname = dto.Nickname ?? string.Empty,
                Type = (VehicleType)dto.VehicleTypeId, //TODO: change to appropriated conversion
                DisplayName = string.IsNullOrEmpty(dto.Nickname) ? dto.Model : dto.Nickname
            };

            return model;
        }
    }
}