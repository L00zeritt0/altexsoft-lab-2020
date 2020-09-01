using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a recipe
    /// </summary>
    public class Recipe
    {
        private string name;
        [JsonIgnore]
        public int Id { get; set; }
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
        [JsonIgnore]
        public int RecipeBookCategoryId { get; set; }
        /// <summary>
        /// Category of a recipe
        /// </summary>
        public RecipeBookCategory Category { get; set; }
        [JsonIgnore]
        public int RecipeBookSubcategoryId { get; set; }
        public RecipeBookSubcategory SubCategory { get; set; }
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
