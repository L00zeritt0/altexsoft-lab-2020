using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a recipe
    /// </summary>
    public class Recipe
    {
        private string recipeName;
        /// <summary>
        /// Name of a current recipe (with checking).
        /// </summary>
        public string RecipeName 
        {
            get
            {
                return recipeName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Recipe name can't be empty.");
                }
                recipeName = value;
            }
        }
        /// <summary>
        /// Category of a recipe
        /// </summary>
        public RecipeBookCategory RecipeCategory { get; set; }
        /// <summary>
        /// Current recipe ingredients list.
        /// </summary>
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        /// <summary>
        /// Cooking step list of a recipe
        /// </summary>
        public List<CookingStep> RecipeSteps { get; set; }
        public Recipe() { }
        public override string ToString()
        {
            return RecipeName;
        }
    }
}
