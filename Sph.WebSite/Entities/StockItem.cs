using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sph.WebSite.Entities
{
    public class StockItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockItemId { get; set; }

        public string StockCode { get; set; }
        public string Description { get; set; }
    }
}