using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetListOrderDetailByOrderId(int orderID)
        {
            using var db = new RestaurantPosContext();
            return db.OrderDetails.Where(od => od.OrderId == orderID).ToList();
        }

        public static void AddFoodItem(int orderId, FoodItem foodItem)
        {
            using var db = new RestaurantPosContext();
            var existingDetail = db.OrderDetails
                .FirstOrDefault(od => od.OrderId == orderId && od.FoodId == foodItem.FoodId);

            if (existingDetail != null)
            {
                existingDetail.Quantity += 1;
            }
            else
            {
                var newDetail = new OrderDetail
                {
                    OrderId = orderId,
                    FoodId = foodItem.FoodId,
                    Quantity = 1,
                    UnitPrice = foodItem.Price
                };
                db.OrderDetails.Add(newDetail);
            }

            db.SaveChanges();
        }
        public static decimal CalculateOrderTotal(int orderId)
        {
            using var db = new RestaurantPosContext();
            var details = GetListOrderDetailByOrderId(orderId);
            decimal total = details.Sum(d => d.Quantity * d.UnitPrice);
            return total;
        }
    }
}
