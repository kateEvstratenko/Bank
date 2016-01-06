namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecretCodeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SecretCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SecretCode");
        }
    }
}
