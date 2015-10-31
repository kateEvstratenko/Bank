namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimePeriodIsChanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.Int(nullable: false),
                        UserId = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Credits", "MinMonthPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.Credits", "MaxMonthPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.CreditRequests", "MilitaryIdPath", c => c.String());
            AddColumn("dbo.CreditRequests", "IncomeCertificatePath", c => c.String());
            AddColumn("dbo.CreditRequests", "CreditRequestStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.Deposits", "MinMonthPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.Deposits", "MaxMonthPeriod", c => c.Int(nullable: false));
            CreateIndex("dbo.CreditRequests", "CreditRequestStatusId");
            AddForeignKey("dbo.CreditRequests", "CreditRequestStatusId", "dbo.CreditRequestStatus", "Id", cascadeDelete: true);
            DropColumn("dbo.BankClients", "MilitaryIdPath");
            DropColumn("dbo.BankClients", "IncomeCertificatePath");
            DropColumn("dbo.Credits", "MinPeriod");
            DropColumn("dbo.Credits", "MaxPeriod");
            DropColumn("dbo.Credits", "LoanPeriod");
            DropColumn("dbo.Deposits", "MinPeriod");
            DropColumn("dbo.Deposits", "MaxPeriod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deposits", "MaxPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Deposits", "MinPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Credits", "LoanPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Credits", "MaxPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Credits", "MinPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.BankClients", "IncomeCertificatePath", c => c.String());
            AddColumn("dbo.BankClients", "MilitaryIdPath", c => c.String());
            DropForeignKey("dbo.CreditRequests", "CreditRequestStatusId", "dbo.CreditRequestStatus");
            DropIndex("dbo.CreditRequests", new[] { "CreditRequestStatusId" });
            DropColumn("dbo.Deposits", "MaxMonthPeriod");
            DropColumn("dbo.Deposits", "MinMonthPeriod");
            DropColumn("dbo.CreditRequests", "CreditRequestStatusId");
            DropColumn("dbo.CreditRequests", "IncomeCertificatePath");
            DropColumn("dbo.CreditRequests", "MilitaryIdPath");
            DropColumn("dbo.Credits", "MaxMonthPeriod");
            DropColumn("dbo.Credits", "MinMonthPeriod");
            DropTable("dbo.CreditRequestStatus");
        }
    }
}
