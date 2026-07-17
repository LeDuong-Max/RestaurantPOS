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
        public static void UpdateStatus(int tableID,int status)
        {
            using var db = new RestaurantPosContext();
            var table = db.DiningTables.SingleOrDefault(a=>a.TableId == tableID);
            if (table != null)
            {
                table.Status = status;
                db.SaveChanges();
            }
        }
    }
}
