using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.Web.Extensions
{
    public static class MediatRSetup
    {
        public static void AddMediatRSetup(this IServiceCollection services) =>
            services.AddMediatR(Assembly.Load("ContractManager.Domain"));
    }
}
