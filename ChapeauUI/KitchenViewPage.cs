using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class KitchenViewPage : Form
    {
        private OrderService orderService;
        private Order order;
        public KitchenViewPage()
        {
            InitializeComponent();
            orderService = new OrderService();
            order = new Order();
        }

        private void KitchenViewPage_Load(object sender, EventArgs e)
        {

        }
        private void FillOrderView()
        {
            //clear the list
            listView1.Items.Clear();
            //get the place orders 
            List<OrderItem> RunningOrders = orderService.GetDrinkOrderItems();

            //double foreach to get the table 
            //foreach (Order O in RunningOrders)
            //{
            //    foreach (OrderItem I in O.items)
            //    {
            //        ListViewItem li = new ListViewItem(I.Order.OrderId.ToString());
            //        li.SubItems.Add(I.MenuItem.Name);
            //        if (I.Comment == "no comment")
            //        {
            //            I.Comment = "";
            //        }
            //        li.SubItems.Add(I.Comment.ToString());
            //        li.SubItems.Add(I.Count.ToString());
            //        li.SubItems.Add(I.OrderTime.ToString("HH:mm"));
            //        li.SubItems.Add(O.Table.TableId.ToString());
            //        li.SubItems.Add(I.Status.ToString());
            //        li.Tag = I;
            //        //if its equal to preparing make it blue
            //        //fill the items in the listview
            //        listView1.Items.Add(li);
            //    }
            //}
        }
    }
}
