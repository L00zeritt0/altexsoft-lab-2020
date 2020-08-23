using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes the ingredient of a recipe
    /// </summary>
    public class RecipeIngredient
    {
        private string quantityOfFoodProduct;
        /// <summary>
        /// Food product of our recipe ingredient
        /// </summary>
        public FoodProduct FoodProduct { get; set; }
        /// <summary>
        /// Quantitey of our recipe ingredient
        /// </summary>
        public string QuantityOfFoodProduct 
        {
            get 
            {
                return quantityOfFoodProduct;
            } 
            set 
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Quantity of a recipe ingredient can't be empty.");
                }
                quantityOfFoodProduct = value;
            } 
        }
        public RecipeIngredient() { }
        public override string ToString()
        {
            return $"{FoodProduct} ({QuantityOfFoodProduct})";
        }
    }
}
