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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankClients",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Phone = c.String(),
                        MilitaryIdPath = c.String(),
                        IncomeCertificatePath = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);
            
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
                "dbo.CreditPayments",
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
                "dbo.DepositPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        PersonDepositId = c.Int(nullable: false),
                        SourceBillId = c.Int(nullable: false),
                        DestinationBillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.DestinationBillId, cascadeDelete: true)
                .ForeignKey("dbo.PersonDeposits", t => t.PersonDepositId, cascadeDelete: true)
                .ForeignKey("dbo.Bills", t => t.SourceBillId, cascadeDelete: false)
                .Index(t => t.PersonDepositId)
                .Index(t => t.SourceBillId)
                .Index(t => t.DestinationBillId);
            
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
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PersonId = c.Int(nullable: false),
                        Password = c.String(),
                        WrongPasswordCount = c.Int(nullable: false),
                        StartBlockDateTime = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropForeignKey("dbo.Bills", "PersonId", "dbo.People");
            DropForeignKey("dbo.DepositPayments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.PersonDeposits", "PersonId", "dbo.People");
            DropForeignKey("dbo.DepositPayments", "PersonDepositId", "dbo.PersonDeposits");
            DropForeignKey("dbo.PersonDeposits", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.DepositRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.DepositRequests", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.DepositPayments", "DestinationBillId", "dbo.Bills");
            DropForeignKey("dbo.CreditPayments", "SourceBillId", "dbo.Bills");
            DropForeignKey("dbo.PersonCredits", "PersonId", "dbo.People");
            DropForeignKey("dbo.CreditPayments", "PersonCreditId", "dbo.PersonCredits");
            DropForeignKey("dbo.PersonCredits", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.Credits", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.CreditRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.CreditRequests", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.CreditPayments", "DestinationBillId", "dbo.Bills");
            DropForeignKey("dbo.BankClients", "PersonId", "dbo.People");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DepositRequests", new[] { "DepositId" });
            DropIndex("dbo.DepositRequests", new[] { "PersonId" });
            DropIndex("dbo.PersonDeposits", new[] { "DepositId" });
            DropIndex("dbo.PersonDeposits", new[] { "PersonId" });
            DropIndex("dbo.DepositPayments", new[] { "DestinationBillId" });
            DropIndex("dbo.DepositPayments", new[] { "SourceBillId" });
            DropIndex("dbo.DepositPayments", new[] { "PersonDepositId" });
            DropIndex("dbo.CreditRequests", new[] { "CreditId" });
            DropIndex("dbo.CreditRequests", new[] { "PersonId" });
            DropIndex("dbo.Credits", new[] { "PaymentTypeId" });
            DropIndex("dbo.PersonCredits", new[] { "CreditId" });
            DropIndex("dbo.PersonCredits", new[] { "PersonId" });
            DropIndex("dbo.CreditPayments", new[] { "DestinationBillId" });
            DropIndex("dbo.CreditPayments", new[] { "SourceBillId" });
            DropIndex("dbo.CreditPayments", new[] { "PersonCreditId" });
            DropIndex("dbo.Bills", new[] { "PersonId" });
            DropIndex("dbo.Bills", new[] { "Number" });
            DropIndex("dbo.BankClients", new[] { "PersonId" });
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DepositRequests");
            DropTable("dbo.Deposits");
            DropTable("dbo.PersonDeposits");
            DropTable("dbo.DepositPayments");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.CreditRequests");
            DropTable("dbo.Credits");
            DropTable("dbo.PersonCredits");
            DropTable("dbo.CreditPayments");
            DropTable("dbo.Bills");
            DropTable("dbo.BankClients");
            DropTable("dbo.People");
            DropTable("dbo.Addresses");
        }
    }
}