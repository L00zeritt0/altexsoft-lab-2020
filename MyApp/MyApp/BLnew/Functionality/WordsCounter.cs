using MyApp.BLnew.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.BLnew.Functionality
{
    class WordsCounter: BaseTextWorker, IWordsCountable
    {
        public WordsCounter(String path) : base(path) { }
        public int WordCounter()
        {
            return WordsCollectionMaker.TakeWordsCollection(Text).Count;
        }
        
    }
}
