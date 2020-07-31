using MyApp.BL;
using System;
using System.IO;

namespace MyApp.Manager
{
    class TaksManager
    {
        private ITextEditable textEdit;
        private ITextReadable textReader;
        private IDirInformer dirInformer;
        public void DeleteFromTheText(String path, String word)
        {
            Initializer(path);
            textEdit.Word = word;
            Console.WriteLine("\n" + textEdit.EditText());
        }
        public void EveryTenthAndCount(String path)
        {
            Initializer(path);
            Console.WriteLine(textEdit.WordCounter());
            textEdit.EveryTenthWord();
        }
        public void TheThirdSentence(String path)
        {
            Initializer(path);
            Console.WriteLine(textEdit.ThirdSentenceReverse());
        }
        public void WorkWithDir(String path)
        {
            dirInformer = new DirInformer(path);
            dirInformer.ShowCurrentDirContent();
            Console.WriteLine("\nPut the nunber of dir you need:");
            int i = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            dirInformer.ChoseElseDir(i);
        }
        private void Initializer(String path)
        {
            textReader = new TextFileReader(path);
            textEdit = new TextEditer(textReader.ReadText());
        }
    }
}
