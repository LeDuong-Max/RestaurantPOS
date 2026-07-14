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
        public static List<Category> GetAllCategory()
        {
            using var db = new RestaurantPosContext();
            return db.Categories.ToList();
        }
    }
}
