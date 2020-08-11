using MyApp.BLnew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.BLnew.Functionality
{
    class ThirdSentenceReverser: BaseTextWorker, IThirdSentenceReversible
    {
        private String sentence;
        public ThirdSentenceReverser(String path) : base(path) { }
        public String ThirdSentenceReverse()
        {
            sentence = TakeSentencesCollection(Text)[2];
            MatchCollection coll = WordsCollectionMaker.TakeWordsCollection(sentence);

            for (int i = 0; i < coll.Count; i++)
            {
                String word = coll[i].Value;
                sentence = sentence.Replace(word, new string(word.ToCharArray().Reverse().ToArray()));
            }
            return sentence;
        }
        private String[] TakeSentencesCollection(String text)
        {
            Regex regex = new Regex(@"[.]+\s+", RegexOptions.IgnoreCase);
            return regex.Split(text);
        }
    }
}
