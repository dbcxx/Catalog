using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repo;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests
{
    public class ItemsControllerTests
    {
        private readonly Mock<IItemsRepository> repositoryStub = new();
        private readonly Mock<ILogger<ItemsController>> loggerStub = new();
        private readonly Random rand = new();

        [Fact]

        //UnitOfWork_StateUnderTest_ExpectedBehaviour
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange 
            
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);
            
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act 
            var result = await controller.GetItemAsync(Guid.NewGuid());

            // Assert
            //Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]

        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange 
            var expectemItem = CreateRandomItem();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync(expectemItem);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act 
            var result = await controller.GetItemAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectemItem, opt=>opt.ComparingByMembers<Item>()); 
        }

        [Fact]

        public async Task GetItemsAsync_WithExistingItems_ReturnAllItems()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            repositoryStub.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(expectedItems);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualItems = await controller.GetItemsAsync(); 

            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems, opt=>opt.ComparingByMembers<Item>());
        }

        private Item CreateRandomItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
