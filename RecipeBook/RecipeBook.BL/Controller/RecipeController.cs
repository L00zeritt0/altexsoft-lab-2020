using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections;
using RecipeBook.DL;
using RecipeBook.BL.Controller.Interfaces;

namespace RecipeBook.BL.Controller
{
    public class RecipeController: IRecipeControllerInterface<Recipe>
    {
        /// <summary>
        /// Field data connects us with dataLayer
        /// </summary>
        private IRepository<Recipe> data;
        /// <summary>
        /// Property contains list of recipes of our recipe book
        /// </summary>
        public List<Recipe> ListOfRecipes { get; set; }        
        public RecipeController(String path)
        {
            data = new JSONRepository<Recipe>(path);
            ListOfRecipes = data.GetAllItems();
            if(ListOfRecipes == null)
            {
                ListOfRecipes = new List<Recipe>();
            }
        }
        /// <summary>
        /// Method adds new recipe in our recipe list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        public void AddRecipe(String name, String category, List<RecipeIngredient> ingredients, List<CookingStep> steps)
        {
            ListOfRecipes.Add(new Recipe(name, category, ingredients, steps));
            data.Save(ListOfRecipes);
        }
        /// <summary>
        /// Method gets list of recipes by category which we choose
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Recipe> BackRecipeListByCategory(RecipeBookCategory category)
        {
            return (from r in ListOfRecipes
                    where r.RecipeCategory.CategoryName.Equals(category.CategoryName)
                    select r).ToList<Recipe>();
        }
    }
}
