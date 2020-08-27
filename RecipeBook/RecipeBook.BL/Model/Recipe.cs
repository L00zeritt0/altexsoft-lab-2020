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
        private string name;
        /// <summary>
        /// Name of a current recipe (with checking).
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
                    throw new ArgumentException("Recipe name can't be empty.");
                }
                name = value;
            }
        }
        /// <summary>
        /// Category of a recipe
        /// </summary>
        public RecipeBookCategory Category { get; set; }
        /// <summary>
        /// Current recipe ingredients list.
        /// </summary>
        public List<RecipeIngredient> Ingredients { get; set; }
        /// <summary>
        /// Cooking step list of a recipe
        /// </summary>
        public List<CookingStep> Steps { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
