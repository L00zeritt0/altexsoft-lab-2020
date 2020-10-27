using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Class describes a recipe
    /// </summary>
    public class Recipe: BaseEntity
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
        public int RecipeBookCategoryId { get; set; }
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
