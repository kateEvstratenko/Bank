namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billSumAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills");
            DropIndex("dbo.CreditPayments", new[] { "SourceBillId" });
            AddColumn("dbo.Bills", "Sum", c => c.Double(nullable: false));
            AlterColumn("dbo.CreditPayments", "SourceBillId", c => c.Int());
            CreateIndex("dbo.CreditPayments", "SourceBillId");
            AddForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills");
            DropIndex("dbo.CreditPayments", new[] { "SourceBillId" });
            AlterColumn("dbo.CreditPayments", "SourceBillId", c => c.Int(nullable: false));
            DropColumn("dbo.Bills", "Sum");
            CreateIndex("dbo.CreditPayments", "SourceBillId");
            AddForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills", "Id", cascadeDelete: true);
        }
    }
}
