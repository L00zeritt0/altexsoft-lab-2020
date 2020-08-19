using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Controller.Interfaces
{
    public interface IRecipeControllerInterface<T>
    {
        List<T> ListOfRecipes { get; set; }
        void AddRecipe(String name, String category, List<RecipeIngredient> ingredients, List<CookingStep> steps);
        List<T> BackRecipeListByCategory(RecipeBookCategory category);
    }
}
