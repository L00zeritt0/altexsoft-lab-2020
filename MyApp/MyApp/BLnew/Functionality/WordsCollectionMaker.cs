using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.BLnew.Functionality
{
    class WordsCollectionMaker
    {
        public static MatchCollection TakeWordsCollection(String text)
        {
            Regex regex = new Regex(@"[a-z]+'[a-z]+|[a-z]{2,}", RegexOptions.IgnoreCase);
            return regex.Matches(text);
        }
    }
}
