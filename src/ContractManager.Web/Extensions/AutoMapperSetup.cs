using System.Reflection;
using AutoMapper;
using ContractManager.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.Web.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            var mapper = AutoMapperConfig.ConfigureMappings();
            services.AddAutoMapper(x => mapper.CreateMapper(), Assembly.Load("ContractManager.Application"));
        }
    }
}
