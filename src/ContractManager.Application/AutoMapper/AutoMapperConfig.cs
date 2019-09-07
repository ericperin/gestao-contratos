using AutoMapper;
using ContractManager.Application.AutoMapper.Profiles;

namespace ContractManager.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToCommandProfile());
            });
            return mapperConfiguration;
        }
    }
}
