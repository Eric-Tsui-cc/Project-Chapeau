using ChapeauDAL;
using ChapeauModel;

namespace ChapeauService
{
    public class OrderService_Overview
    {
        private OrderDao orderDao;

        public OrderService_Overview()
        {
            orderDao = new OrderDao();
        }

        /* Get All Orders */
        public List<Order> GetAllOrders()
        {
            return orderDao.GetAllOrders();
        }

        public Order GetOrderById(int orderId)
        {
            return orderDao.GetOrderById(orderId);
        }

        // Change Order Status
        public void ChangeOrderStatus(Order order)
        {
            orderDao.UpdateOrderStatus(order);
        }

        public List<Order> SortOrders(List<Order> orders, string sortBy)
        {
            switch (sortBy)
            {
                case "ReadyToServe":
                    return orders
                        .Where(o => o.Status == Order.OrderStatus.Done)
                        .OrderByDescending(o => o.GetWaitingTime())
                        .ThenBy(o => o.TableId)
                        .ToList();
                case "Preparing":
                    return orders
                        .Where(o => o.Status == Order.OrderStatus.Preparing)
                        .OrderByDescending(o => o.GetWaitingTime())
                        .ThenBy(o => o.TableId)
                        .ToList();
                case "LongestWaitingTime":
                    return orders
                        .OrderByDescending(o => o.GetWaitingTime())
                        .ToList();
                case "TableIdAsc":
                    return orders
                        .OrderBy(o => o.TableId)
                        .ToList();
                case "TableIdDesc":
                    return orders
                        .OrderByDescending(o => o.TableId)
                        .ToList();
                case "All":
                    return orders;
                default:
                    return orders;
            }
        }
    }
}
