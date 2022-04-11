using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repo;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests
{
    public class ItemsControllerTests
    {
        [Fact]

        //UnitOfWork_StateUnderTest_ExpectedBehaviour
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange 
            var repositoryStub = new Mock<IItemsRepository>();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);
            var loggerStub = new Mock<ILogger<ItemsController>>();
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);
            // Act 

            // Assert
        }
    }
}
