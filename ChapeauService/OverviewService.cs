using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class OverviewService
    {
        private TableDao tabledb;
        
        public OverviewService()
        {
            tabledb = new TableDao();
        }
        public StatusOfTable GetStatusByTableId(int tableId)
        {
            return tabledb.GetStatusByTableId(tableId);
        }
        public Table GetTableById(int tableId)
        {
            return tabledb.GetTableById(tableId);
        }
        public void ChangeTableStatusToFree(int tableId)
        {
            tabledb.ChangeTableStatusToFree(tableId);
        }
        public void ChangeTableStatusToOccupied(int tableId)
        {
            tabledb.ChangeTableStatusToOccupied(tableId);
        }
    }
}
