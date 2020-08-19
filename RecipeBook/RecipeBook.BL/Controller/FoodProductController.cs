using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RecipeBook.DL;
using RecipeBook.BL.Controller.Interfaces;

namespace RecipeBook.BL.Controller
{
    public class FoodProductController: IFoodProductControllerInterface<FoodProduct>
    {
        /// <summary>
        /// Field data connects us with dataLayer
        /// </summary>
        private IRepository<FoodProduct> data;
        /// <summary>
        /// Property contains list of FoodProducts which we select to our list of recipe ingredients
        /// </summary>
        public List<FoodProduct> ListOfFoods { get; set; }
        public FoodProductController(String path) 
        {
            data = new JSONRepository<FoodProduct>(path);
            ListOfFoods = data.GetAllItems();
            if (ListOfFoods == null)
            {
                ListOfFoods = new List<FoodProduct>();
            }
        }
        /// <summary>
        /// Method adds some foodProduct in our list, if somebody couldn't find one there.
        /// </summary>
        /// <param name="name"></param>
        public void AddFoodProduct(String name)
        {
            ListOfFoods.Add(new FoodProduct(name));
            data.Save(ListOfFoods);
        }
    }
}
