using ChapeauDAL;
using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauService
{
    public class KitchenBarService
    {
        private OrderDao orderdb;
        private TableDao Tabledb;
        public KitchenBarService()
        {
            orderdb = new OrderDao();
            Tabledb = new TableDao();
        }
        public List<OrderItem> GetAllRunningDrinkOrderItem()
        {
            return orderdb.GetAllRunningDrinkOrderItem();
        }
        public List<OrderItem> GetAllRunningFoodOrderItem()
        {
            return orderdb.GetAllRunningFoodOrderItem();
        }
        public Table GetTableByOrderId(int orderId)
        {
            return Tabledb.GetTableByOrderId(orderId);
        }
        public List<OrderItem> GetAllTodayServedDrinkOrderItem()
        {
            return orderdb.GetAllTodayServedDrinkOrderItem();
        }
        public List<OrderItem> GetAllTodayServedFoodOrderItem()
        {
            return orderdb.GetAllTodayServedFoodOrderItem();
        }
    }
}
