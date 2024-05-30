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
        private List<Order> ReadOrders(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int orderId = Convert.ToInt32(dr["OrderId"]);
                int employeeId = Convert.ToInt32(dr["EmployeeId"]);
                int tableId = Convert.ToInt32(dr["TableId"]);

                Employee employee = GetEmployeeById(employeeId);
                Table table = GetTableById(tableId);
                List<OrderItem> items = GetOrderItemsByOrderId(orderId);

                Order order = new Order()
                {
                    OrderId = orderId,
                    EmployeeId = employee,
                    Status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), dr["Status"].ToString()),
                    TableId = table,
                    items = items
                };
                orders.Add(order);
            }
            return orders;
        }


        public void CreateOrder(Order order)
        {
            string query = "INSERT INTO Orders (orderId, tableId, status, employeeId) VALUES (@OrderId, @TableId, @Status, @EmployeeId)";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@OrderId", order.OrderId),
                new SqlParameter("@TableId", order.TableId),
                new SqlParameter("@Status", order.Status),
                new SqlParameter("@EmployeeId", order.EmployeeId)
            };
            ExecuteEditQuery(query, sqlParameters);

            foreach (OrderItem item in order.items)
            {
                string itemQuery = "INSERT INTO OrderItems (orderId, menuItem, count, status) VALUES (@OrderId, @MenuItem, @Count, @Status)";
                SqlParameter[] itemParameters =
                {
                new SqlParameter("@OrderId", order.OrderId),
                new SqlParameter("@MenuItem", item.MenuItemId),
                new SqlParameter("@Count", item.Count),
                new SqlParameter("@Status", item.Status)
            };
                ExecuteEditQuery(itemQuery, itemParameters);
            }
        }

        


    }
}