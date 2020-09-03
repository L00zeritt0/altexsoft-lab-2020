using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a food product
    /// </summary>
    public class FoodProduct
    {
        private string name;
        public int Id { get; set; }
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
        public FoodProduct()
        {
            Id = this.GetHashCode();
        }
        public override string ToString()
        {
            return name;
        }
    }
}
