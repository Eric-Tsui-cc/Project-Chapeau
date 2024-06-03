using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrderitem { }


    public class OrderItem
    {
        public DateTime OrderTime { get; set; }
        public MenuItem MenuItemId { get; set; }
        public Order OrderId { get; set; }
        public int Count { get; set; }
        public StatusOfOrderitem Status { get; set; }

        public OrderItem() { }
        public OrderItem(DateTime OrderTime, MenuItem MenuItemId, Order OrderId, int Count, StatusOfOrderitem Status)
        {
            this.OrderTime = OrderTime;
            this.MenuItemId = MenuItemId;
            this.OrderId = OrderId;
            this.Count = Count;
            this.Status = Status;
        }
    }
}
