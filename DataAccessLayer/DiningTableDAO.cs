using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DiningTableDAO
    {
        public static List<DiningTable> GetAllDiningTable()
        {
            using var db = new RestaurantPosContext();
            return db.DiningTables.ToList();
        }
    }
}
