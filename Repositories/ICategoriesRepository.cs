using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICategoriesRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        void UpdateCategoryByID(Category category);
        void DeleteCategoryByID(int id);
    }
}
