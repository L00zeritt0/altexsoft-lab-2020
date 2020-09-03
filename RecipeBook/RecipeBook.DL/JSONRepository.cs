using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RecipeBook.DL
{

    public class JsonRepository<T>: IRepository<T>
    {
        /// <summary>
        /// Field path contains the path to our local file
        /// </summary>
        private string path;
        public JsonRepository(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The path is empty or wrong.");
            }
            this.path = path;
        }
        /// <summary>
        /// Method returns list of items along the given path
        /// </summary>
        /// <returns>The list of items from json file</returns>
        public IEnumerable<T> GetAllItems()
        { 
            return JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(path));
        }
        /// <summary>
        /// Method adds new T item to our list and save it into json file.
        /// </summary>
        /// <param name="item">It takes object of class which will be add to the list</param>
        public void Create(T item)
        {
            List<T> listOfItems = (List<T>)GetAllItems();
            if(listOfItems == null)
            {
                listOfItems = new List<T>();
            }
            listOfItems.Add(item);
            Save(listOfItems);
        }
        /// <summary>
        /// Method saves list of items along the given path
        /// </summary>
        /// <param name="list">The list of items we should to write to *.json</param>
        public void Save(IEnumerable<T> list)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(list));
        }
    }
    
}
