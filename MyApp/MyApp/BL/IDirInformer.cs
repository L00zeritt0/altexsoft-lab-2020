using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.BL
{
    interface IDirInformer
    {
        String Path { get; set; }
        void ShowCurrentDirContent();
        void ChoseElseDir(int i);
    }
}
