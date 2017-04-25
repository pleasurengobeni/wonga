class Program
{
		/// <summary>
		/// An
		/// Refactor the system to allow better unit testability so that we can verify the email communications sent to the customer.
		/// Refactor any part as you see fit (create new objects, rename existing ones)
		/// Third party libraries or software should not be required with this excersize
		/// Replace manual steps of testing for emails marked with TODO with automated tests
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			var order = new Order()
			{
				EmailAddress = "eric.cartman@southpark.com"
			};

			var orderService = new OrderService();
			orderService.AddToCart(order, "DRML-3000-BR", 1);
			orderService.AddToCart(order, "SS-SSD-850-250", 3);
			
			Database.Instance.Add(order);


			var stockService = new StockService();

			stockService.StockReceived(new StockItem()
			{
				StockCode = "SS-SSD-850-250"
			});

			Assert(() => !order.Statuses.Contains(OrderStatus.EnRoute), "Expect order not to be shipped" );

			// TODO: Refactor this so the test can be automated
			Console.WriteLine("Please check your inbox and ensure that the shipping service did not send an email, press any key to continue...");
			Console.ReadKey();

			stockService.StockReceived(new StockItem()
			{
				StockCode = "SS-SSD-850-250"
			});

			Assert(() => order.Statuses.Contains(OrderStatus.EnRoute), "Expect order to be shipped");

			// TODO: Refactor this so the test can be automated
			Console.WriteLine("Please check your inbox and ensure that the shipping service sent a confirmation email, press any key to continue...");
			Console.ReadKey();

		}

		static void Assert(Func<bool> assert, string error)
		{
			if (!assert())
			{
				throw new Exception(error);
			}
		}
	}
}


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

public class StockItem
{
	public string StockCode { get; set; }
	public DateTime ReceivedOn { get; set; }
	public Order Order { get; set; }
}

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
	
public class EmailUtility
{
	public static bool SendEmail(string mailto, string subject, string Message)
	{
		bool EmailSent = false;
		try
		{
			System.Net.NetworkCredential NetworkCredential = new System.Net.NetworkCredential();
			NetworkCredential.Password = ConfigurationManager.AppSettings["SMTPpassword"];
			NetworkCredential.UserName = ConfigurationManager.AppSettings["SMTPuser"];
			System.Net.Mail.MailMessage mailmsg = new System.Net.Mail.MailMessage();

			mailmsg.Body = Message;
			mailmsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["SMTPFrom"]);
			mailmsg.To.Add(mailto);
			System.Net.Mail.SmtpClient s = new System.Net.Mail.SmtpClient();
			s.UseDefaultCredentials = false;
			s.Credentials = NetworkCredential;
			s.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
			s.EnableSsl = false;
			s.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPserverport"]);
			s.Host = ConfigurationManager.AppSettings["SMTPserver"];
			
			s.Send(mailmsg);
			mailmsg = null;
		}
		catch (Exception ex)
		{
			EmailSent = false;
			Console.WriteLine(ex.ToString());
		}
		return EmailSent;
	}
}

public class OrderSchedulerService
{
	public void ScheduleOrderForDelivery(Order order)
	{
		if (!order.Statuses.Any(x => new[] {OrderStatus.InStock}.Any()))
		{
			order.Statuses.Add(OrderStatus.ScheduledForDelivery);
		}
	}
}

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
