using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describe the ingredient of a recipe
    /// </summary>
    public class RecipeIngredient
    {
        private int weightOfFoodProduct;
        /// <summary>
        /// FoodProduct object
        /// </summary>
        public FoodProduct FoodProduct { get; set; }
        /// <summary>
        /// Weight of our recipe ingredient
        /// </summary>
        public int WeightOfFoodProduct 
        {
            get 
            {
                return weightOfFoodProduct;
            } 
            set 
            { 
                if (value < 1)
                {
                    throw new ArgumentException("Weight of ingredient have to be greater than zero.");
                }
                weightOfFoodProduct = value;
            } 
        }
        /// <summary>
        /// Constructor of RecipeIngredient class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        public RecipeIngredient() { }
        public RecipeIngredient(String name, int weight)
        {
            FoodProduct = new FoodProduct(name);
            WeightOfFoodProduct = weight;
        }
        public override string ToString()
        {
            return $"{FoodProduct} ({WeightOfFoodProduct} gm)";
        }
    }
}
