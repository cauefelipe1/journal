using JetBrains.Annotations;
using Journal.Domain.Base;
using Journal.Domain.Models.Driver;
using Journal.Identity.Features.User;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class CreateDriverCommand : IRequest<ModelDoublePK>
    {
        public DriverModel Model { get; }

        public Guid UserId { get; set; }

        public CreateDriverCommand(DriverModel model, Guid userId)
        {
            Model = model;
            UserId = userId;
        }
    }

    [UsedImplicitly]
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, ModelDoublePK>
    {
        private readonly IDriverRepository _repo;
        private readonly ISender _sender;

        public CreateDriverCommandHandler(IDriverRepository repo, ISender sender)
        {
            _repo = repo;
            _sender = sender;
        }

        public async Task<ModelDoublePK> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var userData = await _sender.Send(new UserMediator.GetUserDataQuery(request.UserId));

            var dto = BuildDTO(request.Model);

            dto.SecondaryId = Guid.NewGuid();
            dto.UserId = userData.SecondaryId;

            var result = _repo.InsertDriver(dto);

            return result;
        }

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