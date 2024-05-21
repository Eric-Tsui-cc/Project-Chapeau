using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfOrder { Paid,Unpaid }
    public class Order
    {   
        public int OrderId { get; set; }
        public string Comment { get; set; }
        public int EmployeeId { get; set; }
        public StatusOfOrder Status { get; set; }
        public int TableId {  get; set; }
        public List<OrderItem> items { get; set; }
        public Order() { }
        public Order(int OrderId, string Comment, int EmployeeId, StatusOfOrder Status, int TableId, List<OrderItem> items)
        {
            this.OrderId = OrderId;
            this.Comment = Comment;
            this.EmployeeId = EmployeeId;
            this.Status = Status;
            this.TableId = TableId;
            this.items = items;
        }
    }


}
