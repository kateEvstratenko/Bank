namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Country = c.String(),
                        City = c.String(),
                        House = c.String(),
                        Flat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Patronymic = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        IdenticationNumber = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        WrongPasswordCount = c.Int(nullable: false),
                        StartBlockDateTime = c.DateTime(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 200),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.Number)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainSum = c.Double(nullable: false),
                        PercentSum = c.Double(nullable: false),
                        DelaySum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        PersonCreditId = c.Int(nullable: false),
                        SourceBillId = c.Int(),
                        DestinationBillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.DestinationBillId, cascadeDelete: true)
                .ForeignKey("dbo.PersonCredits", t => t.PersonCreditId, cascadeDelete: true)
                .ForeignKey("dbo.Bills", t => t.SourceBillId)
                .Index(t => t.PersonCreditId)
                .Index(t => t.SourceBillId)
                .Index(t => t.DestinationBillId);
            
            CreateTable(
                "dbo.PersonCredits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(),
                        CreditSum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        CreditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credits", t => t.CreditId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
                .Index(t => t.PersonId)
                .Index(t => t.CreditId);
            
            CreateTable(
                "dbo.Credits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PercentRate = c.Int(nullable: false),
                        MinSum = c.Double(nullable: false),
                        MaxSum = c.Double(nullable: false),
                        MinPeriod = c.Time(nullable: false, precision: 7),
                        MaxPeriod = c.Time(nullable: false, precision: 7),
                        LoanPeriod = c.Time(nullable: false, precision: 7),
                        PaymentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.CreditRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreditGoal = c.String(),
                        Sum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        CreditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credits", t => t.CreditId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.CreditId);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepositRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        DepositId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deposits", t => t.DepositId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.DepositId);
            
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        InterestRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(),
                        InitialSum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        DepositId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deposits", t => t.DepositId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.DepositId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepositRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.PersonDeposits", "PersonId", "dbo.People");
            DropForeignKey("dbo.PersonDeposits", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.DepositRequests", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropForeignKey("dbo.Bills", "PersonId", "dbo.People");
            DropForeignKey("dbo.Payments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.PersonCredits", "PersonId", "dbo.People");
            DropForeignKey("dbo.Payments", "PersonCreditId", "dbo.PersonCredits");
            DropForeignKey("dbo.PersonCredits", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.Credits", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.CreditRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.CreditRequests", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.Payments", "DestinationBillId", "dbo.Bills");
            DropIndex("dbo.PersonDeposits", new[] { "DepositId" });
            DropIndex("dbo.PersonDeposits", new[] { "PersonId" });
            DropIndex("dbo.DepositRequests", new[] { "DepositId" });
            DropIndex("dbo.DepositRequests", new[] { "PersonId" });
            DropIndex("dbo.CreditRequests", new[] { "CreditId" });
            DropIndex("dbo.CreditRequests", new[] { "PersonId" });
            DropIndex("dbo.Credits", new[] { "PaymentTypeId" });
            DropIndex("dbo.PersonCredits", new[] { "CreditId" });
            DropIndex("dbo.PersonCredits", new[] { "PersonId" });
            DropIndex("dbo.Payments", new[] { "DestinationBillId" });
            DropIndex("dbo.Payments", new[] { "SourceBillId" });
            DropIndex("dbo.Payments", new[] { "PersonCreditId" });
            DropIndex("dbo.Bills", new[] { "PersonId" });
            DropIndex("dbo.Bills", new[] { "Number" });
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropTable("dbo.PersonDeposits");
            DropTable("dbo.Deposits");
            DropTable("dbo.DepositRequests");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.CreditRequests");
            DropTable("dbo.Credits");
            DropTable("dbo.PersonCredits");
            DropTable("dbo.Payments");
            DropTable("dbo.Bills");
            DropTable("dbo.People");
            DropTable("dbo.Addresses");
        }
    }
}
