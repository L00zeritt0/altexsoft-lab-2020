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

namespace RecipeBook.ConsoleInterface
{
    class Manager
    {
        /// <summary>
        /// Temporary variables
        /// </summary>
        private int result;
        private string temp;
        private string categoryName;
        public List<Recipe> recipeListOfCategory;
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
        }
        /// <summary>
        /// Method collect an information about our recipe and adds it into our recipe list
        /// </summary>
        public void AddRecipe()
        {
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
            List<CookingStep> steps = new List<CookingStep>();
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the name of recipe:");
            string recipeName = Console.ReadLine();

            EnterRecipeCategory();
            EnterRecipeIngredients(ingredients);
            EnterRecipeSteps(steps);
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
            while (true)
            {
                Console.WriteLine("\nCategories:");
                ShowCategoryList();
                Console.WriteLine("\nChoose the number of category");
                temp = Console.ReadLine();
                if (int.TryParse(temp, out result))
                {
                    if (result > 0 || result <= categoryController.GetAllItems().Count)
                    {
                        categoryName = categoryController.GetAllItems()[result - 1].CategoryName;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Method gets and check list of ingredients. It contains a small menu too.
        /// </summary>
        /// <param name="ingredients">list of ingredients of new recipe</param>
        private void EnterRecipeIngredients(List<RecipeIngredient> ingredients)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nList of products:");
                ShowList<FoodProduct>(foodProductController.GetAllItems());
                Console.WriteLine("\nChoose the number of product or press:" +
                    "\n q - to go to input of steps;" +
                    "\n a - to add a new product;");

                temp = Console.ReadLine();
                if (temp.Equals("q"))
                {
                    break;
                }
                else if (temp.Equals("a"))
                {
                    AddFoodProduct();
                    continue;
                }
                else if (int.TryParse(temp, out result))
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
        }
        /// <summary>
        /// Method gets and checks list of steps. It contains a small menu too.
        /// </summary>
        /// <param name="steps">The lis of steps of new recipe</param>
        private void EnterRecipeSteps(List<CookingStep> steps)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nCooking steps:");
                ShowList<CookingStep>(steps);
                Console.WriteLine("\nEnter the step of cooking or press:" +
                    "\n q - to finish recipe adding;");
                temp = Console.ReadLine();
                if (temp.Equals("q"))
                {
                    break;
                }
                CookingStep cookingStep = new CookingStep();
                cookingStep.CookingStepDescription = temp;
                steps.Add(cookingStep);
            }
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
    }
}
