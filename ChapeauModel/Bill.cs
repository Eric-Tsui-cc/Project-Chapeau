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
        public int billId { get; set; }
        public decimal Amount { get; set; }
        public decimal Tip { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int OrderId { get; set; }
        public Bill() { }
        public Bill(int billId, decimal amount, decimal tip, PaymentMethod paymentMethod, int orderId)
        {
            this.billId = billId;
            Amount = amount;
            Tip = tip;
            PaymentMethod = paymentMethod;
            OrderId = orderId;
        }
    }
}
