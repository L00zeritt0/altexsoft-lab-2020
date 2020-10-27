using System;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Class describes the ingredient of a recipe
    /// </summary>
    public class RecipeIngredient: BaseEntity
    {
        private string quantityOfFoodProduct;
        public int FoodProductId { get; set; }
        /// <summary>
        /// Food product of our recipe ingredient
        /// </summary>
        public FoodProduct FoodProduct { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
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
