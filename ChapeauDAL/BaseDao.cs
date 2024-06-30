using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ChapeauModel;

namespace ChapeauDAL
{
    public abstract class BaseDao
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;

        public BaseDao()
        {
            if (ConfigurationManager.ConnectionStrings["RestuarantProjectGroup4"] == null)
            {
                throw new Exception("Connection string 'RestuarantProjectGroup4' not found in App.config.");
            }
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestuarantProjectGroup4"].ConnectionString);
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
                // Print.ErrorLog(e);
                throw;
            }
            return conn;
        }

        private void CloseConnection()
        {
            conn.Close();
        }

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
                // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

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

        public object ExecuteScalarQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Plebbproject"].ConnectionString))
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

    }
}
