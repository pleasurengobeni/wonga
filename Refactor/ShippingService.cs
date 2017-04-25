using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor.Entities;

namespace Refactor
{
    public class ShippingService
    {

        public void ShipOrder(Order order)
        {
            order.Statuses.Add(OrderStatus.EnRoute);
            EmailUtility.SendEmail(order.EmailAddress, "Order shipped", "Great news! Your order has been shipped");
        }

        public void ConfirmDelivery(Order order)
        {
            order.Statuses.Add(OrderStatus.Delivered);
        }
    }
}
