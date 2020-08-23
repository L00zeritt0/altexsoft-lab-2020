using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a recipe book category.
    /// </summary>
    public class RecipeBookCategory
    {
        private string categoryName;
        /// <summary>
        /// Name of a recipe category (with checking)
        /// </summary>
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Category must to have a name.");
                }
                categoryName = value;
            }
        }
        public RecipeBookCategory() { }
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
