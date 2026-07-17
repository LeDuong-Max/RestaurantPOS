using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository orderRepository;
        public OrderService()
        {
            orderRepository = new OrderRepository();
        }
        public void AddOrder(Order order)
        {
            orderRepository.AddOrder(order);
        }

        public Order GetOrder(int tableID, int status)
        {
            return orderRepository.GetOrder(tableID, status);
        }

        public void UpdateOrder(Order order)
        {
            orderRepository.UpdateOrder(order);
        }
    }
}
