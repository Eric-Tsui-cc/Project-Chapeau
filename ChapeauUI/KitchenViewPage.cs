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
        private KitchenBarService kitchenBarService;
        private Order order;
        public KitchenViewPage()
        {
            InitializeComponent();
            kitchenBarService = new KitchenBarService();
            order = new Order();
        }

        private void KitchenViewPage_Load(object sender, EventArgs e)
        {
            listView1.Show();
            listViewFinishOrders.Hide();
            BackButton.Hide();
            FillRunningOrderView();
            FillServedOrderView();


        }
        private void FillRunningOrderView()
        {
            //clear the list

            listView1.Items.Clear();
            //get the place orders 
            List<OrderItem> RunningFoodOrders = kitchenBarService.GetAllRunningFoodOrderItem();

            foreach (OrderItem I in RunningFoodOrders)
            {
                TimeSpan difference = DateTime.Now - I.OrderTime;
                double minutesDifference = difference.TotalMinutes;
                int waittime = (int)difference.TotalMinutes;
                string time = $" {waittime}mins";
                Table table = kitchenBarService.GetTableByOrderId(I.Order.OrderId);
                ListViewItem li = new ListViewItem(I.Order.OrderId.ToString());
                li.SubItems.Add(I.MenuItem.Name);
                if (I.Comment == null)
                {
                    I.Comment = "";
                }
                li.SubItems.Add(I.Count.ToString());
                li.SubItems.Add(table.TableId.ToString());
                li.SubItems.Add(I.Comment.ToString());
                li.SubItems.Add(time);
                li.SubItems.Add(I.Order.Status.ToString());
                li.Tag = I;
                //if its equal to preparing make it blue
                //fill the items in the listview
                listView1.Items.Add(li);
            }
        }
        private void FillServedOrderView()
        {
            List<OrderItem> ServedFoodOrders = kitchenBarService.GetAllTodayServedFoodOrderItem();
            foreach (OrderItem I in ServedFoodOrders)
            {
                
                Table table = kitchenBarService.GetTableByOrderId(I.Order.OrderId);
                ListViewItem li = new ListViewItem(I.Order.OrderId.ToString());
                li.SubItems.Add(I.MenuItem.Name);
                if (I.Comment == null)
                {
                    I.Comment = "";
                }
                li.SubItems.Add(I.Count.ToString());
                li.SubItems.Add(table.TableId.ToString());
                li.SubItems.Add(I.Comment.ToString());
                li.SubItems.Add(I.OrderTime.ToString("HH:MM"));
                li.SubItems.Add(I.Order.Status.ToString());
                li.Tag = I;
                //if its equal to preparing make it blue
                //fill the items in the listview
                listViewFinishOrders.Items.Add(li);
            }
        }

        private void Switch_Click(object sender, EventArgs e)
        {   
            listView1.Hide();
            listViewFinishOrders.Show();
            FillServedOrderView();
            PrepareButton.Hide();
            ReadyButton.Hide();
            Switch.Hide();
            BackButton.Show();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            listView1.Show();
            listViewFinishOrders.Hide();
            FillRunningOrderView();
        }
    }
}
