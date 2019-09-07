using ContractManager.Data.Interfaces;
using ContractManager.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();
        }
    }
}
