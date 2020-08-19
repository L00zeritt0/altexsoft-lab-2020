using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// CookingStep class describe the step of cooking process
    /// </summary>
    public class CookingStep
    {
        private String cookingStepDescription;
        /// <summary>
        /// Property of cookingStepDescription. Describe the step of cooking process
        /// </summary>
        public String CookingStepDescription
        {
            get
            {
                return cookingStepDescription;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Cooking step can't be empty.");
                }
                cookingStepDescription = value;
            }
        }
        /// <summary>
        /// Cooking step constructor
        /// </summary>
        /// <param name="description"></param>
        public CookingStep() { }
        public CookingStep(String description)
        {
            CookingStepDescription = description;
        }
        public override string ToString()
        {
            return CookingStepDescription;
        }
    }
}
