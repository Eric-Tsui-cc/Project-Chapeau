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
        private OrderDao orderdb;
        private OrderItemDao orderItemdb;

        public OverviewService()
        {
            tabledb = new TableDao();

            // Avoiding circular dependencies when creating OrderDao and OrderItemDao instances
            orderdb = new OrderDao(null); // temp for null
            orderItemdb = new OrderItemDao(orderdb);
            orderdb = new OrderDao(orderItemdb); // Reassign the value, passing the actual orderItemdb
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
        public Order GetOrderById(int orderId)
        {
            return orderdb.GetOrderById(orderId);
        }
        public List<int> GetRunningOrderIdsByTableId(int tableId)
        {
            return orderdb.GetRunningOrderIdsByTableId(tableId);
        }
        public void ChangeOrderStatusToServed(int orderId)
        {
            orderdb.ChangeOrderStatusToServed(orderId);
        }
        public bool HasRunningOrders(int tableId)
        {
            return orderdb.HasRunningOrders(tableId);
        }
    }
}
