using Sph.WebSite.Entities;

namespace Sph.WebSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sph.WebSite.DataAccess.SphDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Sph.WebSite.DataAccess.SphDataContext context)
        {
            context.StockItems.AddOrUpdate(p => p.StockCode, 
                new StockItem { StockCode = "CS-ST-MS290", Description = "STIHL MS 290 Petrol Chainsaw"},
                new StockItem { StockCode = "RYOB-NG-200W", Description = "Ryobi Electric Staple & Nail Gun 200 Watt" }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
