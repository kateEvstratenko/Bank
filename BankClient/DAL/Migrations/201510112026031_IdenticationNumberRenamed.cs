using System.Data.Entity.Migrations;

namespace DAL.Migrations
{
    public partial class IdenticationNumberRenamed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DocumentNumber", c => c.String());
            AddColumn("dbo.Customers", "IdentificationNumber", c => c.String());
            DropColumn("dbo.Customers", "IdenticationNumber");
            DropColumn("dbo.AspNetUsers", "WrongPasswordCount");
            DropColumn("dbo.AspNetUsers", "EndBlockDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "EndBlockDateTime", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "WrongPasswordCount", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "IdenticationNumber", c => c.String());
            DropColumn("dbo.Customers", "IdentificationNumber");
            DropColumn("dbo.Customers", "DocumentNumber");
        }
    }
}
