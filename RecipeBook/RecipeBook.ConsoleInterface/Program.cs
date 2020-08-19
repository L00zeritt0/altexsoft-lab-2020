using System;
using RecipeBook.BL.Model;
using RecipeBook.BL.Controller;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace RecipeBook.ConsoleInterface
{
    class Program
    {
        static void Main()
        {
            //Paths to our files
            String pathRecipes = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\recipes.json";
            String pathFoods = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\foods.json";
            String pathCategories = @"C:\Users\L00zeritt0\Desktop\AltexSoft\altexsoft-lab-2020\RecipeBook\categories.json";
            //Temporary variables
            String temp;
            int result;
            int category;

            Manager manager;
            //if something wrong with our files
            while (true)
            {
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

            while (true)
            {   
                // Show categories and choose one of them. 
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
                        manager.RecipeAdder();
                        continue;
                    }
                    else if (!Int32.TryParse(temp, out category))
                    {
                        continue;
                    }
                    else if (category <= 0 || category > manager.categoryController.ListOfCatigories.Count)
                    {
                        continue;
                    }
                    break;
                }
                //Show recipes of category and choose one of them or go back to out categories
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
                        manager.RecipeAdder();
                        break;
                    }
                    else if (!Int32.TryParse(temp, out result))
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
            }
        }
    }
}
