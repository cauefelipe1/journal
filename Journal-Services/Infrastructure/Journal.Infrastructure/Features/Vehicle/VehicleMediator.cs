using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using MediatR;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public abstract partial class VehicleMediator
{

    private static VehicleModel BuildModel(VehicleDTO dto)
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

    public class GetVehicleByIdQuery : IRequest<VehicleModel?>
    {
        public long? VehicleId { get; }

        public Guid? VehicleSecondaryId { get; }

        public GetVehicleByIdQuery(long vehicleId) => VehicleId = vehicleId;

        public GetVehicleByIdQuery(Guid vehicleSecondaryId) => VehicleSecondaryId = vehicleSecondaryId;
    }

    [UsedImplicitly]
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, VehicleModel?>
    {
        private readonly IVehicleRepository _repo;

        public GetVehicleByIdHandler(IVehicleRepository repository) => _repo = repository;

        public Task<VehicleModel?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            VehicleModel? result = null;

            VehicleDTO? dto;

            if (request.VehicleId.HasValue)
                dto = _repo.GetVehicleById(request.VehicleId.Value);

            else if (request.VehicleSecondaryId.HasValue)
                dto = _repo.GetVehicleBySecondaryId(request.VehicleSecondaryId.Value);

            else
                throw new ArgumentException("A valid Vehicle Id or Secondary Id must be informed.");

            if (dto is not null)
                result = BuildModel(dto);

            return result;
        }, cancellationToken);
    }

    public class GetVehicleByMainDriverQuery : IRequest<IList<VehicleModel>>
    {
        public int? MainDriverId { get; }

        public Guid? MainDriverSecondaryId { get; }

        public GetVehicleByMainDriverQuery(int mainDriverId) => MainDriverId = mainDriverId;

        public GetVehicleByMainDriverQuery(Guid mainDriverId) => MainDriverSecondaryId = mainDriverId;
    }

    [UsedImplicitly]
    public class GetVehicleByMainDriverHandler : IRequestHandler<GetVehicleByMainDriverQuery, IList<VehicleModel>>
    {
        private readonly IVehicleRepository _repo;
        private readonly ISender _sender;

        public GetVehicleByMainDriverHandler(IVehicleRepository repository, ISender sender)
        {
            _repo = repository;
            _sender = sender;
        }

        public Task<IList<VehicleModel>> Handle(GetVehicleByMainDriverQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            IList<VehicleDTO> vehicleDTOs;

            if (request.MainDriverId.HasValue)
                vehicleDTOs = _repo.GetVehicleByMainDriverId(request.MainDriverId.Value);

            else if (request.MainDriverSecondaryId.HasValue)
                vehicleDTOs = _repo.GetVehicleByMainDriverSecondaryId(request.MainDriverSecondaryId.Value);

            else
                throw new ArgumentException("A valid main Driver Id or Secondary Id must be informed.");

            IList<VehicleModel> vehicles = vehicleDTOs.Select(dto => BuildModel(dto)).ToList();

            return vehicles;
        }, cancellationToken);
    }
}