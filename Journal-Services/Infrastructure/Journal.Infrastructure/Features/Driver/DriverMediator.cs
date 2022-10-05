using JetBrains.Annotations;
using Journal.Domain.Models.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class GetDriverByIdQuery : IRequest<DriverModel?>
    {
        public int DriverId { get; }

        public GetDriverByIdQuery(int driverId) => DriverId = driverId;
    }

    [UsedImplicitly]
    public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdQuery, DriverModel?>
    {
        private IDriverRepository _repo;

        public GetDriverByIdHandler(IDriverRepository repo) => _repo = repo;

        public Task<DriverModel?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            DriverModel? result = null;

            var dto = _repo.GetDriverById(request.DriverId);

            if (dto is not null)
                result = BuildModel(dto);

            return result;
        }, cancellationToken);

        private DriverModel BuildModel(DriverDTO dto)
        {
            return new()
            {
                DriverId = dto.DriverId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                FullName = $"{dto.FirstName} {dto.LastName}",
                UserId = dto.UserId
            };
        }
    }
}