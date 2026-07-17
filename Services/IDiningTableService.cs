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
        List<DiningTable>? GetAllDiningTable();
        void UpdateStatus(int tableID, int status);
    }
}
