namespace Sph.WebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        StockItemId = c.Int(nullable: false, identity: true),
                        StockCode = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StockItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockItems");
        }
    }
}
