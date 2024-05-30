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
    }
}
