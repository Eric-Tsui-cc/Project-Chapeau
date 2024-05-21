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
    public class OrderDao : BaseDao
    {
        public List<Order> GetAllOrders()
        {
            string query = "SELECT orderId, tableId, status, employeeId FROM Orders";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadOrders(ExecuteSelectQuery(query, sqlParameters));
        }
        public Order GetOrderInfo(int orderId)
        {
            string query = "SELECT orderId, tableId, status, employeeId FROM Orders WHERE orderId = @OrderId";
            SqlParameter[] sqlParameters = { new SqlParameter("@OrderId", orderId) };
            DataTable resultTable = ExecuteSelectQuery(query, sqlParameters);

            if (resultTable.Rows.Count == 1)
            {
                DataRow dr = resultTable.Rows[0];
                Order order = new Order()
                {
                    orderId = (int)dr["orderId"],
                    tableId = (int)dr["tableId"],
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
            string query = "INSERT INTO Orders (orderId, tableId, status, employeeId) VALUES (@OrderId, @TableId, @Status, @EmployeeId)";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@OrderId", order.orderId),
                new SqlParameter("@TableId", order.tableId),
                new SqlParameter("@Status", order.status),
                new SqlParameter("@EmployeeId", order.employeeId)
            };
            ExecuteEditQuery(query, sqlParameters);

            foreach (OrderItem item in order.items)
            {
                string itemQuery = "INSERT INTO OrderItems (orderId, menuItem, count, status) VALUES (@OrderId, @MenuItem, @Count, @Status)";
                SqlParameter[] itemParameters =
                {
                new SqlParameter("@OrderId", order.orderId),
                new SqlParameter("@MenuItem", item.menuItem),
                new SqlParameter("@Count", item.count),
                new SqlParameter("@Status", item.status)
            };
                ExecuteEditQuery(itemQuery, itemParameters);
            }
        }

        private List<Order> ReadOrders(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Order order = new Order()
                {
                    orderId = (int)dr["orderId"],
                    tableId = dr["tableId"],
                    status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), dr["status"].ToString()),
                    employeeId = (int)dr["employeeId"]
                };
                orders.Add(order);
            }
            return orders;
        }

    }
}