using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum PaymentMethod
    {
        Credit,
        Debit,
        Cash
    }

    public class Bill
    {
        public int BillId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tip { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Order OrderId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public Bill() { }
        public Bill(int BillId, decimal Amount, decimal Tip, PaymentMethod PaymentMethod, Order OrderId, DateOnly Date, TimeOnly Time)
        {
            this.BillId = BillId;
            this.Amount = Amount;
            this.Tip = Tip;
            this.PaymentMethod = PaymentMethod;
            this.OrderId = OrderId;
            this.Date = Date;
            this.Time = Time;    
        }
    }
}
