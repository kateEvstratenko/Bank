namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creditAndCreditRequestRelationShip : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DepositPayments", "SourceBillId", "dbo.Bills");
            DropIndex("dbo.DepositPayments", new[] { "SourceBillId" });
            AddColumn("dbo.CustomerCredits", "CreditRequestId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerDeposits", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.DepositPayments", "SourceBillId", c => c.Int());
            CreateIndex("dbo.CustomerCredits", "CreditRequestId");
            CreateIndex("dbo.DepositPayments", "SourceBillId");
            AddForeignKey("dbo.CustomerCredits", "CreditRequestId", "dbo.CreditRequests", "Id", cascadeDelete: false);
            AddForeignKey("dbo.DepositPayments", "SourceBillId", "dbo.Bills", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepositPayments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.CustomerCredits", "CreditRequestId", "dbo.CreditRequests");
            DropIndex("dbo.DepositPayments", new[] { "SourceBillId" });
            DropIndex("dbo.CustomerCredits", new[] { "CreditRequestId" });
            AlterColumn("dbo.DepositPayments", "SourceBillId", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerDeposits", "IsPaid");
            DropColumn("dbo.CustomerCredits", "CreditRequestId");
            CreateIndex("dbo.DepositPayments", "SourceBillId");
            AddForeignKey("dbo.DepositPayments", "SourceBillId", "dbo.Bills", "Id", cascadeDelete: true);
        }
    }
}
