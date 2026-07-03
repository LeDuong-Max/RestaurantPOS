using BusinessObject;
using Services;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository categories = new CategoriesRepository();
        public void DeleteCategoryByID(int id)
        {
            categories.DeleteCategoryByID(id);
        }

        public List<Category> GetCategories()
        {
            return categories.GetCategories();
        }

        public Category GetCategory(int id)
        {
            return categories.GetCategory(id);
        }

        public void UpdateCategoryByID(Category category)
        {
            categories.UpdateCategoryByID(category);
        }
    }
}
