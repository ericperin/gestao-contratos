using ContractManager.Data.Repositories;
using ContractManager.Domain.Interfaces.Repositories;
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
