using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace RecipeBook.DL
{

    public class JSONRepository<T>: IRepository<T>
    {
        /// <summary>
        /// Field path contains the path to our local file
        /// </summary>
        private String path;
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                if (!File.Exists(value))
                {
                    throw new ArgumentNullException("The path is empty or wrong.");
                }
                path = value;
            }
        }

        public JSONRepository(String path)
        {
            Path = path;
        }
        /// <summary>
        /// Method returns list of items along the given path
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllItems()
        {
            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(Path));
        }
        /// <summary>
        /// Meths saves list of items along the given path
        /// </summary>
        /// <param name="list"></param>
        public void Save(List<T> list)
        {
            File.WriteAllText(Path, JsonSerializer.Serialize<List<T>>(list));
        }
    }
    
}
