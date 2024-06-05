using ChapeauDAL;
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
    public partial class Orders : Form
    {
        private OrderService_Overview orderService = new OrderService_Overview();
        private List<Order> orders = new List<Order>();

        public Orders()
        {
            InitializeComponent();
            ConfigureTableListView();
            LoadOrderOverview();
        }


        private void ConfigureTableListView()
        {
            this.TableOrdersListView.View = View.Details;
            this.TableOrdersListView.FullRowSelect = true;
            this.TableOrdersListView.GridLines = true;

            // Clear any existing columns
            this.TableOrdersListView.Columns.Clear();

            // Add columns to the ListView in the desired order
            this.TableOrdersListView.Columns.Add("Item Name", 100, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Table ID", 80, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Order ID", 80, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Wait", 100, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Order Status", 100, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Comments", 100, HorizontalAlignment.Left);
            this.TableOrdersListView.Columns.Add("Count", 70, HorizontalAlignment.Left);
        }

        private void LoadOrderOverview()
        {
            orders = new OrderDao().GetAllOrders();
            DisplayOrders(orders);
        }

        private void DisplayOrders(List<Order> ordersToDisplay)
        {
            this.TableOrdersListView.Items.Clear();

            foreach (var order in ordersToDisplay)
            {

                ListViewItem item = new ListViewItem(order.MenuItemId.ToString());
                item.SubItems.Add(order.TableId.ToString());
                item.SubItems.Add(order.OrderId.ToString());
                item.SubItems.Add(order.GetWaitingTime().ToString(@"hh\:mm\:ss"));
                if (order.Status == Order.OrderStatus.Done)
                {
                    item.BackColor = Color.SeaGreen;
                    item.ForeColor = Color.FloralWhite;
                    item.SubItems.Add(Order.StatusToString(order.Status));

                }
                else
                {
                    item.SubItems.Add(Order.StatusToString(order.Status));

                }
                item.SubItems.Add(order.Comments);
                item.SubItems.Add(order.Count.ToString());

                item.Tag = order;
                this.TableOrdersListView.Items.Add(item);
            }
        }


        private void SortAndDisplayOrders(string sortBy)
        {
            List<Order> sortedOrders = this.orderService.SortOrders(this.orders, sortBy);
            DisplayOrders(sortedOrders);
        }




        //BUTTONS
        private void OpenUI(Form newForm)
        {
            // define active form (LoginUI) and hide it
            Form activeForm = ActiveForm;
            activeForm.Hide();

            // show new form, which needs to be open
            newForm.ShowDialog();

            // close previous form (LoginPage), so it's not running in the background
            activeForm.Close();
        }

        private void roundedButton2_Click_1(object sender, EventArgs e)
        {
            ChapeauUI newForm = new ChapeauUI();

            OpenUI(newForm);
        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {
            Tables newForm = new Tables();

            OpenUI(newForm);
        }

        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            LoginPage newForm = new LoginPage();
            OpenUI(newForm);

        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            string selectedSort = this.comboBoxStatus.SelectedItem.ToString();

            switch (selectedSort)
            {
                case "Ready to Serve":
                    SortAndDisplayOrders("ReadyToServe");
                    break;
                case "Preparing":
                    SortAndDisplayOrders("Preparing");
                    break;
                case "Longest Waiting Time":
                    SortAndDisplayOrders("LongestWaitingTime");
                    break;
                case "Table ID Ascending":
                    SortAndDisplayOrders("TableIdAsc");
                    break;
                case "Table ID Descending":
                    SortAndDisplayOrders("TableIdDesc");
                    break;
                case "All":
                    SortAndDisplayOrders("All");
                    break;
                default:
                    break;
            }
        }
        private void RefreshOrdersListView()
        {
            List<Order> orders = orderService.GetAllOrders();
            TableOrdersListView.Items.Clear();

            foreach (var order in orders)
            {
                ListViewItem item = new ListViewItem(order.OrderId.ToString());
                item.SubItems.Add(order.EmployeeId.ToString());
                item.SubItems.Add(Order.StatusToString(order.Status));
                item.SubItems.Add(order.OrderTakenTime.ToString());
                item.SubItems.Add(order.Comments);
                item.SubItems.Add(order.TableId.ToString());
                item.SubItems.Add(order.BillId.ToString());
                item.SubItems.Add(order.Count.ToString());
                item.SubItems.Add(order.MenuItemId.ToString());

                TableOrdersListView.Items.Add(item);
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (TableOrdersListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = TableOrdersListView.SelectedItems[0];
                int orderId = int.Parse(selectedItem.SubItems[2].Text);

                if (comboBox1.SelectedItem != null)
                {
                    string newStatus = comboBox1.SelectedItem.ToString();

                    try
                    {
                        Order order = orderService.GetOrderById(orderId);
                        if (order != null)
                        {
                            order.Status = Order.StringToStatus(newStatus);
                            orderService.ChangeOrderStatus(order);
                            RefreshOrdersListView();
                        }
                        else
                        {
                            MessageBox.Show("Order not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error changing order status: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a status.");
                }
            }
            else
            {
                MessageBox.Show("Please select an order to change the status.");
            }
        }

    }
}
