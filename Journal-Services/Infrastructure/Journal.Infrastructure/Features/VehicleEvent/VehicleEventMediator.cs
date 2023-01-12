using JetBrains.Annotations;
using Journal.Domain.Models.VehicleEvent;
using MediatR;

namespace Journal.Infrastructure.Features.VehicleEvent;

[UsedImplicitly]
public abstract partial class VehicleEventMediator
{

    public class GetVehicleEventByVehicleQuery : IRequest<IList<VehicleEventModel>>
    {
        public int VehicleId { get; }

        public GetVehicleEventByVehicleQuery(int vehicleId) => VehicleId = vehicleId;
    }

    [UsedImplicitly]
    public class GetVehicleEventByVehicleHandler : IRequestHandler<GetVehicleEventByVehicleQuery, IList<VehicleEventModel>>
    {
        private readonly IVehicleEventRepository _repo;

        public GetVehicleEventByVehicleHandler(IVehicleEventRepository repository) => _repo = repository;

        public Task<IList<VehicleEventModel>> Handle(GetVehicleEventByVehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicleDTOs = _repo.GetVehicleEventsByVehicleId(request.VehicleId);

            IList<VehicleEventModel> vehicles = vehicleDTOs.Select(dto => BuildModel(dto)).ToList();

            return Task.FromResult(vehicles);
        }

        private VehicleEventModel BuildModel(VehicleEventDTO dto)
        {
            var model = new VehicleEventModel
            {
                Id = dto.VehicleEventId,
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