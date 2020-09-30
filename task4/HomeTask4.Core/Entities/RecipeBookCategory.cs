using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Class describes a recipe book category.
    /// </summary>
    public class RecipeBookCategory: BaseEntity
    {
        private string name;

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
        public int? ParentId { get; set; }
        public RecipeBookCategory Parent { get; set; }
        public List<RecipeBookCategory> SubCategories { get; set; } = new List<RecipeBookCategory>();
        public List<Recipe> ListOfRecipes { get; set; } = new List<Recipe>();
        public override string ToString()
        {
            return Name;
        }
    }
}
