namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PercentRateWasChangedToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credits", "PercentRate", c => c.Double(nullable: false));
            AlterColumn("dbo.Deposits", "InterestRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deposits", "InterestRate", c => c.Int(nullable: false));
            AlterColumn("dbo.Credits", "PercentRate", c => c.Int(nullable: false));
        }
    }
}
