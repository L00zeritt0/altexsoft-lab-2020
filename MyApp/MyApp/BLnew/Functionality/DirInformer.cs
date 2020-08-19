using MyApp.BLnew.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyApp.BLnew.Functionality
{
    class DirInformer: IDirInformable
    {
        private String path;
        private DirectoryInfo di;
        private DirectoryInfo[] dirArr;
        private FileInfo[] filesArr;
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    path = value;
                }
                else
                {
                    throw new NullReferenceException("Check the path. There is something wrong with this one.");
                }
            }
        }
          
        public DirInformer(String path)
        {
            Path = path;
            di = new DirectoryInfo(Path);
            dirArr = di.GetDirectories();
            filesArr = di.GetFiles();
        }
        public void ShowCurrentDirContent()
        {
            for (int i = 0; i < dirArr.Length; i++)
            {
                Console.WriteLine((i + 1) + " --- /" + dirArr[i].Name);
            }
            for (int i = 0; i < filesArr.Length; i++)
            {
                Console.WriteLine(filesArr[i].Name);
            }
        }
        public void ChooseElseDir(int i)
        {
            if (i > 0 && i <= dirArr.Length)
            {
                di = dirArr[i - 1];
                dirArr = di.GetDirectories();
                filesArr = di.GetFiles();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Enter the correct ID of a directory");
            }
        }
    }
}
