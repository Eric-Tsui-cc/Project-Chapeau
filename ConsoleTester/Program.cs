using ChapeauDAL;
using ChapeauModel;
using ChapeauService;

namespace ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {

            KitchenBarService kitchenBarService = new KitchenBarService();
            List<OrderItem> ServedFoodOrders = kitchenBarService.GetAllTodayServedFoodOrderItem();

            foreach (OrderItem order in ServedFoodOrders)
            {
              
                Console.WriteLine(order.Order.Status.ToString());
            }


        }
    }
}