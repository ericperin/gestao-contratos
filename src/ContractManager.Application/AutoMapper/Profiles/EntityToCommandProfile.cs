using AutoMapper;
using ContractManager.Domain.Commands.Contract;
using ContractManager.Domain.Entities;

namespace ContractManager.Application.AutoMapper.Profiles
{
    public class EntityToCommandProfile : Profile
    {
        public EntityToCommandProfile()
        {
            CreateMap<Contract, CreateContractCommand>()
                .ReverseMap();

            CreateMap<Contract, UpdateContractCommand>()
                .ReverseMap();
        }
    }
}
