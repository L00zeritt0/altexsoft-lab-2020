using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Model
{
    /// <summary>
    /// Class describe a recipe
    /// </summary>
    public class Recipe
    {
        private String recipeName;
        /// <summary>
        /// Name of a current recipe with checking.
        /// </summary>
        public String RecipeName 
        {
            get
            {
                return recipeName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Recipe name can't be empty.");
                }
                recipeName = value;
            }
        }
        /// <summary>
        /// Category of a current recipe
        /// </summary>
        public RecipeBookCategory RecipeCategory { get; set; }
        /// <summary>
        /// Current recipe's ingredients list.
        /// </summary>
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        /// <summary>
        /// Current recipe's cooking steps list
        /// </summary>
        public List<CookingStep> RecipeSteps { get; set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryName"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        public Recipe() { }
        public Recipe(String name,
                      String categoryName,
                      List<RecipeIngredient> ingredients,
                      List<CookingStep> steps)
        {
            RecipeName = name;
            RecipeCategory = new RecipeBookCategory(categoryName);
            RecipeIngredients = ingredients;
            RecipeSteps = steps;
        }

        public override string ToString()
        {
            return RecipeName;
        }
    }
}
