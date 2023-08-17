using JetBrains.Annotations;
using Journal.Domain.Models.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class CreateDriverByIdQuery : IRequest<int>
    {
        public DriverModel Model { get; }

        public CreateDriverByIdQuery(DriverModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateDriverByIdQueryHandler : IRequestHandler<CreateDriverByIdQuery, int>
    {
        private IDriverRepository _repo;

        public CreateDriverByIdQueryHandler(IDriverRepository repo) => _repo = repo;

        public Task<int> Handle(CreateDriverByIdQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var dto = BuildDTO(request.Model);

            int id = _repo.InsertDriver(dto);

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
                UserId = model.UserId
            };
        }
    }
}