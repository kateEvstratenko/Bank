namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statussesToCredirRequestAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreditRequests", "CreditRequestStatusId", "dbo.CreditRequestStatus");
            DropIndex("dbo.CreditRequests", new[] { "CreditRequestStatusId" });
            AddColumn("dbo.CreditRequestStatus", "CreditRequestId", c => c.Int(nullable: false));
            AddColumn("dbo.CreditRequestStatus", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CreditRequestStatus", "CreditRequestId");
            CreateIndex("dbo.CreditRequestStatus", "AppUser_Id");
            AddForeignKey("dbo.CreditRequestStatus", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CreditRequestStatus", "CreditRequestId", "dbo.CreditRequests", "Id", cascadeDelete: true);
            DropColumn("dbo.CreditRequests", "CreditRequestStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreditRequests", "CreditRequestStatusId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CreditRequestStatus", "CreditRequestId", "dbo.CreditRequests");
            DropForeignKey("dbo.CreditRequestStatus", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CreditRequestStatus", new[] { "AppUser_Id" });
            DropIndex("dbo.CreditRequestStatus", new[] { "CreditRequestId" });
            DropColumn("dbo.CreditRequestStatus", "AppUser_Id");
            DropColumn("dbo.CreditRequestStatus", "CreditRequestId");
            CreateIndex("dbo.CreditRequests", "CreditRequestStatusId");
            AddForeignKey("dbo.CreditRequests", "CreditRequestStatusId", "dbo.CreditRequestStatus", "Id", cascadeDelete: true);
        }
    }
}
