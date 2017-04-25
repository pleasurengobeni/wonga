using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor.Entities;

namespace Refactor.Services
{
    public class StockService
    {
        public void StockReceived(StockItem stockItem)
        {
            Database.Instance.Add(stockItem);

            new OrderService().FillOrder(stockItem, 1);
        }

        public void StockAllocated(Order order, StockItem stockItem)
        {
            stockItem.Order = order;
        }
    }
}
