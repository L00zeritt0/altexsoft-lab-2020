using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// class describes a food product
    /// </summary>
    public class FoodProduct
    {
        private string name;
        /// <summary>
        /// name of food product (with checking)
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name of food product can't be empty. Please, chech it");
                }
                name = value;
            }
        }
        public FoodProduct() { }
        public override string ToString()
        {
            return name;
        }
    }
}
