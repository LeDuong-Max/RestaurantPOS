using BusinessObject;
using System.Collections.Generic;

namespace Repositories
{
    public interface IDiningTableRepository
    {
        List<DiningTable> GetDiningTables();
        DiningTable GetDiningTableByID(int id);
        void AddDiningTable(DiningTable table);
        void UpdateDiningTable(DiningTable table);
        void DeleteDiningTable(int id);
    }
}
