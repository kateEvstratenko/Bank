namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIncomeSumToCreditRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreditRequests", "IncomeSum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditRequests", "IncomeSum");
        }
    }
}
