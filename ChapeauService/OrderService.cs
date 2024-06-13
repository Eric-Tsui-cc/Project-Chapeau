using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class OrderService
    {
        private OrderDao orderdb;
        private MenuItemDao menuItemdb;
        private EmployeeDao employeedb;
        private OrderItemDao orderitemdb;
        private TableDao tabledb;
        private static int currentOrderId = 0;


        public OrderService()
        {
            orderdb = new OrderDao();
            menuItemdb = new MenuItemDao();
            employeedb = new EmployeeDao();
            orderitemdb = new OrderItemDao();
            tabledb = new TableDao();
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = orderdb.GetAllOrders();
            return orders;
        }
        public List<MenuItem> GetMenuItemsbyCard(string Card)
        {
            List<MenuItem> menuItems = menuItemdb.GetMenuItemsByCard(Card);
            return menuItems;
        }
        public List<string> GetAllCards()
        {
            return menuItemdb.GetAllCards();
        }
        public List<string> GetAllCategories()
        {
            return menuItemdb.GetAllCategories();
        }
        public List<Employee> GetAllActiveEmployees()
        {
            return employeedb.GetAllActiveEmployees();
        }
        public List<Table> GetAllTables()
        {
            return tabledb.GetAllTables();
        }
        public List<Table>GetAllFreeTables()
        {
            return tabledb.GetAllFreeTables();
        }
        public List<MenuItem> GetMenuItemGetByCardAndCategory(string card, string category)
        {
            return menuItemdb.GetMenuItemsByCardAndCategory(card, category);
        }
        public List<MenuItem> GetAllMenuItems()
        {
            return menuItemdb.GetAllMenuItems();
        }
        public void AddOrder(Order order)
        {
            orderdb.CreateOrder(order);
        }
        public Order GetByOrderId(int id)
        {
            Order order = orderdb.GetOrderById(id);
            return order;
        }
        public static int GetNextOrderId()
        {
            return currentOrderId++;
        }
        public void ChangeTableStatusToOccupied(int tableId)
        {
            tabledb.ChangeTableStatusToOccupied(tableId);
        }

    }
    public static class OrderIdGenerator
    {
        private static int currentOrderId = 0;

        public static int GetNextOrderId()
        {
            return currentOrderId++;
        }
    }
}
