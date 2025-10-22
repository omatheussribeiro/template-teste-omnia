using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM
{
    public sealed class DbMigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<DbMigrationHostedService> _logger;

        public DbMigrationHostedService(IServiceProvider provider, ILogger<DbMigrationHostedService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _logger.LogInformation("Applying EF Core migrations...");
            await db.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("EF Core migrations applied.");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
