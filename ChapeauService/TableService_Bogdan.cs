using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class TableService_Bogdan
    {
        private TableDao tableDao;

        public TableService_Bogdan()
        {
            tableDao = new();
        }

        /*Get Tables*/

        public List<Table> GetAllTables()
        {
            return tableDao.GetAllTables();
        }

        public Table GetTableById(int tableId)
        {
            return tableDao.GetById(tableId);
        }
        //Change
        public void ChangeTableStatus(Table table)
        {
             tableDao.ChangeTableStatus(table);

        }
    }

}
