using BusinessObject;
using Repositories;
using System.Collections.Generic;

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
    }
}
