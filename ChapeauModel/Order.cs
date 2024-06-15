using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrder { Running, Preparing, Prepared, Served }
    public class Order
    {   
        public int OrderId { get; set; }
        public Employee Employee { get; set; }
        public StatusOfOrder Status { get; set; }
        public Table Table {  get; set; }
        public List<OrderItem> items { get; set; }
        public int PaymentStatus { get; set; }
        public Order() { }
        public Order(int OrderId, string Comment, Employee EmployeeId, StatusOfOrder Status, Table TableId, List<OrderItem> items, int paymentStatus)
        {
            this.OrderId = OrderId;
            this.Employee = EmployeeId;
            this.Status = Status;
            this.Table = TableId;
            this.items = items;
            PaymentStatus = paymentStatus;
        }
    }


}
