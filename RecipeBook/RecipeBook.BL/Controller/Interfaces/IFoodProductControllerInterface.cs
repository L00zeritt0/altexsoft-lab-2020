using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.BL.Controller.Interfaces
{
    public interface IFoodProductControllerInterface<T>
    {
        List<T> ListOfFoods { get; set; }
        void AddFoodProduct(String name);
    }
}
