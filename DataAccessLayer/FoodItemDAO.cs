using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FoodItemDAO
    {
        public static List<FoodItem> ShowAllFoodItem()
        {
            using var db = new RestaurantPosContext();
            return db.FoodItems.ToList();
        }
        public static List<FoodItem> FilterFoodIitem(int categoryId)
        {
            using var db = new RestaurantPosContext();
            return db.FoodItems.Where(a=>a.CategoryId == categoryId).ToList();
        }
        public static FoodItem GetFoodById(int foodId)
        {
            using var db = new RestaurantPosContext();
            return db.FoodItems.FirstOrDefault(f => f.FoodId == foodId);
        }
    }
}
