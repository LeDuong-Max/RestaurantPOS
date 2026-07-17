using BusinessObject;
using System.Collections.Generic;
using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDiningTableService
    {
        List<DiningTable> GetDiningTables();
        DiningTable GetDiningTableByID(int id);
        void AddDiningTable(DiningTable table);
        void UpdateDiningTable(DiningTable table);
        void DeleteDiningTable(int id);
        List<DiningTable>? GetAllDiningTable();
        void UpdateStatus(int tableID, int status);
    }
}
