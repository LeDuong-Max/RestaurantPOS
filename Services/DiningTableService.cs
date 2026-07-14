using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiningTableService : IDiningTableService
    {
        private readonly IDiningTableRepository diningTableRepository;
        public DiningTableService()
        {
            diningTableRepository = new DiningTableRepository();
        }
        public List<DiningTable>? GetAllDiningTable()
        {
            return diningTableRepository.GetAllDiningTable();
        }
    }
}
