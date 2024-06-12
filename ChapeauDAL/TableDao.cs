using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class TableDao:BaseDao
    {
        public Table GetTableById(int tableId)
        {
            Table table = null;
            string query = "SELECT * FROM [TABLE] WHERE TableId = @TableId;";
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
                    Capacity = Convert.ToInt32(row["Capacity"]),
                    Status = (StatusOfTable)Enum.Parse(typeof(StatusOfTable), row["Status"].ToString())
                };
            }

            return table;
        }
        public List<Table> GetAllTables()
        {
            string query = "SELECT TableId, Status,TableNumber FROM [TABLE]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Table> GetAllFreeTables()
        {
            string query = "SELECT * FROM [TABLE] WHERE Status = 'Free'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Table> ReadTables(DataTable dataTable)
        {
            List<Table> tables = new List<Table>();

            foreach (DataRow dr in dataTable.Rows)
            {
                StatusOfTable status;
                if (!Enum.TryParse(dr["Status"].ToString(), out status))
                {
                    // deal with wrong value maybe?
                    status = StatusOfTable.Occupied; //defalut occupied
                }

                Table table = new Table()
                {
                    TableId = Convert.ToInt32(dr["TableId"]),
                    Status = status,
                    Capacity = Convert.ToInt32(dr["Capacity"]),
                };
                tables.Add(table);
            }
            return tables;
        }

        public void ChangeTableStatusToFree(int tableId)
        {
            string query = "UPDATE [TABLE] SET Status = @Status WHERE TableId = @TableId";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Status", StatusOfTable.Free.ToString()),
                new SqlParameter("@TableId", tableId)
    };

            ExecuteEditQuery(query, sqlParameters);
        }
        public void ChangeTableStatusToOccupied(int tableId)
        {
            string query = "UPDATE [TABLE] SET Status = @Status WHERE TableId = @TableId";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Status", StatusOfTable.Occupied.ToString()),
                new SqlParameter("@TableId", tableId)
    };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteTable(Table table)
        {
            string query = "DELETE FROM TABLE WHERE TableId=@id;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@id", table.TableId)
            };
            ExecuteEditQuery(query, sqlParameters);
        }
        public void AddTable(Table table)
        {
            string query = "INSERT INTO TABLE (TableId, Status, Capacity) VALUES (@TableNumber, @Status, @Capacity);";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@TableId", table.TableId),
            new SqlParameter("@Status", table.Status),
            new SqlParameter("@Capacity", table.Capacity),

        };
            ExecuteEditQuery(query, sqlParameters);
        }
        public List<Table> GetByStatus(string status)
        {
            List<Table> tables = new List<Table>();

            string query = "SELECT * FROM TABLE WHERE Status = @Status;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Status", status)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow dr in dataTable.Rows)
            {
                Table table = new Table()
                {
                    TableId = Convert.ToInt32(dr["TableId"]),
                    Status = (StatusOfTable)Enum.Parse(typeof(StatusOfTable), dr["Status"].ToString()),
                    Capacity = Convert.ToInt32(dr["Capacity"]),
                };
                tables.Add(table);
            }

            return tables;


        }
        public StatusOfTable GetStatusByTableId(int tableId)
        {
            string query = "SELECT Status FROM [TABLE] WHERE TableId = @TableId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@TableId", tableId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                string statusString = row["Status"].ToString();
                if (Enum.TryParse(statusString, out StatusOfTable status))
                {
                    return status;
                }
            }

            // Return a default status if no data found
            return StatusOfTable.Occupied;
        }
    }
}
