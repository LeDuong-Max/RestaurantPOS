using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly FoodItemRepository foodItemRepository;
        public FoodItemService()
        {
            foodItemRepository = new FoodItemRepository();
        }

        public List<FoodItem> FilterFoodIitem(int categoryId)
        {
            return foodItemRepository.FilterFoodItem(categoryId);
        }

        public List<FoodItem> ShowAllFoodItem()
        {
            return foodItemRepository.ShowAllFoodItem();
        }
    }
}
