using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor.Entities;
using Refactor.Services;

namespace Refactor
{
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

            try
            {
                Assert(() => !order.Statuses.Contains(OrderStatus.EnRoute), "Expect order not to be shipped");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
