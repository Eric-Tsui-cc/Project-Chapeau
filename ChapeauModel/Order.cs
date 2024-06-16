using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChapeauModel.Table;

namespace ChapeauModel
{
    public class Order
    {
        public enum OrderStatus
        {
            Done,Preparing, Served, Undefined
        }
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderTakenTime { get; set; }
        public string Comments { get; set; }
        public int TableId { get; set; }
        public int MenuItemId { get; set; }
        public int Count { get; set; }

        public int BillId { get; set; }

        public static OrderStatus StringToStatus(string status)
        {
            return status switch
            {
                "Done" => OrderStatus.Done,
                "Preparing" => OrderStatus.Preparing,
                "Served" => OrderStatus.Served,
                _ => OrderStatus.Undefined,
            };
        }

        public static string StatusToString(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Done => "Done",
                OrderStatus.Preparing => "Preparing",
                OrderStatus.Served => "Served",
                _ => "Undefined",
            };
        }
        public TimeSpan GetWaitingTime()
        {
            return DateTime.Now - OrderTakenTime;
        }
        public override string ToString()
        {
            return $"";
        }

    }
}
