using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class MenuItemDao:BaseDao
    {
        public List<MenuItem> GetAllMenuItems()
        {
            string query = "SELECT MenuItemId, Name, Category, Card, Price, Stock FROM MENU_ITEM";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<MenuItem> ReadMenuItems(DataTable dataTable)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int menuItemId = Convert.ToInt32(dr["MenuItemId"]);
                string name = dr["Name"].ToString();
                Category category = (Category)Enum.Parse(typeof(Category), dr["Category"].ToString());
                Card card = (Card)Enum.Parse(typeof(Card), dr["Card"].ToString());
                decimal price = Convert.ToDecimal(dr["Price"]);
                int stock = Convert.ToInt32(dr["Stock"]);

                MenuItem menuItem = new MenuItem()
                {
                    MenuItemId = menuItemId,
                    Name = name,
                    Category = category,
                    Card = card,
                    Price = price,
                    Stock = stock
                };

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        public MenuItem GetMenuItemById(int menuItemId)
        {
            MenuItem menuItem = null;
            string query = "SELECT * FROM MENU_ITEM WHERE MenuItemId = @MenuItemId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@MenuItemId", menuItemId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                menuItem = new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemId"]),
                    Name = row["Name"].ToString(),
                    Category = (Category)Enum.Parse(typeof(Category), row["Category"].ToString()),
                    Card = (Card)Enum.Parse(typeof(Card), row["Card"].ToString()),
                    Price = Convert.ToDecimal(row["Price"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
            }

            return menuItem;
        }
        public MenuItem GetMenuItemByName(string itemName)
        {
            MenuItem menuItem = null;
            string query = "SELECT * FROM MENU_ITEM WHERE Name = @Name;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@Name", itemName)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                menuItem = new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemId"]),
                    Name = row["Name"].ToString(),
                    Category = (Category)Enum.Parse(typeof(Category), row["Category"].ToString()),
                    Card = (Card)Enum.Parse(typeof(Card), row["Card"].ToString()),
                    Price = Convert.ToDecimal(row["Price"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
            }

            return menuItem;
        }
        public List<MenuItem> GetMenuItemsByCard(string cardType)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            string query = "SELECT * FROM MENU_ITEM WHERE Card = @Card;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@Card", cardType)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                MenuItem menuItem = new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemId"]),
                    Name = row["Name"].ToString(),
                    Category = (Category)Enum.Parse(typeof(Category), row["Category"].ToString()),
                    Card = (Card)Enum.Parse(typeof(Card), row["Card"].ToString()),
                    Price = Convert.ToDecimal(row["Price"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
                menuItems.Add(menuItem);
            }

            return menuItems;
        }
        public List<string> GetAllCards()
        {
            List<string> cards = new List<string>();
            string query = "SELECT DISTINCT Card FROM MENU_ITEM";
            SqlParameter[] sqlParameters = new SqlParameter[0];

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                cards.Add(row["Card"].ToString());
            }

            return cards;
        }
        public List<string> GetAllCategories()
        {
            List<string> cards = new List<string>();
            string query = "SELECT DISTINCT Category FROM MENU_ITEM";
            SqlParameter[] sqlParameters = new SqlParameter[0];

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                cards.Add(row["Category"].ToString());
            }

            return cards;
        }
        public List<MenuItem> GetMenuItemsByCardAndCategory(string card, string category)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            string query = "SELECT * FROM MENU_ITEM WHERE Card = @Card AND Category = @Category;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Card", card),
                new SqlParameter("@Category", category)
            };

            DataTable dataTable = ExecuteSelectQuery(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                MenuItem menuItem = new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemId"]),
                    Name = row["Name"].ToString(),
                    Card = (Card)Enum.Parse(typeof(Card), row["Card"].ToString()),
                    Category = (Category)Enum.Parse(typeof(Category), row["Category"].ToString()),
                    Price = Convert.ToDecimal(row["Price"]),
                    // Add more properties as needed
                };

                menuItems.Add(menuItem);
            }

            return menuItems;
        }
        public void CreateMenuItem(MenuItem menuItem)
        {
            string query = "INSERT INTO MENU_ITEM (MenuItemId, Name, Category, Card, Price, Stock) VALUES (@MenuItemId, @Name, @Category, @Card, @Price, @Stock)";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@MenuItemId", menuItem.MenuItemId),
            new SqlParameter("@Name", menuItem.Name),
            new SqlParameter("@Category", menuItem.Category.ToString()),  
            new SqlParameter("@Card", menuItem.Card.ToString()),  
            new SqlParameter("@Price", menuItem.Price),
            new SqlParameter("@Stock", menuItem.Stock)
        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateMenuItem(MenuItem menuItem)
        {
            string query = "UPDATE MENU_ITEM " +
                           "SET Name=@Name, Category=@Category, Card=@Card, Price=@Price, Stock=@Stock " +
                           "WHERE MenuItemId=@MenuItemId;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@MenuItemId", menuItem.MenuItemId),
            new SqlParameter("@Name", menuItem.Name),
            new SqlParameter("@Category", menuItem.Category.ToString()),  
            new SqlParameter("@Card", menuItem.Card.ToString()),  
            new SqlParameter("@Price", menuItem.Price),
            new SqlParameter("@Stock", menuItem.Stock)
        };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteMenuItem(int menuItemId)
        {
            string query = "DELETE FROM MENU_ITEM WHERE MenuItemId=@MenuItemId;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@MenuItemId", menuItemId)
        };
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
