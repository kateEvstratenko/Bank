namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDebts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Debts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MainSum = c.Double(nullable: true),
                        PercentSum = c.Double(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CreditPaymentPlanItems", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Debts", "Id", "dbo.CreditPaymentPlanItems");
            DropIndex("dbo.Debts", new[] { "Id" });
            DropTable("dbo.Debts");
        }
    }
}
