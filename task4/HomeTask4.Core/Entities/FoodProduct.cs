using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Class describes a food product
    /// </summary>
    public class FoodProduct: BaseEntity
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
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
