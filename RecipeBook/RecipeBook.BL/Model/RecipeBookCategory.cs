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
        //public RecipeBookCategory() { }
        public override string ToString()
        {
            return Name;
        }
    }
}
