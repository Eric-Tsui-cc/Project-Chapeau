using ChapeauModel;
using System.Data.SqlClient;
using System.Data;

namespace ChapeauDAL
{
    public class OrderDao : BaseDao
    {
        public List<Order> GetAllOrders()
        {
            string query = "SELECT OrderId, EmployeeId, Status, OrderTakenTime, Comments, TableId, BillId, [Count], MenuItemId FROM Orders";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadAllOrders(ExecuteSelectQuery(query, sqlParameters));
        }

        private Order ReadOrder(DataRow row)
        {
            return new Order()
            {
                OrderId = Convert.ToInt32(row["OrderId"]),
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                Status = Order.StringToStatus(row["Status"].ToString()),
                OrderTakenTime = (DateTime)row["OrderTakenTime"],
                Comments = row["Comments"].ToString(),
                TableId = Convert.ToInt32(row["TableId"]),
                BillId = Convert.ToInt32(row["BillId"]),
                Count = Convert.ToInt32(row["Count"]),
                MenuItemId = Convert.ToInt32(row["MenuItemId"])
            };
        }

        public List<Order> ReadAllOrders(DataTable dataTable)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                orders.Add(ReadOrder(dr));
            }
            return orders;
        }

        public Order GetOrderById(int id)
        {
            string query = "SELECT * FROM [Orders] WHERE OrderId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return ReadOrder(row);
            }

            return null;
        }

        public void UpdateOrderStatus(Order order)
        {
            string query = "UPDATE [Orders] SET Status = @Status WHERE OrderId = @OrderId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Status", Order.StatusToString(order.Status)),
                new SqlParameter("@OrderId", order.OrderId)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
