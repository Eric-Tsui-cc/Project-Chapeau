using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TestMethod
{
    internal class Program
    {
        public OrderService orderService;
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.orderService = new OrderService();
            myProgram.Start();
        }

        public void Start()
        {
            List<Order> orders = orderService.GetOrders();
            Console.WriteLine("All Orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Status: {order.Status}, Employee ID: {order.EmployeeId.EmployeeId}");
            }
        }
    }


}