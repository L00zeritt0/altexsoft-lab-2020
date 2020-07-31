using System;
using System.IO;

namespace MyApp.BL
{
    class TextFileReader: ITextReadable
    {
        private String path;
        public String Path
        {
            set
            {
                if (!value.Equals(""))
                {
                    path = value;
                }
                else
                {
                    Console.WriteLine("Check the path. It's empty.");
                    throw new NullReferenceException();
                }
            }
        }
        public TextFileReader(String path)
        {
            Path = path;
        }
        public String ReadText()
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Check your path.");
                throw e;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory not found");
                throw e;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Wrong path");
                throw e;
            }

        }

    }
}
