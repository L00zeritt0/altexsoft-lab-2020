using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Controller
{
    public interface IController<T>
    {
        /// <summary>
        /// Get the list of items from repository
        /// </summary>
        /// <returns>The list of items is class T</returns>
        IEnumerable<T> GetAllItems();
        /// <summary>
        /// Method add new item of class T in our list of items
        /// </summary>
        /// <param name="item">An Item of class T which will be adds to the list of items</param>
        void AddItem(T item);
    }
}
