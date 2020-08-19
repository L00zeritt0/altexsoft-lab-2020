using RecipeBook.BL.Controller;
using RecipeBook.BL.Controller.Interfaces;
using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.ConsoleInterface
{
    class Manager
    {
        /// <summary>
        /// Temporary variables
        /// </summary>
        private int result;
        private String temp;
        private String categoryName;
        /// <summary>
        /// Our controllers
        /// </summary>
        public CategoryController categoryController;
        private IRecipeControllerInterface<Recipe> recipeController;
        private IFoodProductControllerInterface<FoodProduct> foodProductController;
        public List<Recipe> recipeListOfCategory;
        public Manager(String pathRecipes, String pathFoods, String pathCategories)
        {
            categoryController = new CategoryController(pathCategories);
            recipeController = new RecipeController(pathRecipes);
            foodProductController = new FoodProductController(pathFoods);
        }
        /// <summary>
        /// Methd shows list of categories of our recipe book
        /// </summary>
        public void ShowCategoryList()
        {
            ShowList<RecipeBookCategory>(categoryController.ListOfCatigories);
        }
        /// <summary>
        /// Methd shows recipe list by some category
        /// </summary>
        /// <param name="i"></param>
        public void ShowRecipeList(int  i )
        {
            Console.WriteLine("Recipes:");
            recipeListOfCategory = recipeController.BackRecipeListByCategory(categoryController.ListOfCatigories[i - 1]);
            ShowList<Recipe>(recipeListOfCategory);
        }
        /// <summary>
        /// Method Shows current recipe
        /// </summary>
        /// <param name="i"></param>
        public void ShowRecipe(int i)
        {
            Console.WriteLine("Recipe of " + recipeListOfCategory[i-1].RecipeName);
            Console.WriteLine("\n Ingredients of the recipe:");
            ShowList<RecipeIngredient>(recipeListOfCategory[i - 1].RecipeIngredients);
            Console.WriteLine("\n Steps of cooking:");
            ShowList<CookingStep>(recipeListOfCategory[i - 1].RecipeSteps);
        }
        /// <summary>
        /// Method adds food product in out foodProduct list
        /// </summary>
        public void FoodProductAdder()
        {
            Console.WriteLine("\nEnter the name of product:");
            foodProductController.AddFoodProduct(Console.ReadLine());
        }
        /// <summary>
        /// Method collect the information about our recipe and adds it into our recipe list
        /// </summary>
        public void RecipeAdder()
        {
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
            List<CookingStep> steps = new List<CookingStep>();
            Console.WriteLine("Enter the name of recipe:");
            String recipeName = Console.ReadLine();

            EnterRecipeCategory();
            EnterRecipeIngredients(ingredients);
            EnterRecipeSteps(steps);
            try
            {
                recipeController.AddRecipe(recipeName, categoryName, ingredients, steps);
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
                Console.WriteLine("\nChoose the number of category or press:" +
                    "\n q - to go to input of ingredients;");
                temp = Console.ReadLine();
                if (temp.Equals("q"))
                {
                    break;
                }
                else if (!Int32.TryParse(temp, out result))
                {
                    continue;
                }
                else if (result <= 0 || result > categoryController.ListOfCatigories.Count)
                {
                    continue;
                }
                categoryName = categoryController.ListOfCatigories[result - 1].CategoryName;
                break;
            }

        }
        /// <summary>
        /// Method gets and check list of ingredients. It contains a small menu too.
        /// </summary>
        /// <param name="ingredients"></param>
        private void EnterRecipeIngredients(List<RecipeIngredient> ingredients)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nList of products:");
                ShowList<FoodProduct>(foodProductController.ListOfFoods);
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
                    FoodProductAdder();
                    continue;
                }
                else if (!Int32.TryParse(temp, out result))
                {
                    continue;
                }
                else if (result <= 0 || result > foodProductController.ListOfFoods.Count)
                {
                    continue;
                }
                temp = foodProductController.ListOfFoods[result - 1].Name;
                Console.WriteLine($"\nEnter the weight of product {temp} in gramm:");
                if (!Int32.TryParse(Console.ReadLine(), out result))
                {
                    continue;
                }
                else if (result <= 0)
                {
                    continue;
                }
                ingredients.Add(new RecipeIngredient(temp, result));
            }
        }
        /// <summary>
        /// Method gets and checks list of steps. It contains a small menu too.
        /// </summary>
        /// <param name="steps"></param>
        private void EnterRecipeSteps(List<CookingStep> steps)
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nCooking steps:");
                ShowList<CookingStep>(steps);
                Console.WriteLine("\nEnter the step of cooking or press:" +
                    "\n q - to finish recipe adding;");
                String temp = Console.ReadLine();
                if (temp.Equals("q"))
                {
                    break;
                }
                steps.Add(new CookingStep(temp));
            }
        }
        /// <summary>
        /// Method show all items of some list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private void ShowList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"   {i + 1}.   {list[i]}");
            }
        }
    }
}
