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
    public class OrderDao:BaseDao
    {
        public List<Order> GetAllOrders()
        {
            string query = "SELECT orderId, tableName, comment, orderName, status, employeeId FROM Orders";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadOrders(ExecuteSelectQuery(query, sqlParameters));
        }
        public Order GetOrderInfo(int orderId)
        {
            string query = "SELECT orderId, tableName, comment, orderName, status, employeeId FROM Orders WHERE orderId = @OrderId";
            SqlParameter[] sqlParameters = { new SqlParameter("@OrderId", orderId) };
            DataTable resultTable = ExecuteSelectQuery(query, sqlParameters);

            if (resultTable.Rows.Count == 1)
            {
                DataRow dr = resultTable.Rows[0];
                Order order = new Order()
                {
                    orderId = (int)dr["orderId"],
                    tableName = dr["tableName"].ToString(),
                    comment = dr["comment"].ToString(),
                    orderName = dr["orderName"].ToString(),
                    status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), dr["status"].ToString()),
                    employeeId = (int)dr["employeeId"]
                };
                return order;
            }
            else
            {
                return null;
            }
        }
        public void CreateOrder(Order order)
        {
            string query = "INSERT INTO Orders (orderId, tableName, comment, orderName, status, employeeId) VALUES (@OrderId, @TableName, @Comment, @OrderName, @Status, @EmployeeId)";
            SqlParameter[] sqlParameters =
            {
        new SqlParameter("@OrderId", order.orderId),
        new SqlParameter("@TableName", order.tableName),
        new SqlParameter("@Comment", order.comment),
        new SqlParameter("@OrderName", order.orderName),
        new SqlParameter("@Status", order.status),
        new SqlParameter("@EmployeeId", order.employeeId)
    };
            ExecuteEditQuery(query, sqlParameters);
        }



        private List<Order> ReadOrders(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Order order = new Order()
                {
                    orderId = (int)dr["orderId"],
                    tableName = dr["tableName"].ToString(),
                    comment = dr["comment"].ToString(),
                    orderName = dr["orderName"].ToString(),
                    status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), dr["status"].ToString()),
                    employeeId = (int)dr["employeeId"]
                };
                orders.Add(order);
            }
            return orders;
        }

    }
}
