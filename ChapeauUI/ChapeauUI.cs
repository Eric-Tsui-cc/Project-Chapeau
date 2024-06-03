using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
using SomerenService;

namespace ChapeauUI

{
    public partial class ChapeauUI : Form
    {

        public ChapeauUI()
        {
            InitializeComponent();
            List<Order> orders = GetOrders();
            label1.Text = orders[0].OrderId.ToString();
            label2.Text = orders[0].TableId.TableId.ToString();

        }
        private List<Order> GetOrders()
        {

            OrderService orderService = new OrderService();
            List<Order> orders = orderService.GetOrders();
            return orders;

        }
    }
}