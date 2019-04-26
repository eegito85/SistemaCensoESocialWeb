using Microsoft.Extensions.DependencyInjection;
using Mpce.CensoEsocial.Data.Repositories;
using Mpce.ECensoSocial.Domain.Domain.Interfaces.Repositories;

namespace Mpce.CensoEsocial.EoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITrabalhadorRepository, TrabalhadorRepository>();
        }
    }
}