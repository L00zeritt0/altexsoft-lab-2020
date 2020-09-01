using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;

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
                try
                {
                    manager.ShowMainMenu();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("The recipe didn't add. There is some problem!");
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
