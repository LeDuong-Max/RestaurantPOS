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
        public static List<DiningTable> GetDiningTables()
        {
            using var context = new RestaurantPosContext();
            return context.DiningTables.ToList();
        }

        public static DiningTable GetDiningTableByID(int id)
        {
            using var context = new RestaurantPosContext();
            return context.DiningTables.FirstOrDefault(t => t.TableId == id);
        }

        public static void AddDiningTable(DiningTable table)
        {
            using var context = new RestaurantPosContext();
            context.DiningTables.Add(table);
            context.SaveChanges();
        }

        public static void UpdateDiningTable(DiningTable table)
        {
            using var context = new RestaurantPosContext();
            var existingTable = context.DiningTables.FirstOrDefault(t => t.TableId == table.TableId);
            if (existingTable != null)
            {
                existingTable.TableName = table.TableName;
                existingTable.Status = table.Status;
                context.SaveChanges();
            }
        }

        public static void DeleteDiningTable(int id)
        {
            using var context = new RestaurantPosContext();
            var table = context.DiningTables.FirstOrDefault(t => t.TableId == id);
            if (table != null)
            {
                context.DiningTables.Remove(table);
                context.SaveChanges();
            }
        }
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
