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
       
        // Finalize the payment for one or more bills
        public void FinalizePayment(Bill bill)
        {
            string query = "INSERT INTO [Bill] (OrderId,Amount, Tip, PaymentMethod, Date, Time, Feedback) VALUES (@orderId, @amount, @tip, @paymentMethod, @date, @time, @feedback)";
            SqlParameter[] sqlParameters = {
                    new SqlParameter("@orderId", bill.Orders[0].OrderId),
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
}
