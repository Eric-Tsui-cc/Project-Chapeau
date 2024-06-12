using ChapeauDAL;
using ChapeauModel;

namespace ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderDao orderDao = new OrderDao();
            MenuItemDao menuItemDao = new MenuItemDao();
            TableDao tableDao = new TableDao();
            string card = "Lunch";
            string catagory = "Mains";
            List<MenuItem> menuItemList = menuItemDao.GetAllMenuItems();
            List<string>Cards = menuItemDao.GetAllCategories();
            List<Table>tables = tableDao.GetAllFreeTables();
            



            foreach (MenuItem menuItem in menuItemList)
            {
                Console.WriteLine(menuItem.Name);
            }
        }
    }
}