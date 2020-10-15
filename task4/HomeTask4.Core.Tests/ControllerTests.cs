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
        private Mock<IRepository> repositoryMock;

        private Mock<IUnitOfWork> unitOfWorkMock;
        private Controller controller;
        

        private IEnumerable<Recipe> recipes = new List<Recipe>
                {
                    new Recipe { Id = 1, Name = "Recipe 1"},
                    new Recipe { Id = 3, Name = "Recipe 2"},
                    new Recipe { Id = 5, Name = "Recipe 3"}
                };

        public ControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(repository => repository.GetItemsAsync<Recipe>()).ReturnsAsync(recipes);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.SetupGet(unitOfWork => unitOfWork.Repository).Returns(repositoryMock.Object);

            controller = new Controller(unitOfWorkMock.Object);
        }

        [Fact]
        public async void GetAllItemsAsync_ShouldReturnListOfEntities()
        {
            //Arrange
            

            //Act
            var list = await controller.GetAllItemsAsync<Recipe>();

            //Assert
            repositoryMock.Verify(repository => repository.GetItemsAsync<Recipe>());
            Assert.Same(recipes, list);

        }
        [Fact]
        public async void AddItems_ShouldAddNewItemToTheList()
        {
            //Arrange
            Recipe newRecipe = new Recipe { Id = 7, Name = "Recipe 4"};
            repositoryMock.Setup(repository => repository.AddItemAsync<Recipe>(newRecipe)).ReturnsAsync((Recipe recipe) => recipe);

            Recipe expectedRecipe = newRecipe;

            //Act
            var item = await controller.AddItem<Recipe>(newRecipe);

            //Assert
            repositoryMock.Verify(repository => repository.AddItemAsync<Recipe>(newRecipe));
            Assert.Equal(expectedRecipe.Name, item.Name);

        }
        [Fact]
        public async void GetItemById_ShouldReturnItemByIdNumber()
        {
            //Arrange
            Recipe recipe = recipes.Last();
            repositoryMock.Setup(repository => repository.GetByIdAsync<Recipe>(5)).ReturnsAsync(recipes.First(recipe => recipe.Id == 5));
            int expectedRecipeId = recipe.Id;

            //Act
            var item = await controller.GetItemById<Recipe>(recipe.Id);

            //Assert
            repositoryMock.Verify(repository => repository.GetByIdAsync<Recipe>(expectedRecipeId));
            Assert.Equal(expectedRecipeId, item.Id);

        }
        [Fact]
        public async void SaveAsync_ShouldWork()
        {
            //Arrange

            //Act
            await controller.SaveAsync();

            //Assert
            unitOfWorkMock.Verify(unitOfWork => unitOfWork.SaveChangesAsync());

        }


    }
}
