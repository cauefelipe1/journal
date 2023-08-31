using JetBrains.Annotations;
using Journal.Domain.Base;
using Journal.Domain.Models.Driver;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class CreateDriverCommand : IRequest<ModelDoublePK>
    {
        public DriverModel Model { get; }

        public CreateDriverCommand(DriverModel model) => Model = model;
    }

    [UsedImplicitly]
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, ModelDoublePK>
    {
        private readonly IDriverRepository _repo;

        public CreateDriverCommandHandler(IDriverRepository repo) => _repo = repo;

        public Task<ModelDoublePK> Handle(CreateDriverCommand request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            var dto = BuildDTO(request.Model);
            dto.SecondaryId = Guid.NewGuid();

            var result = _repo.InsertDriver(dto);

            return result;
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