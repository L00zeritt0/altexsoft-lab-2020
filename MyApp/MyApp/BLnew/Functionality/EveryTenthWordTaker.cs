using MyApp.BLnew.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.BLnew.Functionality
{
    class EveryTenthWordTaker : BaseTextWorker, IEveryTenthWordTakable
    {
        public EveryTenthWordTaker(String path) : base(path) { }
        public String EveryTenthWord()
        {
            List<String> list = new List<String>();
            MatchCollection collection = WordsCollectionMaker.TakeWordsCollection(Text);
            for (int i = 9; i < collection.Count; i += 10)
            {
                list.Add(collection[i].Value);
            }
            return String.Join(", ", list);
        }
    }
}
