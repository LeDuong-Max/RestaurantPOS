using BusinessObject;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class FoodItemService : IFoodItemService
    {
        private IFoodItemRepository repository;

        public FoodItemService()
        {
            repository = new FoodItemRepository();
        }

        public List<FoodItem> GetFoodItems() => repository.GetFoodItems();

        public FoodItem GetFoodItemByID(int id) => repository.GetFoodItemByID(id);

        public void AddFoodItem(FoodItem foodItem) => repository.AddFoodItem(foodItem);

        public void UpdateFoodItem(FoodItem foodItem) => repository.UpdateFoodItem(foodItem);

        public void DeleteFoodItem(int id) => repository.DeleteFoodItem(id);
    }
}
