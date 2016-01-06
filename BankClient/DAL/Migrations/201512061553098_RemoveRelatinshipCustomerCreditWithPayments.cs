namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelatinshipCustomerCreditWithPayments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditPayments", "CustomerCredit_Id", "dbo.CustomerCredits");
            DropForeignKey("dbo.CustomerCredits", "BillId", "dbo.Bills");
            DropIndex("dbo.CustomerCredits", new[] { "BillId" });
            DropIndex("dbo.CreditPayments", new[] { "CustomerCredit_Id" });
            AlterColumn("dbo.CustomerCredits", "BillId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerCredits", "BillId");
            AddForeignKey("dbo.CustomerCredits", "BillId", "dbo.Bills", "Id", cascadeDelete: false);
            DropColumn("dbo.CreditPayments", "CustomerCredit_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreditPayments", "CustomerCredit_Id", c => c.Int());
            DropForeignKey("dbo.CustomerCredits", "BillId", "dbo.Bills");
            DropIndex("dbo.CustomerCredits", new[] { "BillId" });
            AlterColumn("dbo.CustomerCredits", "BillId", c => c.Int());
            CreateIndex("dbo.CreditPayments", "CustomerCredit_Id");
            CreateIndex("dbo.CustomerCredits", "BillId");
            AddForeignKey("dbo.CustomerCredits", "BillId", "dbo.Bills", "Id");
            AddForeignKey("dbo.CreditPayments", "CustomerCredit_Id", "dbo.CustomerCredits", "Id");
        }
    }
}
