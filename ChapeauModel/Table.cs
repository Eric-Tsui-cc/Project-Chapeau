using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum StatusOfTable
    {
        Free,
        Occupied
    }
    public class Table
    {
        public int tableId { get; set; }
        public string tableName { get; set; }
        public StatusOfTable status { get; set; }
        public Table() { }
        public Table(int tableId, string tableName, StatusOfTable status)
        {
            this.tableId = tableId;
            this.tableName = tableName;
            this.status = status;
        }
    }
}
