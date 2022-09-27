using JetBrains.Annotations;
using MediatR;

namespace Journal.Infrastructure.Features.HealthCheck;

[UsedImplicitly]
public class HealthCheckMediator
{
    public class HealthCheckQuery : IRequest<bool> { }

    [UsedImplicitly]
    public class HealthCheckHandler : IRequestHandler<HealthCheckQuery, bool>
    {
        private readonly IHealthCheckRepository _repo;

        public HealthCheckHandler(IHealthCheckRepository repo) => _repo = repo;

        public Task<bool> Handle(HealthCheckQuery request, CancellationToken cancellationToken) => Task.Run(() =>
        {
            bool isDbAlive = _repo.IsDatabaseAlive();

            return isDbAlive;
        });
    }
}