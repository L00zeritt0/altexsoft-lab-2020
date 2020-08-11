using System;
using System.Collections.Generic;
using System.Text;
using MyApp.BLnew.Interfaces;

namespace MyApp.BLnew.Functionality
{
    class TextEditer: BaseTextWorker, ITextEditable
    {
        private String word;
        public String Word
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    word = value;
                }
                else
                {
                    throw new NullReferenceException("Check your word. It's empty.");
                }
            }
        }

        public TextEditer(String path, String word): base(path)
        {
            Word = word;
        }

        public String EditText()
        {
            if (Text.Contains(word))
            {
                return Text.Replace(word, "");
            }
            throw new ArgumentNullException("There is no current word.");
        }
    }
}
