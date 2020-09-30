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
    class Manager
    {
        #region Fields for working with resource files
        private ResourceManager addCategoryStrings = new ResourceManager("HomeTask4.Cmd.AddCategoryStrings", typeof(Manager).Assembly);
        private ResourceManager addIngredientStrings = new ResourceManager("HomeTask4.Cmd.AddIngredientStrings", typeof(Manager).Assembly);
        private ResourceManager addStepStrings = new ResourceManager("HomeTask4.Cmd.AddStepStrings", typeof(Manager).Assembly);
        private ResourceManager mainMenuStrings = new ResourceManager("HomeTask4.Cmd.MainMenuStrings", typeof(Manager).Assembly);
        private ResourceManager subcategoryRecipeListMenuStrings = new ResourceManager("HomeTask4.Cmd.SubcategoryRecipeListMenuStrings", typeof(Manager).Assembly);
        private ResourceManager addSubCategoryStrings = new ResourceManager("HomeTask4.Cmd.AddSubcategoryStrings", typeof(Manager).Assembly);
        #endregion

        #region Temporary variables
        private bool flag = false;
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
        private List<Recipe> listOfRecipes;
        private List<Recipe> noCategoryRecipes;
        #endregion

        #region Fields for working with Controllers of different types 
        private readonly IController controller;
        #endregion

        public Manager(IController controller)
        {
            this.controller = controller;
            categories = controller.GetAllItems<RecipeBookCategory>().Result;
            mainCategories = categories.FindAll(category => category.ParentId == null);
            recipeListOfCategory = new List<Recipe>();
            foodProductList = controller.GetAllItems<FoodProduct>().Result;
        }
        /// <summary>
        /// Method shows the list of categories of our recipe book and show the list of recipes and subcategories of chosen category
        /// </summary>
        public void ShowMainMenu()
        {
            CommonMethodOfMenu(mainCategories, new List<object>(), null, AddRecipe, ChooseCategory, mainMenuStrings);
            GetRecipeListByCategory(categoryId);
            CommonMethodOfMenu(subcategories, recipeListOfCategory, Break, AddRecipe, ChooseSubcategoryOrRecipe, subcategoryRecipeListMenuStrings);
        }
        private void GetRecipeListByCategory(int categoryId)
        {
            recipeListOfCategory = controller.GetAllItems<Recipe>().Result.FindAll(recipe => recipe.RecipeBookCategoryId == categoryId);
        }
        /// <summary>
        /// Method shows current recipe
        /// </summary>
        /// <param name="i">number of recipe</param>
        private void ShowRecipe()
        {
            if (result > 0 && result <= listOfRecipes.Count)
            {
                Console.Clear();
                var currentRecipe = listOfRecipes.ElementAt(result - 1);
                Console.WriteLine("Recipe of " + currentRecipe.Name + "\n\n Ingredients of the recipe:");
                ShowList<RecipeIngredient>(controller.GetAllItems<RecipeIngredient>().
                    Result.FindAll(ingredient => ingredient.RecipeId == currentRecipe.Id));
                Console.WriteLine("\n Steps of cooking:");
                ShowList<CookingStep>(controller.GetAllItems<CookingStep>().
                    Result.FindAll(step => step.RecipeId == currentRecipe.Id));
                flag = true;
                Console.ReadLine();
            }
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
        /// Method collect an information about our recipe and adds it into our recipe list
        /// </summary>
        private async void AddRecipe()
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
            await controller.AddItem<Recipe>(recipe);
            await controller.Save();
            flag = true;
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
        private void CommonMethodOfMenu<T, V>(List<T> list1, List<V> list2, Action pressQ, Action pressA, Action finalStep, ResourceManager rm)
        {
            while (!flag)
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
                            finalStep();
                        }
                        break;
                }
            }
            flag = false;
        }
        /// <summary>
        /// Method chooses subcategory or recipe from both lists (list of subcategories or list of recipes without subcategory)
        /// </summary>
        private void ChooseSubcategoryOrRecipe()
        {
            if (result > 0 && result <= subcategories.Count)
            {
                GetRecipeListByCategory(subcategories[result - 1].Id);
                listOfRecipes = recipeListOfCategory;
                CommonMethodOfMenu(recipeListOfCategory, new List<object>(), Break, AddRecipe, ShowRecipe, subcategoryRecipeListMenuStrings);
                flag = true;
            }
            else if (result > 0 && result <= (subcategories.Count + recipeListOfCategory.Count))
            {
                listOfRecipes = recipeListOfCategory;
                result -= (subcategories.Count);
                ShowRecipe();
            }
           
        }
        /// <summary>
        /// Method sets flag
        /// </summary>
        private void Break()
        {
            flag = true;
        }
        /// <summary>
        /// Method adds new ingredient to the list of ingredients in CommonMethodOfMenu
        /// </summary>
        private void AddIngredientToList()
        {
            FoodProduct foodProduct = foodProductList.ElementAt(result - 1);
            Console.WriteLine($"\nEnter the quantity of product {temp} and its measure:");
            RecipeIngredient recipeIngredient = new RecipeIngredient();
            recipeIngredient.FoodProduct = foodProduct;
            recipeIngredient.QuantityOfFoodProduct = Console.ReadLine();
            ingredients.Add(recipeIngredient);
        }
        /// <summary>
        /// Method chooses the category from category list
        /// </summary>
        private void ChooseCategory()
        {
            if (result > 0 && result <= mainCategories.Count)
            {
                categoryId = mainCategories[result - 1].Id;
                subcategories = categories.FindAll(category => category.ParentId == categoryId);
                flag = true;
            }
        }
        /// <summary>
        /// Method chooses the subcategory from subcategory list
        /// </summary>
        private void ChooseSubcategory()
        {
            if (result > 0 && result <= subcategories.Count)
            {
                categoryId = subcategories[result - 1].Id;
                flag = true;
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
