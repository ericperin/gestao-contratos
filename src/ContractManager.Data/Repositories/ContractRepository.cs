using ContractManager.Data.Interfaces;
using ContractManager.Domain.Entities;

namespace ContractManager.Data.Repositories
{
    public class ContractRepository : RepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
