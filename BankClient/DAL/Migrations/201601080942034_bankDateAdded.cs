namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bankDateAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlobalValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GlobalValues");
        }
    }
}
