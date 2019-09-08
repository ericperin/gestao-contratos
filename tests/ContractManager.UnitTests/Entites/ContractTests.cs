using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ContractManager.UnitTests.Entites
{
    public class ContractTests
    {
        private readonly Contract _contract = new Contract(
            "Eric Perin", Domain.Enums.ETypeOfContract.Compra, 1, 2, new DateTime(2000, 1, 1), 12, null, Guid.NewGuid());

        [Fact]
        public async Task ShouldCreateContract()
        {
            //Arange
            var mockRepository = new Mock<IContractRepository>();

            //Act
            mockRepository.Setup(x => x.Create(_contract));
            await mockRepository.Object.Create(_contract);

            //Asert
            mockRepository.Verify(x => x.Create(_contract), Times.Once());
        }

        [Fact]
        public async Task ShouldEditContract()
        {
            //Arange
            var mockRepository = new Mock<IContractRepository>();

            //Act
            mockRepository.Setup(x => x.Edit(_contract));
            await mockRepository.Object.Edit(_contract);

            //Asert
            mockRepository.Verify(x => x.Edit(_contract), Times.Once());
        }

        [Fact]
        public void ShouldReturnContractById()
        {
            //Arange
            var mockRepository = new Mock<IContractRepository>();

            //Act
            mockRepository.Setup(x => x.GetById(_contract.Id)).ReturnsAsync(_contract);
            mockRepository.Object.GetById(_contract.Id);

            //Asert
            mockRepository.Verify(x => x.GetById(_contract.Id), Times.Once());
        }

        [Fact]
        public void ShouldDeleteContractById()
        {
            //Arange
            var mockRepository = new Mock<IContractRepository>();

            //Act
            mockRepository.Setup(x => x.Delete(_contract.Id));
            mockRepository.Object.Delete(_contract.Id);

            //Asert
            mockRepository.Verify(x => x.Delete(_contract.Id), Times.Once);
        }
    }
}
