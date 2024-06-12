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
        public Order OrderId { get; set; }
        public int Count { get; set; }
        public StatusOfOrderitem Status { get; set; }

        public OrderItem() { }
        public OrderItem(DateTime OrderTime, MenuItem MenuItemId, Order OrderId, int Count, StatusOfOrderitem Status)
        {
            this.OrderTime = OrderTime;
            this.MenuItem = MenuItemId;
            this.OrderId = OrderId;
            this.Count = Count;
            this.Status = Status;
        }
    }
}
