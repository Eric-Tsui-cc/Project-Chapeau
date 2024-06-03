using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class OrderService
    {
        private OrderDao orderdb;

        public OrderService()
        {
            orderdb = new OrderDao();
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = orderdb.GetAllOrders();
            return orders;
        }
        public void AddOrder(Order order)
        {
            orderdb.CreateOrder(order);
        }
        //public void UpdateStudent(Student student)
        //{
        //    studentdb.UpdateStudent(student);

        //}
        //public void DeleteOrder(Order order)
        //{
        //    orderdb.(order);

        //}
        public Order GetByOrderId(int id)
        {
            Order order = orderdb.GetOrderById(id);
            return order;
        }

    }
}
