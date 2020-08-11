using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyApp.BLnew.Functionality
{
    abstract class BaseTextWorker
    {
        private String path;
        public String Text { get; set; }
        public String Path
        {
            private get
            {
                return path;
            }
            set
            {
                if (File.Exists(value))
                {
                    path = value;
                }
                else
                {
                    throw new ArgumentException("Check the path. There is no file or the path is empty.");
                }
            }
        }  

        public BaseTextWorker(String path)
        {
            Path = path;
            Text = ReadTextFile();
        }

        private String ReadTextFile()
        {
            Text = File.ReadAllText(Path);
            if (String.IsNullOrWhiteSpace(Text))
            {
                throw new Exception("The file is empty.");
            }
            return Text;
        }
    }
}
