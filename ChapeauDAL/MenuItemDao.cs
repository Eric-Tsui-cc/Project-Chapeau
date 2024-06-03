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
        public MenuItem GetMenuItemById(int menuItemId)
        {
            MenuItem menuItem = null;
            string query = "SELECT * FROM MenuItems WHERE MenuItemId = @MenuItemId;";
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
        public void CreateMenuItem(MenuItem menuItem)
        {
            string query = "INSERT INTO MenuItem (MenuItemId, Name, Category, Card, Price, Stock) VALUES (@MenuItemId, @Name, @Category, @Card, @Price, @Stock)";
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
            string query = "UPDATE MenuItem " +
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
            string query = "DELETE FROM MenuItem WHERE MenuItemId=@MenuItemId;";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@MenuItemId", menuItemId)
        };
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
