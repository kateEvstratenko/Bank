namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsClosedToCreditAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerCredits", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerCredits", "IsClosed");
        }
    }
}
