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
        public int Id { get; set; }
        public int ParentId { get; set; }
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
        [JsonIgnore]
        public List<RecipeBookCategory> ListOfSubcategories { get; set; } = new List<RecipeBookCategory>();
        public override string ToString()
        {
            return Name;
        }
    }
}
