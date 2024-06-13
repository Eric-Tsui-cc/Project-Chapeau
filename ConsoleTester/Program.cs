using ChapeauDAL;
using ChapeauModel;
using ChapeauService;

namespace ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {

            OverviewService overviewService = new OverviewService();
            int tableid = 4;
            List<int> orderids = overviewService.GetRunningOrderIdsByTableId(tableid);
            foreach(int orderid in orderids)
            {
                Order order = overviewService.GetOrderById(orderid);
                Console.WriteLine(order.OrderId);
            }

            
        }
    }
}