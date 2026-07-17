using BusinessObject;
using System.Collections.Generic;
using System.Linq;

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
    }
}
