using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Sph.WebSite.Entities;

namespace Sph.WebSite.DataAccess
{
    public class SphDataContext : DbContext
    {
        public DbSet<StockItem> StockItems { get; set; }
    }
}