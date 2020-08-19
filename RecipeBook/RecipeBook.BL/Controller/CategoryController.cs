using RecipeBook.BL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RecipeBook.DL;

namespace RecipeBook.BL.Controller
{
    public class CategoryController
    {
        /// <summary>
        /// Field data connects us with dataLayer
        /// </summary>
        private IRepository<RecipeBookCategory> data; 
        /// <summary>
        /// Property contains list of categories of our recipe book
        /// </summary>
        public List<RecipeBookCategory> ListOfCatigories { get; set; }
        public CategoryController(String path)
        {
            data = new JSONRepository<RecipeBookCategory>(path);
            ListOfCatigories = data.GetAllItems();
        }

    }
}
