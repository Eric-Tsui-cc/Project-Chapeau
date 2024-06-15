using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrderitem {
        Outofstock,
        Available
    }


    public class OrderItem
    {
        public DateTime OrderTime { get; set; }
        public MenuItem MenuItem { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
        public StatusOfOrderitem Status { get; set; }
        public string Comment {  get; set; }

        public OrderItem() { }
        public OrderItem(DateTime OrderTime, MenuItem MenuItemId, Order OrderId, int Count, StatusOfOrderitem Status, string comment)
        {
            this.OrderTime = OrderTime;
            this.MenuItem = MenuItemId;
            this.Order = OrderId;
            this.Count = Count;
            this.Status = Status;
            Comment = comment;
        }
    }
}
