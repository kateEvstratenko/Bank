namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerIdToBill : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            AlterColumn("dbo.Bills", "CustomerId", c => c.Int());
            CreateIndex("dbo.Bills", "CustomerId");
            AddForeignKey("dbo.Bills", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            AlterColumn("dbo.Bills", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "CustomerId");
            AddForeignKey("dbo.Bills", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
