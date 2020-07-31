using System;
using System.IO;

namespace MyApp.BL
{
    class DirInformer: IDirInformer
    {
        private String path;
        private DirectoryInfo di;
        private DirectoryInfo[] diArr;
        private FileInfo[] fiArr;

        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                if (!value.Equals(""))
                {
                    path = value;
                }
                else
                {
                    Console.WriteLine("Check the path. It's empty");
                    throw new NullReferenceException();
                }
            }
        }
        public DirInformer(String path)
        {
            Path = path;
            di = new DirectoryInfo(Path);
            try
            {
                diArr = di.GetDirectories();
                fiArr = di.GetFiles();
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory not found");
                throw e;
            }
            catch (IOException e)
            {
                Console.WriteLine("Wrong path");
                throw e;
            }
        }
        public void ShowCurrentDirContent()
        {
            for (int i = 0; i < diArr.Length; i++)
            {
                Console.WriteLine((i + 1) + " --- /" + diArr[i].Name);
            }
            for (int i = 0; i < fiArr.Length; i++)
            {
                Console.WriteLine(fiArr[i].Name);
            }
            Console.WriteLine();
        }
        public void ChoseElseDir(int i)
        {
            if (i > 0 && i <= diArr.Length)
            {
                di = diArr[i - 1];
                diArr = di.GetDirectories();
                fiArr = di.GetFiles();
                ShowCurrentDirContent();
            }
            else
            {
                Console.WriteLine("Put the correct nomber of dir.");
            }
        }
    }
}
