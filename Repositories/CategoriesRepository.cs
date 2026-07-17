using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public void AddCategory(Category category)
        {
            CategoryDAO.AddCategory(category);
        }

        public void DeleteCategoryByID(int id)
        {
            CategoryDAO.DeleteCategoryByID(id);
        }

        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Category GetCategory(int id)
        {
            return CategoryDAO.GetCategoryByID(id);
        }

        public void UpdateCategoryByID(Category category)
        {
            CategoryDAO.UpdateCategoryByID(category);
        }

    }
}
