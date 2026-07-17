using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            OrderDAO.AddOrder(order);
        }

        public Order GetOrder(int tableID, int status)
        {
            return OrderDAO.GetOrder(tableID, status);
        }

        public void UpdateOrder(Order order)
        {
            OrderDAO.UpdateOrder(order);
        }
    }
}
