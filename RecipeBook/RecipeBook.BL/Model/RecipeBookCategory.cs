using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describe recipe book category/
    /// </summary>
    public class RecipeBookCategory
    {
        private String categoryName;
        /// <summary>
        /// Property of a category name with checking.
        /// </summary>
        public String CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Category must to have a name.");
                }
                categoryName = value;
            }
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="name"></param>
        public RecipeBookCategory() { }
        public RecipeBookCategory(String name)
        {
            CategoryName = name;
        }
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
