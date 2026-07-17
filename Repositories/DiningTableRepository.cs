using BusinessObject;
using DataAccessLayer;
using System.Collections.Generic;

namespace Repositories
{
    public class DiningTableRepository : IDiningTableRepository
    {
        public List<DiningTable> GetDiningTables() => DiningTableDAO.GetDiningTables();
        
        public DiningTable GetDiningTableByID(int id) => DiningTableDAO.GetDiningTableByID(id);
        
        public void AddDiningTable(DiningTable table) => DiningTableDAO.AddDiningTable(table);
        
        public void UpdateDiningTable(DiningTable table) => DiningTableDAO.UpdateDiningTable(table);
        
        public void DeleteDiningTable(int id) => DiningTableDAO.DeleteDiningTable(id);
    }
}
