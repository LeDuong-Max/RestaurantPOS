using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IDiningTableRepository
    {
        List<DiningTable> GetDiningTables();
        DiningTable GetDiningTableByID(int id);
        void AddDiningTable(DiningTable table);
        void UpdateDiningTable(DiningTable table);
        void DeleteDiningTable(int id);
        public List<DiningTable> GetAllDiningTable();
        public void UpdateStatus(int tableID, int status);
    }
}
