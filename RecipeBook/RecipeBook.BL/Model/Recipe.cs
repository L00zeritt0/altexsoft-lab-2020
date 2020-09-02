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

        public int RecipeBookCategoryId { get; set; }
        /// <summary>
        /// Category of a recipe
        /// </summary>
        [JsonIgnore]
        public RecipeBookCategory Category { get; set; }
        
        public int RecipeBookSubcategoryId { get; set; }
        [JsonIgnore]
        public RecipeBookCategory SubCategory { get; set; }
        /// <summary>
        /// Current recipe ingredients list.
        /// </summary>
        public List<RecipeIngredient> Ingredients { get; set; }
        /// <summary>
        /// Cooking step list of a recipe
        /// </summary>
        public List<CookingStep> Steps { get; set; }
        public Recipe()
        {
            Id = this.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
