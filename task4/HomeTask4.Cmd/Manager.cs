using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Cmd
{
    public class Manager
    {
        #region Temporary variables
        private bool loopBreaker = true;
        private int result;
        private int? categoryId;
        private int recipeId;
        #endregion
        private readonly IController controller;
        public Manager(IController controller)
        {
            this.controller = controller;
        }
        public async Task ShowMainMenu()
        {
            categoryId = null;
            while (loopBreaker)
            {
                Console.Clear();
                var listOfCategoriesByCategoryId = await GetCategoriesById(categoryId);
                var listOfRecipesByCategoryId = await GetRecipesOfTheCategory(categoryId);

                ShowTwoList(listOfCategoriesByCategoryId, listOfRecipesByCategoryId);

                Console.WriteLine("Choose a category or a recipe. Q - go back. A - add recipe:");

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        var category = await controller.GetItemById<RecipeBookCategory>(categoryId.Value);
                        if (category.ParentId.HasValue)
                        {
                            categoryId = category.ParentId.Value;
                        }
                        else
                        {
                            loopBreaker = false;
                            categoryId = 0;
                        }
                        break;
                    case ConsoleKey.A:
                        await AddRecipe();
                        break;
                    default:
                        if (int.TryParse(key.KeyChar.ToString(), out result))
                        {
                            await ChooseCategoryOrRecipe(listOfCategoriesByCategoryId, listOfRecipesByCategoryId);
                        }
                        break;
                }
            }
            loopBreaker = true;
        }
        private void ChooseCategory(int number, List<RecipeBookCategory> categories)
        {
            if (number > 0 && number <= categories.Count)
            {
                categoryId = categories[number - 1].Id;
            }
        }
        private void ChooseRecipe(int number, List<Recipe> recipes)
        {
            if (number > 0 && number <= recipes.Count)
            {
                recipeId = recipes[number - 1].Id;
            }
        }
        private async Task ChooseCategoryOrRecipe(List<RecipeBookCategory> categories, List<Recipe> recipes)
        {
            if (result > 0 && result <= categories.Count)
            {
                ChooseCategory(result, categories);
            }
            else if (result > 0 && result <= (recipes.Count + categories.Count))
            {
                result = result - categories.Count;
                ChooseRecipe(result, recipes);
                await ShowRecipe(recipeId);
            }
        }
        private async Task<List<RecipeBookCategory>> GetCategoriesById(int? id)
        {
            var list = await controller.GetAllItemsAsync<RecipeBookCategory>();
            return list.ToList().FindAll(category => category.ParentId == id);
        }
        private async Task<List<Recipe>> GetRecipesOfTheCategory(int? id)
        {
            var list = await controller.GetAllItemsAsync<Recipe>();
            return list.ToList().FindAll(recipe => recipe.RecipeBookCategoryId == id);
        }
        private async Task ShowRecipe(int recipeId)
        {
            Console.Clear();
            var currentRecipe = await controller.GetItemById<Recipe>(recipeId);
            var foodProducts = (await controller.GetAllItemsAsync<FoodProduct>()).ToList();
            var ingredients = (await controller.GetAllItemsAsync<RecipeIngredient>()).ToList()
                .FindAll(ingredient => ingredient.RecipeId == recipeId);
            var steps = (await controller.GetAllItemsAsync<CookingStep>()).ToList()
                .FindAll(step => step.RecipeId == recipeId);
            Console.WriteLine("Recipe of " + currentRecipe.Name + "\n\n Ingredients of the recipe:");

            ShowList<RecipeIngredient>(ingredients);
            Console.WriteLine("\n Steps of cooking:");
            ShowList<CookingStep>(steps);
            loopBreaker = true;
            Console.ReadLine();
        }
        private async Task AddRecipe()
        {
            Console.Clear();
            Recipe recipe = new Recipe();
            Console.WriteLine("\nEnter the name of recipe:");
            recipe.Name = Console.ReadLine();
            await EnterRecipeCategory();
            recipe.RecipeBookCategoryId = categoryId.Value;
            recipe.Ingredients = await EnterRecipeIngredients();
            recipe.Steps = EnterRecipeSteps();
            await controller.AddItem<Recipe>(recipe);
            loopBreaker = true;
        }
        private async Task EnterRecipeCategory()
        {
            Console.Clear();
            var categories = (await controller.GetAllItemsAsync<RecipeBookCategory>()).ToList();
            var mainCategories = categories.FindAll(category => category.ParentId == null);
            foreach (var category in mainCategories)
            {
                Console.WriteLine(category.Name + "    ID: " + category.Id);
                var subcategories = categories.FindAll(subcategory => subcategory.ParentId == category.Id);
                foreach (var subcategory in subcategories)
                {
                    Console.WriteLine("     " + subcategory.Name + "     ID: " + subcategory.Id);
                }
            }
            while (loopBreaker)
            {
                Console.WriteLine("\nChoose an ID of a category:");
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (categories.Any(category => category.Id == result))
                    {
                        categoryId = result;
                        loopBreaker = false;
                    }
                }
            }
            loopBreaker = true;
        }
        private async Task<List<RecipeIngredient>> EnterRecipeIngredients()
        {
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();

            while (loopBreaker)
            {
                Console.Clear();
                var foodProducts = (await controller.GetAllItemsAsync<FoodProduct>()).ToList();
                ShowList(foodProducts);
                Console.WriteLine("\nChoose a number of a category. Q - go back. A - add foodProduct");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        loopBreaker = false;
                        break;
                    case ConsoleKey.A:
                        await AddFoodProduct();
                        break;
                    default:
                        if (int.TryParse(key.KeyChar.ToString(), out result))
                        {
                            if (result > 0 && result <= foodProducts.Count)
                            {
                                Console.WriteLine("\nEnter quantity or weight:");
                                RecipeIngredient recipeIngredient = new RecipeIngredient();
                                recipeIngredient.FoodProduct = foodProducts[result - 1];
                                recipeIngredient.QuantityOfFoodProduct = Console.ReadLine();
                                ingredients.Add(recipeIngredient);
                            }
                        }
                        break;
                }

            }

            loopBreaker = true;
            return ingredients;
        }
        private async Task AddFoodProduct()
        {
            Console.WriteLine("\nEnter the name of product:");
            FoodProduct foodProduct = new FoodProduct();
            foodProduct.Name = Console.ReadLine();
            await controller.AddItem<FoodProduct>(foodProduct);
        }
        private List<CookingStep> EnterRecipeSteps()
        {

            List<CookingStep> steps = new List<CookingStep>();
            while (loopBreaker)
            {
                Console.Clear();
                ShowList(steps);
                Console.WriteLine("\nQ - go back. A - add a step");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        loopBreaker = false;
                        break;
                    case ConsoleKey.A:
                        Console.WriteLine("\nEnter a discription of the spep:");
                        steps.Add(new CookingStep { Description = Console.ReadLine() });
                        break;
                }
            }
            loopBreaker = true;
            return steps;
        }
        private void ShowList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"   {i + 1}.   {list[i]}");
            }
        }
        private void ShowTwoList<T, V>(List<T> list1, List<V> list2)
        {
            int i, j;
            for (i = 0; i < list1.Count; i++)
            {
                Console.WriteLine($"   {i + 1}.   {list1[i]}");
            }
            for (j = 0; j < list2.Count; j++)
            {
                Console.WriteLine($"   {j + i + 1}.   {list2[j]}");
            }
        }
    }

}
