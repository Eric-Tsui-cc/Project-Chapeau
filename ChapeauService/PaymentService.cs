using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class PaymentService
    {
        private OrderDao orderdb;
        private TableDao tabledb;
        private OrderItemDao orderItemdb;
        private BillDao billdb;
        public PaymentService()   // Initializes instances of the DAOs
        {   
            orderItemdb = new OrderItemDao();
            orderdb = new OrderDao(orderItemdb);
            tabledb = new TableDao();
            billdb = new BillDao();

        }
        public void MarkOrderAsPaid(int orderId)
        {
            orderdb.MarkOrderAsPaid(orderId);
        }
        public List<Table> GetAllOccupiedTables()
        {
            return tabledb.GetAllOccupiedTables();
        }
        public List<Order> GetUnpaidOrdersByTableId(int tableId)
        {
            return orderdb.GetUnpaidOrdersByTableId(tableId);
        }
        public void ChangeTableStatusToFree(int tableId)
        {
            tabledb.ChangeTableStatusToFree(tableId);
        }
        public void FinalizePayment(Bill bill)
        {
            billdb.FinalizePayment(bill);
        }
        
    }
}
