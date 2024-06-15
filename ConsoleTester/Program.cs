using ChapeauDAL;
using ChapeauModel;
using ChapeauService;

namespace ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {

            OrderService orderService = new OrderService();
            PaymentService paymentService = new PaymentService();
            List<OrderItem> orderItems = orderService.GetDrinkOrderItems();
            int table = 4;
            List<Order>orders = paymentService.GetUnpaidOrdersByTableId(10);
            foreach (Order order in orders)
            {
                Console.WriteLine(order.OrderId);
            }


        }
    }
}