using JetBrains.Annotations;
using Journal.Domain.Models.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    private static DriverModel BuildModel(DriverDTO dto)
    {
        return new()
        {
            DriverId = dto.DriverId,
            SecondaryId = dto.SecondaryId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CountryId = dto.CountryId,
            FullName = $"{dto.FirstName} {dto.LastName}",
            UserId = dto.UserId
        };
    }

    public class GetDriverByIdQuery : IRequest<DriverModel?>
    {
        public Guid? SecondaryId { get; }
        public int? Id { get; }

        public GetDriverByIdQuery(Guid secondaryId) => SecondaryId = secondaryId;
        public GetDriverByIdQuery(int id) => Id = id;
    }

    [UsedImplicitly]
    public class GetDriverByIdHandler : IRequestHandler<GetDriverByIdQuery, DriverModel?>
    {
        private readonly IDriverRepository _repo;

        public GetDriverByIdHandler(IDriverRepository repo) => _repo = repo;

        public Task<DriverModel?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            DriverModel? result = null;

            DriverDTO? dto;

            if (request.Id.HasValue)
                dto = _repo.GetDriverById(request.Id.Value);

            else if (request.SecondaryId.HasValue)
                dto = _repo.GetDriverBySecondaryId(request.SecondaryId.Value);
            else
                throw new ArgumentException("A valid Driver Id or Secondary Id must be informed.");

            if (dto is not null)
                result = BuildModel(dto);

            return result;
        }, cancellationToken);
    }
}