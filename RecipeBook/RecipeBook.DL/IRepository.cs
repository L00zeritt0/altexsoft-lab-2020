using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RecipeBook.DL
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Method returns list of items along the given path
        /// </summary>
        /// <returns>The list of items from json file</returns>
        IEnumerable<T> GetAllItems();
        /// <summary>
        /// Method adds new T item to our list and save it into json file.
        /// </summary>
        /// <param name="temp">It takes object of class which will be add to the list</param>
        void Create(T temp);
        /// <summary>
        /// Method saves list of items along the given path
        /// </summary>
        void Save();

        
    }
}
