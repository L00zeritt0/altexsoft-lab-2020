using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.BL
{
    interface ITextEditable: IThirdSentenceReverse
    {
        String Text { set; }
        String Word { set; }
        String EditText();
        int WordCounter();
        void EveryTenthWord();

    }
}
