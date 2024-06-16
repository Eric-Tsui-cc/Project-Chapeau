using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum PaymentMethod
    {
        Credit=0,
        Debit,
        Cash
    }

    public class Bill
    {
        public List<Order> Orders { get; set; }
        public decimal Amount { get; set; }
        public decimal Tip { get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Feedback { get; set; } // customer feedback if any


        public Bill(List<Order> Orders, decimal amount, decimal tip, PaymentMethod paymentMethod, DateTime date, TimeSpan time, string feedback)
        {
            
            this.Orders = Orders;
            this.Amount = amount;
            this.Tip = tip;
            this.PaymentMethod = paymentMethod;
            this.Date = date;
            this.Time = time;
            this.Feedback = feedback;
        }
    }
}
