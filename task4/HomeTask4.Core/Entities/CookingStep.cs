using System;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Class describes a step of cooking process
    /// </summary>
    public class CookingStep: BaseEntity
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
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public override string ToString()
        {
            return description;
        }
    }
}
