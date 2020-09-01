using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    public class RecipeBookSubcategory
    {
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Name of a recipe category (with checking)
        /// </summary>
        public string Name { get; set; }
        [JsonIgnore]
        public int RecipeBookCategoryId { get; set; }
        [JsonIgnore]
        public List<Recipe> ListOfRecipes { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
