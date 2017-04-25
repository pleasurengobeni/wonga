using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor.Entities;
using Refactor.Services;

namespace Refactor
{
    public class OrderService
    {
        public void AddToCart(Order order, string stockCode, int qty)
        {
            var stockItems = Database.Instance.Items<StockItem>().Where(x =>
                x.StockCode == stockCode &&
                x.Order == null).ToList();

            var orderItem = order.OrderItems.FirstOrDefault(x => x.StockCode == stockCode);

            if (orderItem == null)
            {
                orderItem = new OrderStockItem()
                {
                    Qty = qty,
                    StockCode = stockCode
                };
                order.OrderItems.Add(orderItem);
            }

            var i = 0;
            foreach (var item in stockItems)
            {
                i++;

                orderItem.StockItems.Add(item);
                item.Order = order;
                if (orderItem.Qty == orderItem.StockItems.Count)
                {
                    break;
                }
            }

            if (orderItem.Qty == orderItem.StockItems.Count)
            {
                orderItem.StockStatuses.Add(StockStatus.InStock);
                order.Statuses.Remove(OrderStatus.WaitingForStock);
                order.Statuses.Remove(OrderStatus.InStock);
                order.Statuses.Add(OrderStatus.InStock);
            }
            else
            {
                orderItem.StockStatuses.Add(StockStatus.OutOfstock);
                order.Statuses.Remove(OrderStatus.WaitingForStock);
                order.Statuses.Add(OrderStatus.WaitingForStock);
            }
        }

        public void FillOrder(StockItem stockItem, int qty)
        {
            var orders = (
                from o in Database.Instance.Items<Order>()
                let orderItem =
                    o.OrderItems.FirstOrDefault(
                        x =>
                            x.StockCode == stockItem.StockCode &&
                            x.StockStatuses.Any(ss => ss == StockStatus.OutOfstock))
                where orderItem != null
                select new
                {
                    Order = o,
                    OrderItem = orderItem
                }).ToList();

            foreach (var order in orders)
            {
                var remainingQty = order.OrderItem.Qty - order.OrderItem.StockItems.Count;

                AddToCart(order.Order, stockItem.StockCode, remainingQty > qty ? qty : remainingQty);


                if (order.Order.Statuses.All(x => x != OrderStatus.WaitingForStock))
                {
                    new ShippingService().ShipOrder(order.Order);
                }
            }
        }

        public void FillOrder(Order order)
        {
            if (order.OrderItems.Count(x => x.StockStatuses.Any(ss => ss  == StockStatus.InStock)) == order.OrderItems.Count())
            {
                order.Statuses.Remove(OrderStatus.WaitingForStock);
                order.Statuses.Add(OrderStatus.InStock);
            }
        }
    }
}
