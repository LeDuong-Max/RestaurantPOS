using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            using var categories = new RestaurantPosContext();
            return categories.Categories.ToList();
        }

        public static Category GetCategoryByID(int id)
        {
            using var categories = new RestaurantPosContext();
            return categories.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public static void UpdateCategoryByID(Category category)
        {
            using var categories = new RestaurantPosContext();
            categories.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            categories.SaveChanges();
        }

        public static void DeleteCategoryByID(int id)
        {
            using var categories = new RestaurantPosContext();
            var category = categories.Categories.SingleOrDefault(c => c.CategoryId == id);
            categories.Remove(category);
            categories.SaveChanges();
        }

        public static void AddCategory(Category category)
        {
            using var categories = new RestaurantPosContext();
            categories.Categories.Add(category);
            categories.SaveChanges();
        }
        public static List<Category> GetAllCategory()
        {
            using var db = new RestaurantPosContext();
            return db.Categories.ToList();
        }
    }
}
