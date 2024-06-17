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
    public partial class MenuForm : Form
    {
        private OrderService orderService;
        private Dictionary<string, (MenuItem, int count) > selectedItems;
        public MenuForm()
        {
           // InitializeComponent();
            orderService = new OrderService();
            selectedItems = new Dictionary<string, (MenuItem item, int count)>();
            ConfigureListView();
        }
        private void ConfigureListView() 
        {
            ListViewMenu.View = View.Details;
            ListViewMenu.Columns.Add("Name", 150, HorizontalAlignment.Left);
            ListViewMenu.Columns.Add("Price", 50, HorizontalAlignment.Left);
            ListViewMenu.Columns.Add("Quantity", 50, HorizontalAlignment.Left);
        }
        private void Overview_Click(object sender, EventArgs e)
        {
        }
        private void LoadCategories() 
        {
            cmbCategory.Items.Clear();
            List<string> categories = orderService.GetAllCategories();
            foreach (string category in categories)
            {
                cmbCategory.Items.Add(category);
            }
        }
        private void LoadItems(string category)
        {
            try
            {
                ListViewMenu.Items.Clear();
                List<MenuItem> menuItems = orderService.GetMenuItemGetByCardAndCategory("Lunch", category);
                foreach (var item in menuItems)
                {
                    ListViewItem listItem = new ListViewItem(item.Name);
                    listItem.SubItems.Add(item.Price.ToString("C"));
                    listItem.SubItems.Add("1"); // default count is 1
                    listItem.Tag = item; // store the MenuItem object in the Tag property
                    ListViewMenu.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}");
            }
        }
        private void btnLunch_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem != null)
            {
                LoadItems(cmbCategory.SelectedItem.ToString());
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ListViewMenu.SelectedItems.Count > 0)
            {
                var selectedItem = ListViewMenu.SelectedItems[0];
                var menuItem = (MenuItem)selectedItem.Tag;
                if (selectedItems.ContainsKey(menuItem.Name))
                {
                    selectedItems[menuItem.Name] = (menuItem, selectedItems[menuItem.Name].count + 1);
                }
                else
                {
                    selectedItems[menuItem.Name] = (menuItem, 1);
                }
                UpdateListView();
            }
        }
        private void UpdateListView()
        {
            ListViewMenu.Items.Clear();
            foreach (var entry in selectedItems)
            {
                var item = entry.Value.Item1;
                var count = entry.Value.count;
                ListViewItem listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Price.ToString("C"));
                listItem.SubItems.Add(count.ToString());
                ListViewMenu.Items.Add(listItem);
            }
        }
    }
}
