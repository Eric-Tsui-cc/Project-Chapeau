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

        // Finalize the payment for one or more bills
        public void FinalizePayment(List<Bill> bills)
        {
            foreach (Bill bill in bills)
            {
                string query = "INSERT INTO [Bill] (OrderId, Amount, Tip, PaymentMethod, Date, Time, Feedback) VALUES (@orderId, @amount, @tip, @paymentMethod, @date, @time, @feedback)";
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@orderId", bill.Order),
                    new SqlParameter("@amount", bill.Amount),
                    new SqlParameter("@tip", bill.Tip),
                    new SqlParameter("@paymentMethod", bill.PaymentMethod.ToString()), // Enum as string
                    new SqlParameter("@date", bill.Date.ToString()),
                    new SqlParameter("@time", bill.Time.ToString()),
                    new SqlParameter("@feedback", bill.Feedback)
                };
                ExecuteEditQuery(query, sqlParameters);
            }
        }

        //// Update the table status to 'Free' this should be in the table dao
        //string updateTableQuery = "UPDATE [TABLE] SET Status = 'Free' WHERE TableId = (SELECT TableId FROM [ORDER] WHERE OrderId = @orderId)";
        //SqlParameter[] tableParameters = { new SqlParameter("@orderId", bill.OrderId) };
        //ExecuteEditQuery(updateTableQuery, tableParameters);


        //  Method to read the bills from the data table
        
        
    }
}
