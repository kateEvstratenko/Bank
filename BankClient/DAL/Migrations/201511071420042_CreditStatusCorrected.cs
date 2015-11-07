namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditStatusCorrected : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CreditRequestStatus", name: "AppUser_Id", newName: "AppUserId");
            RenameIndex(table: "dbo.CreditRequestStatus", name: "IX_AppUser_Id", newName: "IX_AppUserId");
            DropColumn("dbo.CreditRequestStatus", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreditRequestStatus", "UserId", c => c.String());
            RenameIndex(table: "dbo.CreditRequestStatus", name: "IX_AppUserId", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.CreditRequestStatus", name: "AppUserId", newName: "AppUser_Id");
        }
    }
}
