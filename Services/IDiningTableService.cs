using BusinessObject;
using System.Collections.Generic;

namespace Services
{
    public interface IDiningTableService
    {
        List<DiningTable> GetDiningTables();
        DiningTable GetDiningTableByID(int id);
        void AddDiningTable(DiningTable table);
        void UpdateDiningTable(DiningTable table);
        void DeleteDiningTable(int id);
    }
}
