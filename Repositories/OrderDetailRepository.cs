using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void AddFoodItem(int orderId, FoodItem foodItem)
        {
            OrderDetailDAO.AddFoodItem(orderId,foodItem);
        }

        public decimal CalculateOrderTotal(int orderId)
        {
            return OrderDetailDAO.CalculateOrderTotal(orderId);
        }

        public List<OrderDetail> GetListOrderDetailByOrderId(int orderID)
        {
           return OrderDetailDAO.GetListOrderDetailByOrderId(orderID);
        }
    }
}
