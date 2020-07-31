using MyApp.BL;
using MyApp.Manager;
using System;
using System.IO;

namespace MyApp
{
    class Program
    {
        static void Main()
        {
            String path;
            TaksManager manager = new TaksManager();
            while (true)
            {
                
                Console.WriteLine("\n1 --- Delete String from a text file");
                Console.WriteLine("2 --- Show count of words and every tenth word in a text file");
                Console.WriteLine("3 --- The third sentence method");
                Console.WriteLine("4 --- Working with dir\n");
                Console.WriteLine("Chose the method:\n");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.WriteLine("\nEnter the path:");
                            path = Console.ReadLine();
                            Console.WriteLine("\nEnter the word or char:");
                            try
                            {
                                manager.DeleteFromTheText(path, Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Catch exception");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("\nEnter the path:");
                            path = Console.ReadLine();
                            try
                            {
                                manager.EveryTenthAndCount(path);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Catch exception");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\nEnter the path:");
                            path = Console.ReadLine();
                            try
                            {
                                manager.TheThirdSentence(path);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Catch exception");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\nEnter the path:");
                            path = Console.ReadLine();
                            try
                            {
                                manager.WorkWithDir(path);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Catch exception");
                            }
                            Console.ReadLine();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nWrong number...");
                            break;
                        }
                }
            }
           
        }
    }
}