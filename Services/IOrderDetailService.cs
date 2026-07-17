using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetListOrderDetailByOrderId(int orderID);
        void AddFoodItem(int orderId, FoodItem foodItem);
        List<OrderDetailDTO> GetOrderDetailsDisplay(int orderId);
        decimal CalculateOrderTotal(int orderId);

    }
}
