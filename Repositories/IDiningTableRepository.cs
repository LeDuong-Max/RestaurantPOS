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
        public List<DiningTable> GetAllDiningTable();
        public void UpdateStatus(int tableID, int status);
    }
}
