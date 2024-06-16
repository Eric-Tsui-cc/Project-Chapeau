using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class BillDao : BaseDao
    {
        private readonly OrderDao _orderDao = new OrderDao();

        // Retrieve all bills from the database
        public List<Bill> GetAllBills()
        {
            string query = "SELECT BillId, OrderId, Amount, Tip, PaymentMethod, Date, Time, Feedback FROM [Bill]";
            return ReadBills(ExecuteSelectQuery(query));
        }

        // Retrieve a specific bill by its ID
        public Bill GetBillById(int billId)
        {
            string query = "SELECT BillId, OrderId, Amount, Tip, PaymentMethod, Date, Time, Feedback FROM [Bill] WHERE BillId = @billId";
            SqlParameter[] sqlParameters = { new SqlParameter("@billId", billId) };
            return ReadBills(ExecuteSelectQuery(query, sqlParameters))[0];
        }

        // Finalize the payment for a particullar Bill
        public void FinalizePayment(Bill bill)
        {
            string query = "INSERT INTO [Bill] (OrderId, Amount, Tip, PaymentMethod, Date, Time, Feedback) VALUES (@orderId, @amount, @tip, @paymentMethod, @date, @time, @feedback)";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@orderId", bill.Orders[0].OrderId), // Assuming OrderId is a property of Order
                new SqlParameter("@amount", bill.Amount),
                new SqlParameter("@tip", bill.Tip),
                new SqlParameter("@paymentMethod", bill.PaymentMethod.ToString()), // Enum as string
                new SqlParameter("@date", bill.Date),
                new SqlParameter("@time", bill.Time),
                new SqlParameter("@feedback", bill.Feedback)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        // Method to read the bills from the data table
        private List<Bill> ReadBills(DataTable dataTable)
        {
            List<Bill> bills = new List<Bill>();

            foreach (DataRow row in dataTable.Rows)
            {
                int orderId = row.Field<int>("OrderId");
                Order order = _orderDao.GetOrderById(orderId); 
                List<Order> orders = new List<Order> { order };

                Bill bill = new Bill(
                    Orders: orders,
                    amount: row.Field<decimal>("Amount"),
                    tip: row.Field<decimal>("Tip"),
                    paymentMethod: Enum.Parse<PaymentMethod>(row.Field<string>("PaymentMethod")),
                    date: row.Field<DateTime>("Date"),
                    time: row.Field<TimeSpan>("Time"),
                    feedback: row.Field<string>("Feedback")
                );
                bills.Add(bill);
            }

            return bills;
        }
    }
}
