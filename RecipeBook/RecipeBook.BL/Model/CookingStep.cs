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
        private string description;
        /// <summary>
        /// Prorepry CookigStepDescription contains descriprion of cooking step (with checking)
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Cooking step can't be empty.");
                }
                description = value;
            }
        }
        public override string ToString()
        {
            return description;
        }
    }
}
