using System;
using RecipeBook.BL.Model;
using RecipeBook.BL.Controller;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using RecipeBook.DL;
using System.Runtime.CompilerServices;

namespace RecipeBook.ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            #region Paths to my files
            string pathRecipes = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\recipes.json";
            string pathFoods = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\foods.json";
            string pathCategories = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\categories.json";
            #endregion

            #region Temporary variables
            string temp;
            int result;
            int category;
            #endregion

            Manager manager;
            
            #region if something wrong with our json files
            while (true)
            {
                //Console.WriteLine("\nEnter the path to file of list of recipes:");
                //string pathRecipes = Console.ReadLine();
                //Console.WriteLine("\nEnter the path to file of list of food product:");
                //string pathFoods = Console.ReadLine();
                //Console.WriteLine("\nEnter the path to file of list of categories:");
                //string pathCategories = Console.ReadLine();
                try
                {
                    manager = new Manager(pathRecipes, pathFoods, pathCategories);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadLine();
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
                    else if (category <= 0 || category > manager.categoryController.GetAllItems().Count)
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
                    manager.ShowRecipeList(category);
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
                    else if (result <= 0 || result > manager.recipeListOfCategory.Count)
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
