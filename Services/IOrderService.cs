using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderService
    {
        Order GetOrder(int tableID, int status);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
    }
}
