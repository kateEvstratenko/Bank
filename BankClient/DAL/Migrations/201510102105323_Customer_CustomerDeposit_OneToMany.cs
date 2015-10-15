using System.Data.Entity.Migrations;

namespace DAL.Migrations
{
    public partial class Customer_CustomerDeposit_OneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankClients", "PersonId", "dbo.People");
            DropForeignKey("dbo.CreditRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.PersonCredits", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.CreditPayments", "PersonCreditId", "dbo.PersonCredits");
            DropForeignKey("dbo.PersonCredits", "PersonId", "dbo.People");
            DropForeignKey("dbo.DepositRequests", "PersonId", "dbo.People");
            DropForeignKey("dbo.PersonDeposits", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.DepositPayments", "PersonDepositId", "dbo.PersonDeposits");
            DropForeignKey("dbo.PersonDeposits", "PersonId", "dbo.People");
            DropForeignKey("dbo.Bills", "PersonId", "dbo.People");
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropIndex("dbo.BankClients", new[] { "PersonId" });
            DropIndex("dbo.Bills", new[] { "PersonId" });
            DropIndex("dbo.CreditPayments", new[] { "PersonCreditId" });
            DropIndex("dbo.PersonCredits", new[] { "PersonId" });
            DropIndex("dbo.PersonCredits", new[] { "CreditId" });
            DropIndex("dbo.CreditRequests", new[] { "PersonId" });
            DropIndex("dbo.DepositPayments", new[] { "PersonDepositId" });
            DropIndex("dbo.PersonDeposits", new[] { "PersonId" });
            DropIndex("dbo.PersonDeposits", new[] { "DepositId" });
            DropIndex("dbo.DepositRequests", new[] { "PersonId" });
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            DropPrimaryKey("dbo.Addresses");
            DropPrimaryKey("dbo.BankClients");
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Patronymic = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        IdenticationNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerCredits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(),
                        CreditSum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreditId = c.Int(nullable: false),
                        BillId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId)
                .ForeignKey("dbo.Credits", t => t.CreditId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.CreditId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.CustomerDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(),
                        InitialSum = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DepositId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Deposits", t => t.DepositId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DepositId)
                .Index(t => t.BillId);
            
            AddColumn("dbo.Addresses", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "Street", c => c.String());
            AddColumn("dbo.BankClients", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.CreditPayments", "CustomerCreditId", c => c.Int(nullable: false));
            AddColumn("dbo.CreditRequests", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.DepositPayments", "CustomerDepositId", c => c.Int(nullable: false));
            AddColumn("dbo.Deposits", "MinSum", c => c.Double(nullable: false));
            AddColumn("dbo.Deposits", "MaxSum", c => c.Double(nullable: false));
            AddColumn("dbo.Deposits", "MinPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Deposits", "MaxPeriod", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.DepositRequests", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "EndBlockDateTime", c => c.DateTime());
            AlterColumn("dbo.Addresses", "Flat", c => c.Int());
            AddPrimaryKey("dbo.Addresses", "CustomerId");
            AddPrimaryKey("dbo.BankClients", "CustomerId");
            CreateIndex("dbo.Addresses", "CustomerId");
            CreateIndex("dbo.BankClients", "CustomerId");
            CreateIndex("dbo.Bills", "CustomerId");
            CreateIndex("dbo.CreditRequests", "CustomerId");
            CreateIndex("dbo.CreditPayments", "CustomerCreditId");
            CreateIndex("dbo.DepositRequests", "CustomerId");
            CreateIndex("dbo.DepositPayments", "CustomerDepositId");
            CreateIndex("dbo.AspNetUsers", "CustomerId");
            AddForeignKey("dbo.BankClients", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Bills", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditRequests", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditPayments", "CustomerCreditId", "dbo.CustomerCredits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DepositRequests", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DepositPayments", "CustomerDepositId", "dbo.CustomerDeposits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Addresses", "PersonId");
            DropColumn("dbo.BankClients", "PersonId");
            DropColumn("dbo.Bills", "PersonId");
            DropColumn("dbo.CreditPayments", "PersonCreditId");
            DropColumn("dbo.CreditRequests", "PersonId");
            DropColumn("dbo.DepositPayments", "PersonDepositId");
            DropColumn("dbo.DepositRequests", "PersonId");
            DropColumn("dbo.AspNetUsers", "PersonId");
            DropColumn("dbo.AspNetUsers", "StartBlockDateTime");
            DropTable("dbo.People");
            DropTable("dbo.PersonCredits");
            DropTable("dbo.PersonDeposits");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
            
            AddColumn("dbo.AspNetUsers", "StartBlockDateTime", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.DepositRequests", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.DepositPayments", "PersonDepositId", c => c.Int(nullable: false));
            AddColumn("dbo.CreditRequests", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.CreditPayments", "PersonCreditId", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.BankClients", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "PersonId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DepositPayments", "CustomerDepositId", "dbo.CustomerDeposits");
            DropForeignKey("dbo.DepositRequests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerDeposits", "DepositId", "dbo.Deposits");
            DropForeignKey("dbo.CustomerDeposits", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerDeposits", "BillId", "dbo.Bills");
            DropForeignKey("dbo.CreditPayments", "CustomerCreditId", "dbo.CustomerCredits");
            DropForeignKey("dbo.CustomerCredits", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerCredits", "CreditId", "dbo.Credits");
            DropForeignKey("dbo.CreditRequests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerCredits", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BankClients", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetUsers", new[] { "CustomerId" });
            DropIndex("dbo.DepositPayments", new[] { "CustomerDepositId" });
            DropIndex("dbo.DepositRequests", new[] { "CustomerId" });
            DropIndex("dbo.CustomerDeposits", new[] { "BillId" });
            DropIndex("dbo.CustomerDeposits", new[] { "DepositId" });
            DropIndex("dbo.CustomerDeposits", new[] { "CustomerId" });
            DropIndex("dbo.CreditPayments", new[] { "CustomerCreditId" });
            DropIndex("dbo.CreditRequests", new[] { "CustomerId" });
            DropIndex("dbo.CustomerCredits", new[] { "BillId" });
            DropIndex("dbo.CustomerCredits", new[] { "CreditId" });
            DropIndex("dbo.CustomerCredits", new[] { "CustomerId" });
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            DropIndex("dbo.BankClients", new[] { "CustomerId" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropPrimaryKey("dbo.BankClients");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Flat", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "EndBlockDateTime");
            DropColumn("dbo.AspNetUsers", "CustomerId");
            DropColumn("dbo.DepositRequests", "CustomerId");
            DropColumn("dbo.Deposits", "MaxPeriod");
            DropColumn("dbo.Deposits", "MinPeriod");
            DropColumn("dbo.Deposits", "MaxSum");
            DropColumn("dbo.Deposits", "MinSum");
            DropColumn("dbo.DepositPayments", "CustomerDepositId");
            DropColumn("dbo.CreditRequests", "CustomerId");
            DropColumn("dbo.CreditPayments", "CustomerCreditId");
            DropColumn("dbo.Bills", "CustomerId");
            DropColumn("dbo.BankClients", "CustomerId");
            DropColumn("dbo.Addresses", "Street");
            DropColumn("dbo.Addresses", "CustomerId");
            DropTable("dbo.CustomerDeposits");
            DropTable("dbo.CustomerCredits");
            DropTable("dbo.Customers");
            AddPrimaryKey("dbo.BankClients", "PersonId");
            AddPrimaryKey("dbo.Addresses", "PersonId");
            CreateIndex("dbo.AspNetUsers", "PersonId");
            CreateIndex("dbo.DepositRequests", "PersonId");
            CreateIndex("dbo.PersonDeposits", "DepositId");
            CreateIndex("dbo.PersonDeposits", "PersonId");
            CreateIndex("dbo.DepositPayments", "PersonDepositId");
            CreateIndex("dbo.CreditRequests", "PersonId");
            CreateIndex("dbo.PersonCredits", "CreditId");
            CreateIndex("dbo.PersonCredits", "PersonId");
            CreateIndex("dbo.CreditPayments", "PersonCreditId");
            CreateIndex("dbo.Bills", "PersonId");
            CreateIndex("dbo.BankClients", "PersonId");
            CreateIndex("dbo.Addresses", "PersonId");
            AddForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "PersonId", "dbo.People", "Id");
            AddForeignKey("dbo.Bills", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonDeposits", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DepositPayments", "PersonDepositId", "dbo.PersonDeposits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonDeposits", "DepositId", "dbo.Deposits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DepositRequests", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonCredits", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditPayments", "PersonCreditId", "dbo.PersonCredits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonCredits", "CreditId", "dbo.Credits", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreditRequests", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BankClients", "PersonId", "dbo.People", "Id");
        }
    }
}
