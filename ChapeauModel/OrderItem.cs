using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrderitem
    {
        Available,
        OutOfStock
    }

    public class OrderItem
    {
        public string dateTime { get; set; }
        public MenuItem menuItem { get; set; }
        public int orderId { get; set; }
        public int count { get; set; }
        public StatusOfOrderitem status { get; set; }
        public OrderItem() { }
        public OrderItem(string dateTime, MenuItem menuItem, int orderId, int count, StatusOfOrderitem status)
        {
            this.dateTime = dateTime;
            this.menuItem = menuItem;
            this.orderId = orderId;
            this.count = count;
            this.status = status;
        }
    }
}
