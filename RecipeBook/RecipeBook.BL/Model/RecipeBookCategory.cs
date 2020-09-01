using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a recipe book category.
    /// </summary>
    public class RecipeBookCategory
    {
        private string name;
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Name of a recipe category (with checking)
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
                    throw new ArgumentException("Category must to have a name.");
                }
                name = value;
            }
        }
        public List<RecipeBookSubcategory> ListOfSubcategories { get; set; } = new List<RecipeBookSubcategory>();
        public override string ToString()
        {
            return Name;
        }
    }
}
