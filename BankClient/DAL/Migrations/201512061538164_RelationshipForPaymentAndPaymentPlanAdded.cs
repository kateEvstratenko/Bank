namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipForPaymentAndPaymentPlanAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.CreditPayments", "CustomerCreditId", "dbo.CustomerCredits");
            DropIndex("dbo.CreditPayments", new[] { "CustomerCreditId" });
            DropIndex("dbo.CreditPayments", new[] { "SourceBillId" });
            RenameColumn(table: "dbo.CreditPayments", name: "CustomerCreditId", newName: "CustomerCredit_Id");
            AddColumn("dbo.CreditPayments", "DelayMainSum", c => c.Double(nullable: false));
            AddColumn("dbo.CreditPayments", "DelayPercentSum", c => c.Double(nullable: false));
            AddColumn("dbo.CreditPayments", "CreditPaymentPlanItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditPayments", "CustomerCredit_Id", c => c.Int());
            AlterColumn("dbo.CreditPayments", "SourceBillId", c => c.Int(nullable: false));
            CreateIndex("dbo.CreditPayments", "CreditPaymentPlanItemId");
            CreateIndex("dbo.CreditPayments", "SourceBillId");
            CreateIndex("dbo.CreditPayments", "CustomerCredit_Id");
            AddForeignKey("dbo.CreditPayments", "CreditPaymentPlanItemId", "dbo.CreditPaymentPlanItems", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills", "Id", cascadeDelete: false);
            AddForeignKey("dbo.CreditPayments", "CustomerCredit_Id", "dbo.CustomerCredits", "Id");
            DropColumn("dbo.CreditPaymentPlanItems", "DelaySum");
            DropColumn("dbo.CreditPayments", "DelaySum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreditPayments", "DelaySum", c => c.Double(nullable: false));
            AddColumn("dbo.CreditPaymentPlanItems", "DelaySum", c => c.Double(nullable: false));
            DropForeignKey("dbo.CreditPayments", "CustomerCredit_Id", "dbo.CustomerCredits");
            DropForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.CreditPayments", "CreditPaymentPlanItemId", "dbo.CreditPaymentPlanItems");
            DropIndex("dbo.CreditPayments", new[] { "CustomerCredit_Id" });
            DropIndex("dbo.CreditPayments", new[] { "SourceBillId" });
            DropIndex("dbo.CreditPayments", new[] { "CreditPaymentPlanItemId" });
            AlterColumn("dbo.CreditPayments", "SourceBillId", c => c.Int());
            AlterColumn("dbo.CreditPayments", "CustomerCredit_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CreditPayments", "CreditPaymentPlanItemId");
            DropColumn("dbo.CreditPayments", "DelayPercentSum");
            DropColumn("dbo.CreditPayments", "DelayMainSum");
            RenameColumn(table: "dbo.CreditPayments", name: "CustomerCredit_Id", newName: "CustomerCreditId");
            CreateIndex("dbo.CreditPayments", "SourceBillId");
            CreateIndex("dbo.CreditPayments", "CustomerCreditId");
            AddForeignKey("dbo.CreditPayments", "CustomerCreditId", "dbo.CustomerCredits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills", "Id");
        }
    }
}
