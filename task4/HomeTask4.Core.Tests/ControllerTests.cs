using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using HomeTask4.SharedKernel.Interfaces;
using HomeTask4.SharedKernel;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Controllers;
using System.Linq;

namespace HomeTask4.Core.Tests
{
    public class ControllerTests
    {
        List<Recipe> recipes = new List<Recipe>
                {
                    new Recipe { Id = 1, Name = "Recipe 1"},
                    new Recipe { Id = 3, Name = "Recipe 2"},
                    new Recipe { Id = 5, Name = "Recipe 3"}
                };
        [Fact]
        public async void GetAllItemsAsync_ShouldReturnListOfEntities()
        {
            //Arrange
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(repository => repository.GetItemsAsync<Recipe>()).ReturnsAsync(recipes);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Repository).Returns(repositoryMock.Object);

            var controller = new Controller(unitOfWorkMock.Object);

            int expected = 3;

            //Act
            var list = await controller.GetAllItemsAsync<Recipe>();

            //Assert
            Assert.Equal(expected, list.ToList().Count);

        }
        [Fact]
        public async void AddItems_ShouldAddNewItemToTheList()
        {
            //Arrange
            var repositoryMock = new Mock<IRepository>();
            //repositoryMock.Setup(repository => repository.GetItemsAsync<Recipe>()).ReturnsAsync(recipes);

            Recipe newRecipe = new Recipe { Id = 7, Name = "Recipe 4"};
            repositoryMock.Setup(repository => repository.AddItemAsync<Recipe>(newRecipe)).ReturnsAsync((Recipe recipe) => recipe);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Repository).Returns(repositoryMock.Object);

            var controller = new Controller(unitOfWorkMock.Object);

            Recipe expected = newRecipe;

            //Act
            var item = await controller.AddItem<Recipe>(newRecipe);

            //Assert
            Assert.Equal(expected.Name, item.Name);

        }
        [Fact]
        public async void GetItemById_ShouldReturnItemByIdNumber()
        {
            //Arrange
            var repositoryMock = new Mock<IRepository>();

            repositoryMock.Setup(repository => repository.GetItemsAsync<Recipe>()).ReturnsAsync(recipes);
            Recipe recipe = recipes[2];
            repositoryMock.Setup(repository => repository.GetByIdAsync<Recipe>(3)).ReturnsAsync(recipes.Find(recipe => recipe.Id == 3));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Repository).Returns(repositoryMock.Object);

            var controller = new Controller(unitOfWorkMock.Object);

            int expected = 3;

            //Act
            var item = await controller.GetItemById<Recipe>(3);

            //Assert
            Assert.Equal(expected, item.Id);

        }
        [Fact]
        public async void SaveAsync_ShouldWork()
        {
            //Arrange
            var repositoryMock = new Mock<IRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Repository).Returns(repositoryMock.Object);

            var controller = new Controller(unitOfWorkMock.Object);

            //Act
            await controller.SaveAsync();

            //Assert
            unitOfWorkMock.Verify(unitOfWork => unitOfWork.SaveChangesAsync());

        }


    }
}
