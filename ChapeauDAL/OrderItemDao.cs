﻿using ChapeauModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauDAL
{
    public class OrderItemDao:BaseDao
    {
        private readonly MenuItemDao _menuItemDao = new MenuItemDao();
        private readonly OrderDao _orderDao;

        public OrderItemDao(OrderDao orderDao)
        {
            _orderDao = orderDao;
        }
        public OrderItemDao()
        {

        }
        public OrderItem GetOrderItemByMenuItemID(int menuItemId)
        {
            
            OrderItem orderItem = null;
            string query = "SELECT * FROM [ORDER_ITEM] WHERE MenuItemId = @MenuItemId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@MenuItemId", menuItemId)
            };
            
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                int orderId = Convert.ToInt32(row["OrderId"]);

                MenuItem menuItem = _menuItemDao.GetMenuItemById(menuItemId);
                Order order = _orderDao.GetOrderById(orderId);

                orderItem = new OrderItem
                {
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    MenuItem = menuItem,
                    Order = order,
                    Count = Convert.ToInt32(row["Count"]),
                    Status = (StatusOfOrderitem)Enum.Parse(typeof(StatusOfOrderitem), row["Status"].ToString())
                };
            }

            return orderItem;
        }
        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {   
            List<OrderItem> orderItems = new List<OrderItem>();
            string query = "SELECT * FROM [ORDER_ITEM] WHERE OrderId = @OrderId;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@OrderId", orderId)
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                int menuItemId = Convert.ToInt32(row["MenuItemId"]);
                MenuItem menuItem = _menuItemDao.GetMenuItemById(menuItemId);

                OrderItem orderItem = new OrderItem
                {
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    MenuItem = menuItem,
                    Order = new Order { OrderId = orderId },
                    Count = Convert.ToInt32(row["Count"]),
                    Status = (StatusOfOrderitem)Enum.Parse(typeof(StatusOfOrderitem), row["Status"].ToString())
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }
        public List<OrderItem> GetDrinkOrderItems()
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            string query = @"
            SELECT oi.OrderId, oi.MenuItemId, oi.Comment, oi.OrderTime, oi.Count, oi.Status, mi.Name 
            FROM [ORDER_ITEM] oi
            JOIN [MENU_ITEM] mi ON oi.MenuItemId = mi.MenuItemId
            WHERE mi.Card = @Card";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Card", "drink")
            };

            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in dataTable.Rows)
            {
                int menuItemId = Convert.ToInt32(row["MenuItemId"]);
                MenuItem menuItem = new MenuItem
                {
                    MenuItemId = menuItemId,
                    Name = row["Name"].ToString()
                };

                OrderItem orderItem = new OrderItem
                {
                    Order = _orderDao.GetOrderById(Convert.ToInt32(row["OrderId"])),
                    MenuItem = menuItem,
                    Comment = row["Comment"].ToString(),
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    Count = Convert.ToInt32(row["Count"]),
                    Status = (StatusOfOrderitem)Enum.Parse(typeof(StatusOfOrderitem), row["Status"].ToString())
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }
    }
}