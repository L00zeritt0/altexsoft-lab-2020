using System;
using RecipeBook.BL.Model;
using RecipeBook.BL.Controller;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using RecipeBook.DL;
using System.Runtime.CompilerServices;
using System.Linq;

namespace RecipeBook.ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            #region Paths to json files
            string pathRecipes = AppDomain.CurrentDomain.BaseDirectory + "recipes.json";
            string pathFoods = AppDomain.CurrentDomain.BaseDirectory + "foods.json";
            string pathCategories = AppDomain.CurrentDomain.BaseDirectory + "categories.json";
            #endregion

            #region Temporary variables
            string temp;
            int result;
            int category;
            #endregion

            Manager manager;

            #region Create object Manager and make an exception handling.
            try
            {
                manager = new Manager(pathRecipes, pathFoods, pathCategories);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }
            #endregion

            while (true)
            {
                #region Show categories and choose one of them. 

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Categories:");
                    manager.ShowCategoryList();
                    Console.WriteLine("\nChoose the number of category or press:" +
                        "\n a - add new recipe;\n");
                    temp = Console.ReadLine();
                    if (temp.Equals("a"))
                    {
                        Console.Clear();
                        manager.AddRecipe();
                        continue;
                    }
                    else if (!int.TryParse(temp, out category))
                    {
                        continue;
                    }
                    else if (category <= 0 && category > manager.categories.Count)
                    {
                        continue;
                    }
                    break;
                }
                #endregion

                #region Show recipes of category. Menu: choose one of them or go back to our categories
                while (true)
                {
                    Console.Clear();
                    manager.ShowRecipeList(manager.categories[category - 1].Name);
                    Console.WriteLine("\nChoose the number of recipe or press;" +
                        "\n q - go back;" +
                        "\n a - add new recipe;\n");
                    temp = Console.ReadLine();
                    if (temp.Equals("q"))
                    {
                        break;
                    }
                    else if (temp.Equals("a"))
                    {
                        Console.Clear();
                        manager.AddRecipe();
                        break;
                    }
                    else if (!int.TryParse(temp, out result))
                    {
                        continue;
                    }
                    else if (result <= 0 && result > manager.recipeListOfCategory.Count)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        manager.ShowRecipe(result);
                        Console.WriteLine("\nPress ENTER to go back!");
                        Console.ReadLine();
                    }
                }
                #endregion
            }
        }
    }
}
