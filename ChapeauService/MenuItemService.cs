using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class MenuItemService
    {
        private MenuItemDao menuItemdb;
        public MenuItemService() 
        {
            menuItemdb = new MenuItemDao();
        }
        public List<MenuItem> GetMenuItems()
        {
            List<MenuItem> MenuList = menuItemdb.GetAllMenuItems();
            return MenuList;
        }
        public List<MenuItem> GetMenuItemsByID(int ID)
        {
           List<MenuItem> MenuList = menuItemdb.GetMenuItemById(ID);   
            return MenuList;
        }
        public List<MenuItem> GetMenuItemsByName(string name)
        {
            List<MenuItem> MenuList = menuItemdb.GetMenuItemByName(name);
            return MenuList;
        }
        public List<MenuItem> GetMenuItemsByCard(string card)
        {
            List<MenuItem> MenuList = menuItemdb.GetMenuItemsByCard(card);
            return MenuList;
        }
        public List<MenuItem> GetMenuItemsByCardAndCatergory(string card, string category)
        {
            List<MenuItem> MenuList = menuItemdb.GetMenuItemsByCardAndCategory(card, category);
            return MenuList;
        }
       public List<string> GetMenuCards()
        {
            List<string> Cards= menuItemdb.GetAllCards();
            return Cards;
        }
        public List<string> GetCategories()
        {
            List<string> Categories= menuItemdb.GetAllCategories ();
            return Categories;
        }

    }
}
