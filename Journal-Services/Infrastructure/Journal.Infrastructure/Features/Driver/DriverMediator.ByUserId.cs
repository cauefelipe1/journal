using JetBrains.Annotations;
using Journal.Domain.Models.Driver;
using Journal.Identity.Features.User;
using MediatR;

namespace Journal.Infrastructure.Features.Driver;

public abstract partial class DriverMediator
{
    public class GetDriverByUserIdQuery : IRequest<DriverModel?>
    {
        public Guid? Id { get; }
        public uint? SecondaryId { get; }

        public GetDriverByUserIdQuery(Guid id) => Id = id;
        public GetDriverByUserIdQuery(uint secondaryId) => SecondaryId = secondaryId;
    }

    [UsedImplicitly]
    public class GetDriverByUserIdQueryHandler : IRequestHandler<GetDriverByUserIdQuery, DriverModel?>
    {
        private readonly IDriverRepository _repo;
        private readonly ISender _sender;

        public GetDriverByUserIdQueryHandler(IDriverRepository repo, ISender sender)
        {
            _repo = repo;
            _sender = sender;
        }

        public async Task<DriverModel?> Handle(GetDriverByUserIdQuery request, CancellationToken cancellationToken)
        {
            DriverModel? result = null;

            uint userId = 0;

            if (request.Id.HasValue)
            {
                var userModel = await _sender.Send(new UserMediator.GetUserDataQuery(request.Id.Value), cancellationToken);

                if (userModel is not null)
                    userId = userModel.SecondaryId;
            }
            else if (request.SecondaryId.HasValue)
                userId = request.SecondaryId.Value;

            else
                throw new ArgumentException("A valid User Id or Secondary Id must be informed.");

            var dto = _repo.GetDriverByUserId(userId);

            if (dto is not null)
                result = BuildModel(dto);

            return result;
        }
    }
}