using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Food product class
    /// </summary>
    public class FoodProduct
    {
        private String name;
        /// <summary>
        /// Property of name with checking
        /// </summary>
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name of food product can't be empty. Please, chech it");
                }
                name = value;
            }
        }
        /// <summary>
        /// FoodProduct class constructor
        /// </summary>
        /// <param name="name"></param>
        public FoodProduct() { }
        public FoodProduct(String name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
