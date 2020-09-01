using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describes a step of cooking process
    /// </summary>
    public class CookingStep
    {
        private string description;
        [JsonIgnore]
        public int Id { get; set; }
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
        [JsonIgnore]
        public int RecipeId { get; set; }
        public override string ToString()
        {
            return description;
        }
    }
}
