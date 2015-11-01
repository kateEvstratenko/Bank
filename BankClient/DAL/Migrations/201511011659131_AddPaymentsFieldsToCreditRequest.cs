namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentsFieldsToCreditRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreditRequests", "OtherCreditPayments", c => c.Double(nullable: false));
            AddColumn("dbo.CreditRequests", "UtilitiesPayments", c => c.Double(nullable: false));
            AddColumn("dbo.CreditRequests", "OtherPayments", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditRequests", "OtherPayments");
            DropColumn("dbo.CreditRequests", "UtilitiesPayments");
            DropColumn("dbo.CreditRequests", "OtherCreditPayments");
        }
    }
}
