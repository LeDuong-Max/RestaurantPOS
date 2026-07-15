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
        public List<DiningTable> GetAllDiningTable()=>DiningTableDAO.GetAllDiningTable();

        public void UpdateStatus(int tableID, int status)
        {
            DiningTableDAO.UpdateStatus(tableID, status);
        }
    }
}
