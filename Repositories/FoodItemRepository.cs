using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FoodItemRepository : IFoodItemReposiitory
    {
        public List<FoodItem> FilterFoodItem(int categoryId)
        {
            return FoodItemDAO.FilterFoodIitem(categoryId);
        }
        public FoodItem GetFoodById(int foodID)
        {
            return FoodItemDAO.GetFoodById(foodID);
        }
        public List<FoodItem> ShowAllFoodItem()
        {
            return FoodItemDAO.ShowAllFoodItem();
        }
    }
}
