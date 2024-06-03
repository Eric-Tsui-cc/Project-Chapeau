using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class PaymentService
    {
        private readonly BillDao billDao;
        private readonly TableDao tableDao;
        private readonly OrderDao orderDao;

        public PaymentService()
        {
            billDao = new BillDao();
            tableDao = new TableDao();
            orderDao = new OrderDao();
        }

        public void FinalizeBill(Bill bill)
        {
            // Finalize the payment and save the bill to the database
            List<Bill> bills = new List<Bill> { bill };
            billDao.FinalizePayment(bills);

            // Free the table associated with the bill order
            FreeTable(bill.OrderId.TableId);

            // Set the order status to 'Paid'
            SetOrderToPaidOrUnpaid(bill.OrderId.OrderId, true);
        }

        public void FreeTable(int tableId)
        {
            // Retrieve the table and update its status to 'Free'
            Table table = tableDao.GetById(tableId);
            table.Status = "Free";
            tableDao.ChangeTableStatus(table);
        }

        public void SetOrderToPaidOrUnpaid(int orderId, bool isPaid)
        {
            // Retrieve the order and update its status to 'Paid' or 'Unpaid'
            Order order = orderDao.GetOrderById(orderId);
            order.Status = isPaid ? StatusOfOrder.Paid : StatusOfOrder.Unpaid;
            orderDao.CreateOrder(order);
        }
    }
}
