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
        /// <returns></returns>
        List<T> GetAllItems();
        /// <summary>
        /// Meths saves list of items along the given path
        /// </summary>
        /// <param name="list"></param>
        void Save(List<T> list);
    }
}
