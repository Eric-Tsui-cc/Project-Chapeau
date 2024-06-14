using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ChapeauModel;

namespace ChapeauDAL
{
    public class TableDao : BaseDao
    {
        public List<Table> GetAllTables()
        {
            string query = "SELECT TableId, Status, Capacity FROM [TABLE]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Table> ReadTables(DataTable dataTable)
        {
            List<Table> tables = new List<Table>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Table table = new Table
                {
                    TableId = Convert.ToInt32(dr["TableId"]),
                    Status = Table.StringToStatus(dr["Status"].ToString()),
                    Capacity = Convert.ToInt32(dr["Capacity"])
                };
                tables.Add(table);
            }
            return tables;
        }

        public Table GetById(int id)
        {
            string query = "SELECT * FROM [TABLE] WHERE TableId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new Table
                {
                    TableId = Convert.ToInt32(row["TableId"]),
                    Status = Table.StringToStatus(row["Status"].ToString()),
                    Capacity = Convert.ToInt32(row["Capacity"])
                };
            }

            return null;
        }

        public void ChangeTableStatus(Table table)
        {
            string query = "UPDATE [TABLE] SET Status = @Status WHERE TableId = @Id;";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Id", table.TableId),
                new SqlParameter("@Status", Table.StatusToString(table.Status))
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteTable(int tableId)
        {
            string query = "DELETE FROM [TABLE] WHERE TableId = @Id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", tableId)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void AddTable(Table table)
        {
            string query = "INSERT INTO [TABLE] (TableId, Status, Capacity) VALUES (@TableId, @Status, @Capacity);";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@TableId", table.TableId),
                new SqlParameter("@Status", Table.StatusToString(table.Status)),
                new SqlParameter("@Capacity", table.Capacity)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public List<Table> GetByStatus(string status)
        {
            string query = "SELECT * FROM [TABLE] WHERE Status = @Status;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Status", status)
            };

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
    }
}
