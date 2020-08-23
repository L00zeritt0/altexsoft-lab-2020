using RecipeBook.DL;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Controller
{
    public class Controller<T>: IController<T>
    {
        /// <summary>
        /// Field connect us with our data layer
        /// </summary>
        private IRepository<T> data;
        public Controller(string path)
        {
            data = new JsonRepository<T>(path);
        }
        /// <summary>
        /// Get the list of items from repository
        /// </summary>
        /// <returns>The list of items is class T</returns>
        public List<T> GetAllItems()
        {
            var list = (List<T>)data.GetAllItems();
            if (list == null)
            {
                return new List<T>();
            }
            return list;
        }
        /// <summary>
        /// Method add new item of class T in our list of items
        /// </summary>
        /// <param name="item">An Item of class T which will be adds to the list of items</param>
        public void AddItem(T item)
        {
            data.Create(item);
        }
    }
}
