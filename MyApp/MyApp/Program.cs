
using MyApp.BLnew.Functionality;
using MyApp.BLnew.Interfaces;
using System;


namespace MyApp
{
    class Program
    {
        static void Main()
        {

            String path;
            String word;
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
                            word = Console.ReadLine();
                            try
                            {
                                ITextEditable te = new TextEditer(path, word);
                                Console.WriteLine("\n" + te.EditText());
                                Console.WriteLine();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
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
                                IWordsCountable wc = new WordsCounter(path);
                                Console.WriteLine("\n" + wc.WordCounter());
                                IEveryTenthWordTakable wt = new EveryTenthWordTaker(path);
                                Console.WriteLine("\n" + wt.EveryTenthWord());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
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
                                IThirdSentenceReversible ts = new ThirdSentenceReverser(path);
                                Console.WriteLine("\n" + ts.ThirdSentenceReverse());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
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
                                IDirInformable di = new DirInformer(path);
                                di.ShowCurrentDirContent();
                                Console.WriteLine("\nChoose the dir:");
                                di.ChooseElseDir(Int32.Parse(Console.ReadLine()));
                                di.ShowCurrentDirContent();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
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