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
        private readonly TableDao _tableDao = new TableDao();
        private readonly OrderItemDao _orderItemDao;
        private readonly EmployeeDao _employeeDao = new EmployeeDao();
        private readonly MenuItemDao _menuItemDao = new MenuItemDao();

        public OrderDao(OrderItemDao orderItemDao)
        {
            _orderItemDao = orderItemDao;
        }
        public OrderDao()
        {

        }
        public List<Order> GetAllOrders()
        {
            string query = "SELECT orderId, tableId, status, employeeId FROM [ORDER]";
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

                Employee employee = _employeeDao.GetByEmployeeId(employeeId);
                Table table = _tableDao.GetTableById(tableId);
                List<OrderItem> items = _orderItemDao.GetOrderItemsByOrderId(orderId);

                string statusString = dr["Status"].ToString();
                StatusOfOrder status;
                if (Enum.IsDefined(typeof(StatusOfOrder), statusString))
                {
                    status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), statusString);
                }
                else
                {
                    status = StatusOfOrder.Running;
                }
                Order order = new Order()
                {
                    OrderId = orderId,
                    Employee = employee,
                    Status = status,
                    Table = table,
                    items = items
                };
                orders.Add(order);
            }
            return orders;
        }
        public Order GetOrderById(int orderId)
        {
            Order order = null;
            string query = "SELECT * FROM [ORDER] WHERE OrderId = @OrderId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@OrderId", orderId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                int employeeId = Convert.ToInt32(row["EmployeeId"]);
                int tableId = Convert.ToInt32(row["TableId"]);

                Employee employee = _employeeDao.GetByEmployeeId(employeeId);
                Table table = _tableDao.GetTableById(tableId);
                List<OrderItem> items = _orderItemDao.GetOrderItemsByOrderId(orderId);

                order = new Order
                {
                    OrderId = orderId,
                    Employee = employee,
                    Status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), row["Status"].ToString()),
                    Table = table,
                    items = items
                };
            }

            return order;
        }


        public void CreateOrder(Order order)
        {
            string query = "INSERT INTO [ORDER] (tableId, status, employeeId) VALUES (@TableId, @Status, @EmployeeId); SELECT SCOPE_IDENTITY();";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@TableId", order.Table.TableId),
                new SqlParameter("@Status", order.Status.ToString()),
                new SqlParameter("@EmployeeId", order.Employee.EmployeeId)
    };

            // Execute the query and get the generated order ID
            int orderId = Convert.ToInt32(ExecuteScalarQuery(query, sqlParameters));

            // Set the generated order ID to the order object
            order.OrderId = orderId;

            foreach (OrderItem item in order.items)
            {
                string itemQuery = "INSERT INTO ORDER_ITEM (orderId, menuItemId, count, status, OrderTime) VALUES (@OrderId, @MenuItemId, @Count, @Status, @OrderTime)";
                SqlParameter[] itemParameters =
                {
                    new SqlParameter("@OrderId", orderId), // Use the generated order ID
                    new SqlParameter("@MenuItemId", _menuItemDao.GetMenuItemByName(item.MenuItem.Name).MenuItemId),
                    new SqlParameter("@Count", item.Count),
                    new SqlParameter("@Status", item.Status.ToString()),
                    new SqlParameter("@OrderTime", item.OrderTime)
        };
                ExecuteEditQuery(itemQuery, itemParameters);
            }
        }


        public void DeleteOrder(Order order)
        {
            string query = "DELETE FROM [Order] WHERE OrderId=@OrderId;";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@OrderId", order.OrderId)
    };
            ExecuteEditQuery(query, sqlParameters);

            string deleteItemQuery = "DELETE FROM OrderItem WHERE OrderId=@OrderId;";
            SqlParameter[] deleteItemParameters =
            {
                new SqlParameter("@OrderId", order.OrderId)
    };
            ExecuteEditQuery(deleteItemQuery, deleteItemParameters);
        }
        public List<int> GetRunningOrderIdsByTableId(int tableId)
        {
            List<int> orderIds = new List<int>();
            string query = "SELECT OrderId FROM [ORDER] WHERE TableId = @TableId AND Status = @Status;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@TableId", tableId),
                new SqlParameter("@Status", StatusOfOrder.Running.ToString())
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                orderIds.Add(Convert.ToInt32(row["OrderId"]));
            }

            return orderIds;
        }
        public void ChangeOrderStatusToServed(int orderId)
        {
            string query = "UPDATE [ORDER] SET Status = @Status WHERE OrderId = @OrderId";
            SqlParameter[] sqlParameters =
            {
                 new SqlParameter("@Status", StatusOfOrder.Served.ToString()),
                 new SqlParameter("@OrderId", orderId)
    };

            ExecuteEditQuery(query, sqlParameters);
        }

    }
}