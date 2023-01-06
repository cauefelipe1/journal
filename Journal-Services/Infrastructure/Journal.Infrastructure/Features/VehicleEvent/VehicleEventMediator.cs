using JetBrains.Annotations;
using Journal.Domain.Models.VehicleEvent;
using MediatR;

namespace Journal.Infrastructure.Features.VehicleEvent;

[UsedImplicitly]
public abstract partial class VehicleEventMediator
{

    public class GetVehicleEventByEventQuery : IRequest<IList<VehicleEventModel>>
    {
        public int VehicleId { get; }

        public GetVehicleEventByEventQuery(int vehicleId) => VehicleId = vehicleId;
    }

    [UsedImplicitly]
    public class GetVehicleByMainDriverHandler : IRequestHandler<GetVehicleEventByEventQuery, IList<VehicleEventModel>>
    {
        private readonly IVehicleEventRepository _repo;

        public GetVehicleByMainDriverHandler(IVehicleEventRepository repository) => _repo = repository;

        public Task<IList<VehicleEventModel>> Handle(GetVehicleEventByEventQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var vehicleDTOs = _repo.GetVehicleEventsByVehicleId(request.VehicleId);

            IList<VehicleEventModel> vehicles = vehicleDTOs.Select(dto => BuildModel(dto)).ToList();

            return vehicles;
        }, cancellationToken);

        private VehicleEventModel BuildModel(VehicleEventDTO dto)
        {
            var model = new VehicleEventModel
            {
                Id = dto.VehicleId,
                OwnerDriverId = dto.OwnerDriverId,
                DriverId = dto.DriverId,
                VehicleId = dto.VehicleId,
                Type = (VehicleEventType)dto.VehicleEventTypeId,
                VehicleOdometer = dto.VehicleOdometer,
                Date = dto.EventDate,
                Description = dto.EventDescription,
                Note = dto.EventNote
            };

            return model;
        }
    }

}