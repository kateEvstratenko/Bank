namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIOToCustomerAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Firstname", c => c.String());
            AddColumn("dbo.Customers", "Lastname", c => c.String());
            AddColumn("dbo.Customers", "Patronymic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Patronymic");
            DropColumn("dbo.Customers", "Lastname");
            DropColumn("dbo.Customers", "Firstname");
        }
    }
}
