namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameInTokenToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tokens", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tokens", "UserId", c => c.Int(nullable: false));
        }
    }
}
