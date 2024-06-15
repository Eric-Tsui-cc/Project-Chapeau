using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Card Card { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public MenuItem() { }

        public MenuItem(int MenuItemId, string Name, Category Category, Card Card, decimal Price, int Stock)
        {
            this.MenuItemId = MenuItemId;
            this.Name = Name;
            this.Category = Category;
            this.Card = Card;
            this.Price = Price;
            this.Stock = Stock;
        }
    }
    public enum Category
    {
        Mains,
        Starters,
        Entremet,
        Desserts,
        Beers,
        Wines,
        Spirit,
        CoffeeTea
    }
    public enum Card
    {
        Lunch,
        Drinks,
        Dinner
    }
}


