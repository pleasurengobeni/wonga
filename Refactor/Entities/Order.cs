using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactor.Entities
{
    public class Order
    {
        public string EmailAddress { get; set; }

        public IList<OrderStockItem> OrderItems { get; set; }
        
        public IList<OrderStatus> Statuses { get; set; }

       
        public Order()
        {
            OrderItems = new List<OrderStockItem>();
            Statuses = new List<OrderStatus>();
        }
    }

    public class OrderStockItem
    {
        public IList<StockItem> StockItems { get; set; }

        public string StockCode { get; set; }

        public IList<StockStatus> StockStatuses { get; set; }

        public int Qty { get; set; }

        public OrderStockItem()
        {
            StockItems = new List<StockItem>();
            StockStatuses = new List<StockStatus>();
        }
    }

    public enum StockStatus
    {
        InStock,
        Ordered,
        OutOfstock
    }

    public enum OrderStatus
    {
        New,
        WaitingForStock,
        InStock,
        ScheduledForDelivery,
        EnRoute,
        Delivered 
    }
}
