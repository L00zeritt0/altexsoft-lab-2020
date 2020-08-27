using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a food product
    /// </summary>
    public class FoodProduct
    {
        private string name;
        /// <summary>
        /// Preperty Name contains a name of food product (with checking)
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name of food product can't be empty. Please, chech it");
                }
                name = value;
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
}
