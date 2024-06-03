using ChapeauDAL;
using ChapeauModel;

namespace ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderDao orderDao = new OrderDao();
            List<Order> orders = orderDao.GetAllOrders();
            foreach (Order order in orders)
            {
                Console.WriteLine(order.OrderId);
            }
        }
    }
}