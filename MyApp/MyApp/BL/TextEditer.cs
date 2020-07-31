using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.BL
{
    class TextEditer : ITextEditable
    {
        private String text;
        private String word; 
        public String Text
        {
            set
            {
                if (!value.Equals(""))
                {
                    text = value;
                }
                else
                {
                    Console.WriteLine("Check your text. There is nothing.");
                    throw new NullReferenceException();
                }
            }
        }
        public String Word
        {
            set
            {
                if (!value.Equals(""))
                {
                    word = value;
                }
                else
                {
                    Console.WriteLine("Check your word. It's empty.");
                    throw new NullReferenceException();
                }
            }
        }
        public TextEditer(String text)
        {
            Text = text;
        }
        public String EditText()
        {
            if (text.Contains(word))
            {
                StringBuilder sb = new StringBuilder(text);
                return sb.Replace(word, "").ToString();
            }
            else
            {
                return "\nCurrent word/char doesn't exist in the text. Please, try again with another word/char.";
            }

        }
        public int WordCounter()
        {
            Console.WriteLine();
            return TakeWordsCollection(text).Count;
        }
        public void EveryTenthWord()
        {
            MatchCollection collection = TakeWordsCollection(text);
            Console.WriteLine();
            for (int i = 9; i < collection.Count; i+=10)
            {
                Console.Write(collection[i].Value + ", ");
                
            }
        }
        public String ThirdSentenceReverse()
        {
            StringBuilder sentence = new StringBuilder(TakeSentencesCollection(text)[2]);
            
            MatchCollection coll = TakeWordsCollection(sentence.ToString());

            for (int i  = 0; i < coll.Count; i++)
            {
                String word = coll[i].Value;
                sentence = sentence.Replace(word, WordReverse(word));
            }
            return "\n" + sentence.ToString();

        }
        private MatchCollection TakeWordsCollection(String text)
        {
            Regex regex = new Regex(@"[a-z]+'[a-z]+|[a-z]{2,}", RegexOptions.IgnoreCase);  
            return regex.Matches(text);
        }
        private String[] TakeSentencesCollection(String text)
        {
            Regex regex = new Regex(@"[.]+\s+", RegexOptions.IgnoreCase);
            return regex.Split(text);
        }
        private String WordReverse(String word)
        {
            Char[] charArr = word.ToCharArray();
            Array.Reverse<Char>(charArr);
            return new String(charArr);
        }
       
    }
}
