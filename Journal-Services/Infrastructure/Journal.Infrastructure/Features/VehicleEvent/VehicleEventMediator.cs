using JetBrains.Annotations;
using Journal.Domain.Models.VehicleEvent;
using MediatR;

namespace Journal.Infrastructure.Features.VehicleEvent;

[UsedImplicitly]
public abstract partial class VehicleEventMediator
{
    public class GetVehicleEventByVehicleQuery : IRequest<IList<VehicleEventModel>>
    {
        public int? VehicleId { get; }

        public Guid? VehicleSecondaryId { get; }

        public GetVehicleEventByVehicleQuery(int vehicleId) => VehicleId = vehicleId;

        public GetVehicleEventByVehicleQuery(Guid vehicleSecondaryId) => VehicleSecondaryId = vehicleSecondaryId;
    }

    [UsedImplicitly]
    public class GetVehicleEventByVehicleHandler : IRequestHandler<GetVehicleEventByVehicleQuery, IList<VehicleEventModel>>
    {
        private readonly IVehicleEventRepository _repo;

        public GetVehicleEventByVehicleHandler(IVehicleEventRepository repository) => _repo = repository;

        public Task<IList<VehicleEventModel>> Handle(GetVehicleEventByVehicleQuery request, CancellationToken cancellationToken)
        {
            IList<VehicleEventDTO> vehicleDTOs;

            if (request.VehicleId.HasValue)
                vehicleDTOs = _repo.GetVehicleEventsByVehicleId(request.VehicleId.Value);

            else if (request.VehicleSecondaryId.HasValue)
                vehicleDTOs = _repo.GetVehicleEventsByVehicleSecondaryId(request.VehicleSecondaryId.Value);

            else
                throw new ArgumentException("A valid main Vehicle Id or Secondary Id must be informed.");

            IList<VehicleEventModel> vehicles = vehicleDTOs.Select(dto => BuildModel(dto)).ToList();

            return Task.FromResult(vehicles);
        }

        private VehicleEventModel BuildModel(VehicleEventDTO dto)
        {
            var model = new VehicleEventModel
            {
                Id = dto.VehicleEventId,
                SecondaryId = dto.SecondaryId,
                OwnerDriverId = dto.OwnerDriverId,
                OwnerDriverSecondaryId = dto.OwnerDriverSecondaryId.Value,
                DriverId = dto.DriverId,
                DriverSecondaryId = dto.DriverSecondaryId.Value,
                VehicleId = dto.VehicleId,
                VehicleSecondaryId = dto.VehicleSecondaryId.Value,
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