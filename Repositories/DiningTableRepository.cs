using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DiningTableRepository : IDiningTableRepository
    {
        public List<DiningTable> GetDiningTables() => DiningTableDAO.GetDiningTables();
        
        public DiningTable GetDiningTableByID(int id) => DiningTableDAO.GetDiningTableByID(id);
        
        public void AddDiningTable(DiningTable table) => DiningTableDAO.AddDiningTable(table);
        
        public void UpdateDiningTable(DiningTable table) => DiningTableDAO.UpdateDiningTable(table);
        
        public void DeleteDiningTable(int id) => DiningTableDAO.DeleteDiningTable(id);
        public List<DiningTable> GetAllDiningTable()=>DiningTableDAO.GetAllDiningTable();

        public void UpdateStatus(int tableID, int status)
        {
            DiningTableDAO.UpdateStatus(tableID, status);
        }
    }
}
