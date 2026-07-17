using BusinessObject;
using DataAccessLayer;
using System.Collections.Generic;

namespace Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        public List<FoodItem> GetFoodItems() => FoodItemDAO.GetFoodItems();
        
        public FoodItem GetFoodItemByID(int id) => FoodItemDAO.GetFoodItemByID(id);
        
        public void AddFoodItem(FoodItem foodItem) => FoodItemDAO.AddFoodItem(foodItem);
        
        public void UpdateFoodItem(FoodItem foodItem) => FoodItemDAO.UpdateFoodItem(foodItem);
        
        public void DeleteFoodItem(int id) => FoodItemDAO.DeleteFoodItem(id);
    }
}
