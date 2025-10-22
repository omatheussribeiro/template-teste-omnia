using Ambev.DeveloperEvaluation.Common.Security; // AddJwtAuthentication
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class WebApiModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHealthChecks();

            //// ✅ JWT agora centralizado no IoC (tira do Program.cs)
            //builder.Services.AddJwtAuthentication(builder.Configuration);
        }
    }
}
