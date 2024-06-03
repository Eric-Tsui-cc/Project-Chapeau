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
        public Employee EmployeeId { get; set; }
        public StatusOfOrder Status { get; set; }
        public Table TableId {  get; set; }
        public List<OrderItem> items { get; set; }
        public Order() { }
        public Order(int OrderId, string Comment, Employee EmployeeId, StatusOfOrder Status, Table TableId, List<OrderItem> items)
        {
            this.OrderId = OrderId;
            this.EmployeeId = EmployeeId;
            this.Status = Status;
            this.TableId = TableId;
            this.items = items;
        }
    }


}
