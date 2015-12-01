namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsPlanItemAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditPaymentPlanItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainSum = c.Double(nullable: false),
                        PercentSum = c.Double(nullable: false),
                        DelaySum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        CustomerCreditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerCredits", t => t.CustomerCreditId, cascadeDelete: true)
                .Index(t => t.CustomerCreditId);
            
            AddColumn("dbo.CreditRequests", "MonthCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditPaymentPlanItems", "CustomerCreditId", "dbo.CustomerCredits");
            DropIndex("dbo.CreditPaymentPlanItems", new[] { "CustomerCreditId" });
            DropColumn("dbo.CreditRequests", "MonthCount");
            DropTable("dbo.CreditPaymentPlanItems");
        }
    }
}
