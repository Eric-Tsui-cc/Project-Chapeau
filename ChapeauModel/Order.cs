using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrder { Running, Prepared, Server }
    public class Order
    {   
        public int orderId { get; set; }
        public string tableName { get; set; }
        public string comment { get; set; }
        public string orderName { get; set; }
        public StatusOfOrder status { get; set; }
        public int employeeId {  get; set; }
        public List<OrderItem> items { get; set; }
        public Order() { }
        public Order(int orderId, string tableName, List<OrderItem> items, string comment, string orderName, StatusOfOrder status, int employeeId)
        {
            this.orderId = orderId;
            this.tableName = tableName;
            this.items = items;
            this.comment = comment;
            this.orderName = orderName;
            this.status = status; 
            this.employeeId = employeeId;
        }
    }


}
