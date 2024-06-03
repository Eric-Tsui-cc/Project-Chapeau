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
                    TableNumber = Convert.ToInt32(row["TableId"]),
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
                    TableNumber = Convert.ToInt32(dr["TableNumber"]),
                };
                tables.Add(table);
            }
            return tables;
        }

        public void ChangeTableStatus(Table table)
        {
            string query = "UPDATE TABLE " +
                "SET Status = @Status" +
                "WHERE TableId=@id, TableNumber=@TableNumber ";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@id", table.TableId),
            new SqlParameter("@TableNumber", table.TableNumber),
            new SqlParameter("@Status", table.Status),

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
            string query = "INSERT INTO TABLE (TableId, Status, TableNumber) VALUES (@TableNumber, @Status, @TableNumber);";
            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@TableId", table.TableId),
            new SqlParameter("@Status", table.Status),
            new SqlParameter("@TableNumber", table.TableNumber),

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
                    TableNumber = Convert.ToInt32(dr["TableNumber"]),
                };
                tables.Add(table);
            }

            return tables;


        }
    }
}
