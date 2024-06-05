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
        TableService_Bogdan tableService = new TableService_Bogdan();

        public Orders()
        {
            InitializeComponent();
            ConfigureTableListView();
            /*            LoadOrderOverview();
            */
        }


        private void ConfigureTableListView()
        {
            TableOrdersListView.View = View.Details;
            TableOrdersListView.Columns.Add("Table ID", 100, HorizontalAlignment.Left);
            TableOrdersListView.Columns.Add("Status", 100, HorizontalAlignment.Left);
            TableOrdersListView.Columns.Add("Capacity", 100, HorizontalAlignment.Left);
            TableOrdersListView.Columns.Add("Order Item", 100, HorizontalAlignment.Left);
            TableOrdersListView.Columns.Add("Wait", 100, HorizontalAlignment.Left);
            TableOrdersListView.Columns.Add("Order State", 100, HorizontalAlignment.Left);
        }
        /* private void LoadOrderOverview()
         {
             try
             {
                 List<Table> tables = tableService.GetAllTables();
                 List<Table> orders = tableService.GetAllTables();

                 TableOrdersListView.Items.Clear();

                 foreach (var table in tables)
                 {
                     ListViewItem item = new ListViewItem(table.TableId.ToString());
                     item.SubItems.Add(table.Status.ToString());
                     item.SubItems.Add(table.Capacity.ToString());
                     item.SubItems.Add(order.OrderItem);
                     item.SubItems.Add(order.Wait.ToString());
                     item.SubItems.Add(order.OrderState);
                     item.Tag = order;
                     TableOrdersListView.Items.Add(item);
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error loading order overview: {ex.Message}");
             }
         }*/




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
    }
}
