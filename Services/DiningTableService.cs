using BusinessObject;
using Repositories;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiningTableService : IDiningTableService
    {
        private IDiningTableRepository repository;

        public DiningTableService()
        {
            repository = new DiningTableRepository();
        }

        public List<DiningTable> GetDiningTables() => repository.GetDiningTables();

        public DiningTable GetDiningTableByID(int id) => repository.GetDiningTableByID(id);

        public void AddDiningTable(DiningTable table) => repository.AddDiningTable(table);

        public void UpdateDiningTable(DiningTable table) => repository.UpdateDiningTable(table);

        public void DeleteDiningTable(int id) => repository.DeleteDiningTable(id);

        public List<DiningTable>? GetAllDiningTable()
        {
            return repository.GetAllDiningTable();
        }

        public void UpdateStatus(int tableID, int status)
        {
            repository.UpdateStatus(tableID, status);
        }
    }
}
