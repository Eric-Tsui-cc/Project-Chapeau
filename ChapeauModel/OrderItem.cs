using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrderitem { Running, Preparing, Prepared, Servered }


    public class OrderItem
    {
        public DateTime OrderTime { get; set; }
        public MenuItem MenuItemId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public StatusOfOrderitem Status { get; set; }

        public OrderItem() { }
        public OrderItem(DateTime orderTime, MenuItem menuItemId, int orderId, int count, StatusOfOrderitem Status)
        {
            OrderTime = orderTime;
            MenuItemId = menuItemId;
            OrderId = orderId;
            Count = count;
            this.Status = Status;
        }
    }
}
