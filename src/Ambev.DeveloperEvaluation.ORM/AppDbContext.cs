using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.ORM
{
    public class AppDbContext : DbContext
    {
        // DbSets
        public DbSet<User> Users => Set<User>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();

        private readonly IMediator? _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _mediator = null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            if (_mediator != null)
            {
                var entitiesWithEvents = ChangeTracker
                    .Entries<BaseEntity>()
                    .Where(e => e.Entity.DomainEvents.Any())
                    .Select(e => e.Entity)
                    .ToList();

                foreach (var entity in entitiesWithEvents)
                {
                    var events = entity.DomainEvents.ToList();
                    entity.ClearDomainEvents();

                    foreach (var domainEvent in events)
                    {
                        await _mediator.Publish(domainEvent, cancellationToken);
                    }
                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            if (_mediator != null)
            {
                var entitiesWithEvents = ChangeTracker
                    .Entries<BaseEntity>()
                    .Where(e => e.Entity.DomainEvents.Any())
                    .Select(e => e.Entity)
                    .ToList();

                foreach (var entity in entitiesWithEvents)
                {
                    var events = entity.DomainEvents.ToList();
                    entity.ClearDomainEvents();

                    foreach (var domainEvent in events)
                    {
                        _mediator.Publish(domainEvent).GetAwaiter().GetResult();
                    }
                }
            }

            return result;
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var possibleWebApiPath = Path.Combine(basePath, "src", "Ambev.DeveloperEvaluation.WebApi");

            var builderCfg = new ConfigurationBuilder()
                .SetBasePath(Directory.Exists(possibleWebApiPath) ? possibleWebApiPath : basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var configuration = builderCfg.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não configurada.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
