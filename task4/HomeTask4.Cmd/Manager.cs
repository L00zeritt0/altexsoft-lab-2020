using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Cmd
{
    public class Manager
    {
        #region Fields for working with resource files
        private ResourceManager addCategoryStrings = new ResourceManager("HomeTask4.Cmd.Resources.AddCategoryStrings", typeof(Manager).Assembly);
        private ResourceManager addIngredientStrings = new ResourceManager("HomeTask4.Cmd.Resources.AddIngredientStrings", typeof(Manager).Assembly);
        private ResourceManager addStepStrings = new ResourceManager("HomeTask4.Cmd.Resources.AddStepStrings", typeof(Manager).Assembly);
        private ResourceManager mainMenuStrings = new ResourceManager("HomeTask4.Cmd.Resources.MainMenuStrings", typeof(Manager).Assembly);
        private ResourceManager subcategoryRecipeListMenuStrings = new ResourceManager("HomeTask4.Cmd.Resources.SubcategoryRecipeListMenuStrings", typeof(Manager).Assembly);
        private ResourceManager addSubCategoryStrings = new ResourceManager("HomeTask4.Cmd.Resources.AddSubcategoryStrings", typeof(Manager).Assembly);
        #endregion

        #region Temporary variables
        private bool loopBreaker = false;
        private int result;
        private int categoryId;
        private string temp;

        #endregion

        #region The lists
        private List<Recipe> recipeListOfCategory;
        private List<RecipeBookCategory> categories;
        private List<RecipeBookCategory> mainCategories;
        private List<RecipeBookCategory> subcategories;
        private List<FoodProduct> foodProductList;
        private List<RecipeIngredient> ingredients;
        private List<CookingStep> steps;
        #endregion

        #region Fields for working with Controllers of different types 
        private readonly IController controller;
        #endregion

        public Manager(IController controller)
        {
            this.controller = controller;
        }
        /// <summary>
        /// Method shows the list of categories of our recipe book and show the list of recipes and subcategories of chosen category
        /// </summary>
        public void ShowMainMenu()
        {
            GetAllFoodProducts();
            GetMainCategories();
            CommonMethodOfMenu(mainCategories, new List<object>(), null, AddRecipe, ChooseCategory, mainMenuStrings);
            GetRecipeListByCategory(categoryId);
            CommonMethodOfMenu(subcategories, recipeListOfCategory, Break, AddRecipe, ChooseSubcategoryOrRecipe, subcategoryRecipeListMenuStrings);
        }
        private void GetRecipeListByCategory(int categoryId)
        {
            recipeListOfCategory = controller.GetAllItemsAsync<Recipe>().Result.ToList().FindAll(recipe => recipe.RecipeBookCategoryId == categoryId);
        }
        private void GetAllCategoriest()
        {
            categories = controller.GetAllItemsAsync<RecipeBookCategory>().Result.ToList();
        }
        private void GetMainCategories()
        {
            GetAllCategoriest();
            mainCategories = categories.FindAll(category => category.ParentId == null);
        }
        private void GetAllFoodProducts()
        {
            foodProductList = controller.GetAllItemsAsync<FoodProduct>().Result.ToList();
        }
        /// <summary>
        /// Method shows current recipe
        /// </summary>
        private void ShowRecipe(int number)
        {
            Console.Clear();
            number = recipeListOfCategory[number - 1].Id;
            var currentRecipe = controller.GetItemById<Recipe>(number);
            Console.WriteLine("Recipe of " + currentRecipe.Name + "\n\n Ingredients of the recipe:");
            ShowList<RecipeIngredient>(controller.GetAllItemsAsync<RecipeIngredient>().
                Result.ToList().FindAll(ingredient => ingredient.RecipeId == number));
            Console.WriteLine("\n Steps of cooking:");
            ShowList<CookingStep>(controller.GetAllItemsAsync<CookingStep>().
                Result.ToList().FindAll(step => step.RecipeId == number));
            loopBreaker = true;
            Console.ReadLine();
        }
        /// <summary>
        /// Method adds a food product in out foodProduct list
        /// </summary>
        private void AddFoodProduct()
        {
            Console.WriteLine("\nEnter the name of product:");
            FoodProduct foodProduct = new FoodProduct();
            foodProduct.Name = Console.ReadLine();
            controller.AddItem<FoodProduct>(foodProduct);
            foodProductList.Add(foodProduct);
        }
        /// <summary>
        /// Method collects an information about our recipe and adds it into our recipe list
        /// </summary>
        private void AddRecipe()
        {
            Console.Clear();
            ingredients = new List<RecipeIngredient>();
            steps = new List<CookingStep>();
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the name of recipe:");
            string recipeName = Console.ReadLine();

            EnterRecipeCategory();
            EnterRecipeIngredients();
            EnterRecipeSteps();
            recipe.Name = recipeName;
            recipe.RecipeBookCategoryId = categoryId;
            recipe.Ingredients = ingredients;
            recipe.Steps = steps;
            controller.AddItem<Recipe>(recipe);
            controller.SaveAsync().Wait();
            loopBreaker = true;
        }
        /// <summary>
        /// Method gets and checks a name of a category. It contains a small menu too.
        /// </summary>
        private void EnterRecipeCategory()
        {
            CommonMethodOfMenu(mainCategories, new List<object>(), null, null, ChooseCategory, addCategoryStrings);
            EnterRecipeSubCategory();
        }
        /// <summary>
        /// Method gets and checks a name of a subcategory. It contains a small menu too.
        /// </summary>
        private void EnterRecipeSubCategory()
        {
            CommonMethodOfMenu(subcategories, new List<object>(), Break, null, ChooseSubcategory, addSubCategoryStrings);
        }
        /// <summary>
        /// Method gets and check list of ingredients. It contains a small menu too.
        /// </summary>
        /// <param name="ingredients">list of ingredients of new recipe</param>
        private void EnterRecipeIngredients()
        {
            CommonMethodOfMenu(foodProductList, new List<object>(), Break, AddFoodProduct, AddIngredientToList, addIngredientStrings);
        }
        /// <summary>
        /// Method gets and checks list of steps. It contains a small menu too.
        /// </summary>
        /// <param name="steps">The lis of steps of new recipe</param>
        private void EnterRecipeSteps()
        {
            CommonMethodOfMenu(steps, new List<object>(), Break, AddStepToList, null, addStepStrings); ;
        }
        /// <summary>
        /// Method show all items of some list
        /// </summary>
        /// <typeparam name="T">type of items</typeparam>
        /// <param name="list">list of items</param>
        private void ShowList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"   {i + 1}.   {list[i]}");
            }
        }
        /// <summary>
        /// Method show all items of both lists
        /// </summary>
        /// <typeparam name="T">type of items</typeparam>
        /// <typeparam name="V">type of items</typeparam>
        /// <param name="list1">list of items</param>
        /// <param name="list2">list of items</param>
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
        /// <summary>
        /// Common method of menu
        /// </summary>
        /// <typeparam name="T">Type of input list</typeparam>
        /// <param name="list">List of Categories, or food products, or steps of cooking</param>
        /// <param name="pressQ">If press q delegate parameter</param>
        /// <param name="pressA">If press a delegate parameter</param>
        /// <param name="finalStep">Final step of common metod. Delegate parameter</param>
        /// <param name="rm">Recource file</param>
        private void CommonMethodOfMenu<T, V>(List<T> list1, List<V> list2, Action pressQ, Action pressA, Action<int> finalStep, ResourceManager rm)
        {
            while (!loopBreaker)
            {
                Console.Clear();
                Console.WriteLine(rm.GetString("List"));
                if (list2.Count > 0)
                {
                    ShowTwoList(list1, list2);
                }
                else
                {
                    ShowList(list1);
                }

                Console.WriteLine(rm.GetString("Instruction"));

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        pressQ?.Invoke();
                        break;
                    case ConsoleKey.A:
                        pressA?.Invoke();
                        break;
                    default:
                        if (int.TryParse(key.KeyChar.ToString(), out result))
                        {
                            finalStep(result);
                        }
                        break;
                }
            }
            loopBreaker = false;
        }
        /// <summary>
        /// Method chooses subcategory or recipe from both lists (list of subcategories or list of recipes without subcategory)
        /// </summary>
        private void ChooseSubcategoryOrRecipe(int number)
        {
            if (number > 0 && number <= subcategories.Count)
            {
                GetRecipeListByCategory(subcategories[number - 1].Id);
                CommonMethodOfMenu(recipeListOfCategory, new List<object>(), Break, AddRecipe, ShowRecipe, subcategoryRecipeListMenuStrings);
                loopBreaker = true;
            }
            else if (number > 0 && number <= (subcategories.Count + recipeListOfCategory.Count))
            {
                number -= (subcategories.Count);
                ShowRecipe(number);
            }

        }
        /// <summary>
        /// Method sets flag
        /// </summary>
        private void Break()
        {
            loopBreaker = true;
        }
        /// <summary>
        /// Method adds new ingredient to the list of ingredients in CommonMethodOfMenu
        /// </summary>
        private void AddIngredientToList(int number)
        {
            FoodProduct foodProduct = foodProductList.ElementAt(number - 1);
            Console.WriteLine($"\nEnter the quantity of product {temp} and its measure:");
            RecipeIngredient recipeIngredient = new RecipeIngredient();
            recipeIngredient.FoodProduct = foodProduct;
            recipeIngredient.QuantityOfFoodProduct = Console.ReadLine();
            ingredients.Add(recipeIngredient);
        }
        /// <summary>
        /// Method chooses the category from category list
        /// </summary>
        private void ChooseCategory(int number)
        {
            if (number > 0 && number <= mainCategories.Count)
            {
                categoryId = mainCategories[number - 1].Id;
                subcategories = categories.FindAll(category => category.ParentId == categoryId);
                loopBreaker = true;
            }
        }
        /// <summary>
        /// Method chooses the subcategory from subcategory list
        /// </summary>
        private void ChooseSubcategory(int number)
        {
            if (number > 0 && number <= subcategories.Count)
            {
                categoryId = subcategories[number - 1].Id;
                loopBreaker = true;
            }
        }
        /// <summary>
        /// Method adds new step to the list of steps in CommonMethodOfMenu
        /// </summary>
        private void AddStepToList()
        {
            Console.WriteLine("\nEnter a discription of cooking step:");
            CookingStep cookingStep = new CookingStep();
            cookingStep.Description = Console.ReadLine();
            steps.Add(cookingStep);
        }
    }
}
