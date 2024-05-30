using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ChapeauModel;

namespace ChapeauDAL
{                                                           //maybe need to change somethings in this file.
    public abstract class BaseDao
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;

        public BaseDao()
        {
            // DO NOT FORGET TO INSERT YOUR CONNECTION STRING NAMED 'SOMEREN DATABASE' IN YOUR APP.CONFIG!!
            /*
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SomerenDatabase"].ConnectionString);
                adapter = new SqlDataAdapter();
             */
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SomerenDatabase"].ConnectionString);
            adapter = new SqlDataAdapter();
        }

        protected SqlConnection OpenConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }
            }
            catch (Exception e)
            {
                //Print.ErrorLog(e);
                throw;
            }
            return conn;
        }

        private void CloseConnection()
        {
            conn.Close();
        }

        /* For Insert/Update/Delete Queries with transaction */
        protected void ExecuteEditTranQuery(string query, SqlParameter[] sqlParameters, SqlTransaction sqlTransaction)
        {
            SqlCommand command = new SqlCommand(query, conn, sqlTransaction);

            try
            {
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /* For Insert/Update/Delete Queries */
        protected void ExecuteEditQuery(string query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /* For Select Queries */
        public object ExecuteScalarQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(/* Your connection string */))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }

        protected DataTable ExecuteSelectQuery(string query, params SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable;
            DataSet dataSet = new DataSet();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException e)
            {
                // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return dataTable;
        }
        public Table GetTableById(int tableId)
        {
            Table table = null;
            string query = "SELECT * FROM Tables WHERE TableId = @TableId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@TableId", tableId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                table = new Table
                {
                    TableId = Convert.ToInt32(row["TableId"]),
                    TableNr = row["TableNr"].ToString(),
                    Status = (StatusOfTable)Enum.Parse(typeof(StatusOfTable), row["Status"].ToString())
                };
            }

            return table;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeId", employeeId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                employee = new Employee
                {
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), row["Status"].ToString())
                };
            }

            return employee;
        }
        public MenuItem GetMenuItemById(int menuItemId)
        {
            MenuItem menuItem = null;
            string query = "SELECT * FROM MenuItems WHERE MenuItemId = @MenuItemId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@MenuItemId", menuItemId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                menuItem = new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemId"]),
                    Name = row["Name"].ToString(),
                    Category = (Category)Enum.Parse(typeof(Category), row["Category"].ToString()),
                    Card = (Card)Enum.Parse(typeof(Card), row["Card"].ToString()),
                    Price = Convert.ToDecimal(row["Price"]),
                    Stock = Convert.ToInt32(row["Stock"])
                };
            }

            return menuItem;
        }
        public OrderItem GetOrderItemByMenuItemID(int menuItemId)
        {
            OrderItem orderItem = null;
            string query = "SELECT * FROM OrderItems WHERE MenuItemId = @MenuItemId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@MenuItemId", menuItemId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                int orderId = Convert.ToInt32(row["OrderId"]);

                MenuItem menuItem = GetMenuItemById(menuItemId);
                Order order = GetOrderById(orderId);

                orderItem = new OrderItem
                {
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    MenuItemId = menuItem,
                    OrderId = order,
                    Count = Convert.ToInt32(row["Count"]),
                    Status = (StatusOfOrderitem)Enum.Parse(typeof(StatusOfOrderitem), row["Status"].ToString())
                };
            }

            return orderItem;
        }
        public Order GetOrderById(int orderId)
        {
            Order order = null;
            string query = "SELECT * FROM Orders WHERE OrderId = @OrderId;";
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

                Employee employee = GetEmployeeById(employeeId);
                Table table = GetTableById(tableId);
                List<OrderItem> items = GetOrderItemsByOrderId(orderId);

                order = new Order
                {
                    OrderId = orderId,
                    EmployeeId = employee,
                    Status = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), row["Status"].ToString()),
                    TableId = table,
                    items = items
                };
            }

            return order;
        }
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM OrderItems WHERE OrderId = @OrderId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@OrderId", orderId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                int menuItemId = Convert.ToInt32(row["MenuItemId"]);
                MenuItem menuItem = GetMenuItemById(menuItemId);

                OrderItem orderItem = new OrderItem
                {
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    MenuItemId = menuItem,
                    OrderId = new Order { OrderId = orderId }, 
                    Count = Convert.ToInt32(row["Count"]),
                    Status = (StatusOfOrderitem)Enum.Parse(typeof(StatusOfOrderitem), row["Status"].ToString())
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }


    }
}