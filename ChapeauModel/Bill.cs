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
        public Order OrderId { get; set; }
        public double  Amount { get; set; }
        public double  Tip { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Feedback { get; set; } // customer feedback if any

        public Bill()
        {
            
        }
        public Bill(int billId, Order orderId, double amount, double tip, PaymentMethod paymentMethod, DateOnly date, TimeOnly time, string feedback)
        {
            this.BillId = billId;
            this.OrderId = orderId;
            this.Amount = amount;
            this.Tip = tip;
            this.PaymentMethod = paymentMethod;
            this.Date = date;
            this.Time = time;
            this.Feedback = feedback;
        }
    }
}
