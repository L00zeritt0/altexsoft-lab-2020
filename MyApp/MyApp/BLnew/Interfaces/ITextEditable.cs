using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.BLnew.Interfaces
{
    interface ITextEditable
    {
        String Word { set; }
        String EditText();
    }
}
