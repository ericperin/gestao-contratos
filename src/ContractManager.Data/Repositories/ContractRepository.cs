using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces.Repositories;

namespace ContractManager.Data.Repositories
{
    public class ContractRepository : RepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
