namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdRenamed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerId" });
            RenameColumn(table: "dbo.Addresses", name: "CustomerId", newName: "Id");
            RenameColumn(table: "dbo.BankClients", name: "CustomerId", newName: "Id");
            RenameIndex(table: "dbo.Addresses", name: "IX_CustomerId", newName: "IX_Id");
            RenameIndex(table: "dbo.BankClients", name: "IX_CustomerId", newName: "IX_Id");
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Login = c.String(),
                        Guid = c.Guid(nullable: false),
                        GenerationDate = c.DateTime(nullable: false),
                        IsExpired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Patronymic", c => c.String());
            AlterColumn("dbo.AspNetUsers", "CustomerId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CustomerId");
            AddForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers", "Id");
            DropColumn("dbo.Customers", "Firstname");
            DropColumn("dbo.Customers", "Lastname");
            DropColumn("dbo.Customers", "Patronymic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Patronymic", c => c.String());
            AddColumn("dbo.Customers", "Lastname", c => c.String());
            AddColumn("dbo.Customers", "Firstname", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerId" });
            AlterColumn("dbo.AspNetUsers", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Patronymic");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropTable("dbo.Tokens");
            RenameIndex(table: "dbo.BankClients", name: "IX_Id", newName: "IX_CustomerId");
            RenameIndex(table: "dbo.Addresses", name: "IX_Id", newName: "IX_CustomerId");
            RenameColumn(table: "dbo.BankClients", name: "Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Addresses", name: "Id", newName: "CustomerId");
            CreateIndex("dbo.AspNetUsers", "CustomerId");
            AddForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
