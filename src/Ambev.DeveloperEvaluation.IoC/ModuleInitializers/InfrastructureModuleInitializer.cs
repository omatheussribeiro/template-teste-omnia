using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.ORM; // AppDbContext
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
            )
        );

        builder.Services.AddHostedService<DbMigrationHostedService>();

        builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();

        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddSingleton<IDiscountService, DiscountService>();

        //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleHandler).Assembly));
        //builder.Services.AddAutoMapper(typeof(Ambev.DeveloperEvaluation.Application.Sales.Mapping.SaleProfiles).Assembly);
        //builder.Services.AddValidatorsFromAssembly(typeof(Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleValidator).Assembly);
    }
}
