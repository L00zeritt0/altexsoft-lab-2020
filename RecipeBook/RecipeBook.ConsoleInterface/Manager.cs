using RecipeBook.BL.Controller;
using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace RecipeBook.ConsoleInterface
{
    class Manager
    {
        public ResourceManager addCategoryStrings = new ResourceManager("RecipeBook.ConsoleInterface.AddCategoryStrings", typeof(Manager).Assembly);
        public ResourceManager addIngredientStrings = new ResourceManager("RecipeBook.ConsoleInterface.AddIngredientStrings", typeof(Manager).Assembly);
        public ResourceManager addStepStrings = new ResourceManager("RecipeBook.ConsoleInterface.AddStepStrings", typeof(Manager).Assembly);
        
        private int flag = 0;
        /// <summary>
        /// Temporary variables
        /// </summary>
        private int result;
        private string temp;
        private string categoryName;
        public List<Recipe> recipeListOfCategory;
        private List<FoodProduct> foodProductList;
        private List<RecipeIngredient> ingredients;
        private List<CookingStep> steps;
        /// <summary>
        /// Our controllers
        /// </summary>
        public readonly IController<RecipeBookCategory> categoryController;
        private readonly IController<Recipe> recipeController;
        private readonly IController<FoodProduct> foodProductController;
        public Manager(string pathRecipes, string pathFoods, string pathCategories)
        {
            categoryController = new Controller<RecipeBookCategory>(pathCategories);
            recipeController = new Controller<Recipe>(pathRecipes);
            foodProductController = new Controller<FoodProduct>(pathFoods);
        }
        /// <summary>
        /// Method shows the list of categories of our recipe book
        /// </summary>
        public void ShowCategoryList()
        {
            ShowList<RecipeBookCategory>(categoryController.GetAllItems());
        }
        /// <summary>
        /// Methd shows recipe list by some category
        /// </summary>
        /// <param name="i">number of category</param>
        public void ShowRecipeList(int i)
        {
            Console.WriteLine("Recipes:");
            recipeListOfCategory = (from r in recipeController.GetAllItems()
                                    where r.RecipeCategory.CategoryName.Equals(categoryController.GetAllItems()[i - 1].CategoryName)
                                    select r).ToList<Recipe>();
            ShowList<Recipe>(recipeListOfCategory);
        }
        /// <summary>
        /// Method Shows current recipe
        /// </summary>
        /// <param name="i">number of recipe</param>
        public void ShowRecipe(int i)
        {
            Console.WriteLine("Recipe of " + recipeListOfCategory[i - 1].RecipeName);
            Console.WriteLine("\n Ingredients of the recipe:");
            ShowList<RecipeIngredient>(recipeListOfCategory[i - 1].RecipeIngredients);
            Console.WriteLine("\n Steps of cooking:");
            ShowList<CookingStep>(recipeListOfCategory[i - 1].RecipeSteps);
        }
        /// <summary>
        /// Method adds a food product in out foodProduct list
        /// </summary>
        public void AddFoodProduct()
        {
            Console.WriteLine("\nEnter the name of product:");
            FoodProduct foodProduct = new FoodProduct();
            foodProduct.Name = Console.ReadLine();
            foodProductController.AddItem(foodProduct);
            foodProductList.Add(foodProduct);
        }
        /// <summary>
        /// Method collect an information about our recipe and adds it into our recipe list
        /// </summary>
        public void AddRecipe()
        {
            foodProductList = foodProductController.GetAllItems();
            ingredients = new List<RecipeIngredient>();
            steps = new List<CookingStep>();
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the name of recipe:");
            string recipeName = Console.ReadLine();

            EnterRecipeCategory();
            EnterRecipeIngredients();
            EnterRecipeSteps();
            try
            {
                recipe.RecipeName = recipeName;
                RecipeBookCategory recipeBookCategory = new RecipeBookCategory();
                recipeBookCategory.CategoryName = categoryName;
                recipe.RecipeCategory = recipeBookCategory;

                recipe.RecipeIngredients = ingredients;
                recipe.RecipeSteps = steps;
                recipeController.AddItem(recipe);
                
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("The recipe didn't add. There is some problem!");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Method gets and checks a name of a category. It contains a small menu too.
        /// </summary>
        private void EnterRecipeCategory()
        {
            CommonMethodOfMenu(categoryController.GetAllItems(), null, null, ChooseCategory, addCategoryStrings);
        }
        /// <summary>
        /// Method gets and check list of ingredients. It contains a small menu too.
        /// </summary>
        /// <param name="ingredients">list of ingredients of new recipe</param>
        private void EnterRecipeIngredients()
        {
            CommonMethodOfMenu(foodProductList, Break, AddFoodProduct, AddIngredientToList, addIngredientStrings);
        }
        /// <summary>
        /// Method gets and checks list of steps. It contains a small menu too.
        /// </summary>
        /// <param name="steps">The lis of steps of new recipe</param>
        private void EnterRecipeSteps()
        {
            CommonMethodOfMenu(steps, Break, null, AddStepToList, addStepStrings);
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
        /// Common method of menu
        /// </summary>
        /// <typeparam name="T">Type of input list</typeparam>
        /// <param name="list">List of Categories, or food products, or steps of cooking</param>
        /// <param name="pressQ">If press q delegate parameter</param>
        /// <param name="pressA">If press a delegate parameter</param>
        /// <param name="finalStep">Final step of common metod. Delegate parameter</param>
        /// <param name="rm">Recource file</param>
        private void CommonMethodOfMenu<T>(List<T> list, Action pressQ, Action pressA, Action finalStep, ResourceManager rm)
        {
            while (flag != 1)
            {
                Console.Clear();
                Console.WriteLine(rm.GetString("List"));
                ShowList(list);
                Console.WriteLine(rm.GetString("Instruction"));
            
                temp = Console.ReadLine();
                switch (temp)
                {
                    case "q":
                        pressQ?.Invoke();
                        break;
                    case "a":
                        pressA?.Invoke();
                        break;
                    default:
                        finalStep();
                        break;
                }
            }
            flag = 0;
        }
        /// <summary>
        /// Method sets flag
        /// </summary>
        private void Break()
        {
            flag = 1;
        }
        /// <summary>
        /// Method adds new ingredient to the list of ingredients in CommonMethodOfMenu
        /// </summary>
        private void AddIngredientToList()
        {
            if (int.TryParse(temp, out result))
            {
                if (result > 0 || result <= foodProductController.GetAllItems().Count)
                {
                    temp = foodProductController.GetAllItems()[result - 1].Name;
                    Console.WriteLine($"\nEnter the quantity of product {temp} and its measure:");
                    FoodProduct foodProduct = new FoodProduct();
                    foodProduct.Name = temp;
                    RecipeIngredient recipeIngredient = new RecipeIngredient();
                    recipeIngredient.FoodProduct = foodProduct;
                    recipeIngredient.QuantityOfFoodProduct = Console.ReadLine();
                    ingredients.Add(recipeIngredient);
                }
            }
            
        }
        /// <summary>
        /// Method chooses the category from category list
        /// </summary>
        private void ChooseCategory()
        {
            if (int.TryParse(temp, out result))
            {
                if (result > 0 || result <= categoryController.GetAllItems().Count)
                {
                    categoryName = categoryController.GetAllItems()[result - 1].CategoryName;
                    flag = 1;
                }
            }
        }
        /// <summary>
        /// Method adds new step to the list of steps in CommonMethodOfMenu
        /// </summary>
        private void AddStepToList()
        {
            CookingStep cookingStep = new CookingStep();
            cookingStep.CookingStepDescription = temp;
            steps.Add(cookingStep);
        }
    }
}
