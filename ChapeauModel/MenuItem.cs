using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum Category
    {
        Beers,
        SoftDrink
    }
    public enum Card
    {
        Lunch,
        Drink,
        Dinner
    }

    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Card Card { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public MenuItem(int menuItemId, string name, Category category, Card card, decimal price, int stock)
        {
            MenuItemId = menuItemId;
            Name = name;
            Category = category;
            Card = card;
            Price = price;
            Stock = stock;
        }
    }
}
