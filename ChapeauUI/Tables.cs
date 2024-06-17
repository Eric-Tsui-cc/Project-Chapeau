using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class Tables : Form
    {
        TableService_Bogdan tableService = new TableService_Bogdan();
        private Employee currentEmployee;

        public Tables(Employee employee)
        {
            InitializeComponent();
            currentEmployee = employee; // Set the current employee
            ConfigureTableListView();
            LoadTableOverview();
            this.currentEmployee = currentEmployee;
        }


        private void ConfigureTableListView()
        {
            TableListView.View = View.Details;
            TableListView.Columns.Add("Table ID", 100, HorizontalAlignment.Left);
            TableListView.Columns.Add("Status", 100, HorizontalAlignment.Left);
            TableListView.Columns.Add("Capacity", 100, HorizontalAlignment.Left);
        }

        private void LoadTableOverview()
        {
            try
            {
                List<Table> tables = tableService.GetAllTables();
                TableListView.Items.Clear();

                foreach (var table in tables)
                {
                    ListViewItem item = new ListViewItem(table.TableId.ToString())
                    {
                        SubItems = { table.Status.ToString(), table.Capacity.ToString() },
                        Tag = table
                    };
                    TableListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading table overview: {ex.Message}");
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (TableListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = TableListView.SelectedItems[0];
                int tableId = int.Parse(selectedItem.Text);

                if (comboBoxStatus.SelectedItem != null)
                {
                    string newStatus = comboBoxStatus.SelectedItem.ToString();

                    try
                    {
                        Table table = tableService.GetTableById(tableId);
                        if (table != null)
                        {
                            table.Status = Table.StringToStatus(newStatus);
                            tableService.ChangeTableStatus(table);
                            LoadTableOverview();
                        }
                        else
                        {
                            MessageBox.Show("Table not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error changing table status: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a status.");
                }
            }
            else
            {
                MessageBox.Show("Please select a table to change the status.");
            }
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
        private void roundedButton1_Click(object sender, EventArgs e) // LogOut
        {
            LoginPage newForm = new LoginPage();
            OpenUI(newForm);

        }

        private void roundedButton3_Click(object sender, EventArgs e) // orders
        {
            Orders newForm = new Orders(currentEmployee);

            OpenUI(newForm);


        }

        private void roundedButton2_Click(object sender, EventArgs e) //Home
        {
            ChapeauUI newForm = new ChapeauUI(currentEmployee);

            OpenUI(newForm);

        }
    }
}
