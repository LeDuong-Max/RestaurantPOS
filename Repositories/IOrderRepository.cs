using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        public Order GetOrder(int tableID, int status);
        public void AddOrder(Order order);
        public void UpdateOrder(Order order);
    }
}
