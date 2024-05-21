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
        public int TableId { get; set; }
        public string TableNr { get; set; }
        public StatusOfTable Status { get; set; }
        public Table() { }
        public Table(int TableId, string TableNr, StatusOfTable Status)
        {
            this.TableId = TableId;
            this.TableNr = TableNr;
            this.Status = Status;
        }
    }
}
