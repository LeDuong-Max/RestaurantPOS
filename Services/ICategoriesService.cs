using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoriesService
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        void UpdateCategoryByID(Category category);
        void DeleteCategoryByID(int id);
    }
}
