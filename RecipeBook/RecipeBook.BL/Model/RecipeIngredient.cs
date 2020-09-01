using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes the ingredient of a recipe
    /// </summary>
    public class RecipeIngredient
    {
        private string quantityOfFoodProduct;
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int FoodProductId { get; set; }
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Quantity of a recipe ingredient can't be empty.");
                }
                quantityOfFoodProduct = value;
            } 
        }
        public override string ToString()
        {
            return $"{FoodProduct} ({QuantityOfFoodProduct})";
        }
    }
}
