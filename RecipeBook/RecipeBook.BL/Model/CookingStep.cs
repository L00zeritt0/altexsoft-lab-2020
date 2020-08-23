using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a step of cooking process
    /// </summary>
    public class CookingStep
    {
        private string cookingStepDescription;
        /// <summary>
        /// Description of cooking step (with checking)
        /// </summary>
        public string CookingStepDescription
        {
            get
            {
                return cookingStepDescription;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Cooking step can't be empty.");
                }
                cookingStepDescription = value;
            }
        }
        public CookingStep() { }
        public override string ToString()
        {
            return cookingStepDescription;
        }
    }
}
