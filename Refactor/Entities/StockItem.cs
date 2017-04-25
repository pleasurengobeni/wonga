using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactor.Entities
{
    public class StockItem
    {
        public string StockCode { get; set; }
        public DateTime ReceivedOn { get; set; }
        public Order Order { get; set; }
    }
}
