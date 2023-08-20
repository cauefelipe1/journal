using JetBrains.Annotations;
using Journal.Domain.Models.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class CreateDriverByIdQuery : IRequest<long>
    {
        public DriverModel Model { get; }

        public CreateDriverByIdQuery(DriverModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverByIdQuery, long>
    {
        private readonly IDriverRepository _repo;

        public CreateDriverCommandHandler(IDriverRepository repo) => _repo = repo;

        public Task<long> Handle(CreateDriverByIdQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var dto = BuildDTO(request.Model);
            dto.SecondaryId = Guid.NewGuid();

            long id = _repo.InsertDriver(dto);

            return id;
        }, cancellationToken);

        private DriverDTO BuildDTO(DriverModel model)
        {
            return new()
            {
                DriverId = model.DriverId,
                SecondaryId = model.SecondaryId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CountryId = model.CountryId,
                UserId = model.UserId
            };
        }
    }
}