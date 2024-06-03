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
        public int TableNumber { get; set; }
        public StatusOfTable Status { get; set; }
        public Table() { }
        public Table(int TableId, int TableNumber, StatusOfTable Status)
        {
            this.TableId = TableId;
            this.TableNumber = TableNumber;
            this.Status = Status;
        }
    }
}
