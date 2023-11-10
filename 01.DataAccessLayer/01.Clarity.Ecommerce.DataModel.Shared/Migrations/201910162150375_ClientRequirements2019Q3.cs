// <copyright file="201910162150375_ClientRequirements2019Q3.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201910162150375 client requirements 2019 q 3 class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ClientRequirements2019Q3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Hangfire.State", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.JobParameter", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.JobQueue", "JobId", "Hangfire.Job");
            DropIndex("Attributes.GeneralAttribute", new[] { "CustomKey" });
            DropIndex("Hangfire.AggregatedCounter", new[] { "Id" });
            DropIndex("Hangfire.Counter", new[] { "Id" });
            DropIndex("Hangfire.Hash", new[] { "Id" });
            DropIndex("Hangfire.Hash", "IX_HangFire_Hash_ExpireAt");
            DropIndex("Hangfire.JobParameter", new[] { "Id" });
            DropIndex("Hangfire.JobParameter", "IX_HangFire_JobParameter_JobIdAndName");
            DropIndex("Hangfire.JobParameter", new[] { "JobId" });
            DropIndex("Hangfire.Job", new[] { "Id" });
            DropIndex("Hangfire.State", new[] { "Id" });
            DropIndex("Hangfire.State", new[] { "JobId" });
            DropIndex("Hangfire.JobQueue", "IX_HangFire_JobQueue_QueueAndFetchedAt");
            DropIndex("Hangfire.JobQueue", new[] { "JobId" });
            DropIndex("Hangfire.List", new[] { "Id" });
            DropIndex("Hangfire.List", new[] { "Key" });
            DropIndex("Hangfire.Server", new[] { "Id" });
            DropIndex("Hangfire.Set", new[] { "Id" });
            DropPrimaryKey("Hangfire.AggregatedCounter");
            // DropPrimaryKey("Hangfire.Counter"); // Uses a different constraint, may be in an odd state
            Sql("ALTER TABLE [Hangfire].[Counter] DROP CONSTRAINT [PK_Hangfire.Counter]");
            DropPrimaryKey("Hangfire.Hash");
            DropPrimaryKey("Hangfire.JobParameter");
            DropPrimaryKey("Hangfire.Job");
            DropPrimaryKey("Hangfire.State");
            DropPrimaryKey("Hangfire.List");
            DropPrimaryKey("Hangfire.Set");
            CreateTable(
                "Accounts.AccountProduct",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.SlaveID, cascadeDelete: true)
                .ForeignKey("Accounts.AccountProductType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.TypeID);

            CreateTable(
                "Accounts.AccountProductType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
            Sql("INSERT INTO Accounts.AccountProductType (CreatedDate,Active,CustomKey,[Name],DisplayName,SortOrder,JsonAttributes) VALUES (CURRENT_TIMESTAMP,1,'Customized','Customized','Customized',0,'{}')");

            CreateTable(
                "Vendors.VendorAccount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Vendors.Vendor", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Accounts.Account", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Messaging.ProductNotification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.ProductID);

            CreateTable(
                "Products.ProductStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
            Sql("INSERT INTO Products.ProductStatus (CreatedDate,Active,CustomKey,[Name],DisplayName,SortOrder,JsonAttributes) VALUES (CURRENT_TIMESTAMP,1,'NORMAL','Normal','Normal',0,'{}')");

            CreateTable(
                "Accounts.AccountUsageBalance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            AddColumn("Accounts.Account", "Token", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Accounts.Account", "SageID", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Stores.Store", "MinimumOrderDollarAmountWarningMessage", c => c.String(maxLength: 512));
            AddColumn("Stores.Store", "MinimumOrderDollarAmount", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Stores.Store", "MinimumOrderDollarAmountAfter", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Products.Product", "StatusID", c => c.Int(nullable: false));
            Sql("UPDATE Products.Product SET StatusID = 1");
            AddColumn("Vendors.Vendor", "UserName", c => c.String(maxLength: 128));
            AddColumn("Vendors.Vendor", "PasswordHash", c => c.String(maxLength: 128));
            AddColumn("Vendors.Vendor", "SecurityToken", c => c.String(maxLength: 128));
            AddColumn("Vendors.Vendor", "MustResetPassword", c => c.Boolean(nullable: false));
            AddColumn("Shipping.Shipment", "TargetShippingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Shipping.RateQuote", "TargetShippingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Pricing.PriceRule", "UnitOfMeasure", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("Attributes.GeneralAttribute", "CustomKey", c => c.String(nullable: false, maxLength: 128, unicode: false));
            AlterColumn("Hangfire.Counter", "Value", c => c.Int(nullable: false));
            AlterColumn("Hangfire.Hash", "ExpireAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("Hangfire.JobParameter", "JobId", c => c.Long(nullable: false));
            AlterColumn("Hangfire.Job", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("Hangfire.Job", "StateId", c => c.Long());
            AlterColumn("Hangfire.State", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("Hangfire.State", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("Hangfire.State", "JobId", c => c.Long(nullable: false));
            AlterColumn("Hangfire.JobQueue", "JobId", c => c.Long(nullable: false));
            AlterColumn("Hangfire.List", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("Hangfire.AggregatedCounter", "Key");
            // AddPrimaryKey("Hangfire.Counter", "Key"); // Needs a different constraint
            Sql("CREATE CLUSTERED INDEX [CX_HangFire_Counter] ON [HangFire].[Counter] ([Key] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            AddPrimaryKey("Hangfire.Hash", new[] { "Key", "Field" });
            AddPrimaryKey("Hangfire.JobParameter", new[] { "JobId", "Name" });
            AddPrimaryKey("Hangfire.Job", "Id");
            AddPrimaryKey("Hangfire.State", "Id");
            AddPrimaryKey("Hangfire.List", "Id");
            AddPrimaryKey("Hangfire.Set", new[] { "Key", "Value" });
            CreateIndex("Products.Product", "StatusID");
            CreateIndex("Vendors.Vendor", "UserName");
            CreateIndex("Vendors.Vendor", "PasswordHash");
            CreateIndex("Vendors.Vendor", "SecurityToken");
            CreateIndex("Vendors.Vendor", "MustResetPassword");
            CreateIndex("Attributes.GeneralAttribute", "CustomKey", unique: true);
            CreateIndex("Hangfire.AggregatedCounter", "ExpireAt", name: "[IX_HangFire_AggregatedCounter_ExpireAt]");
            CreateIndex("Hangfire.Hash", "ExpireAt", name: "IX_HangFire_Hash_ExpireAt");
            CreateIndex("Hangfire.JobParameter", new[] { "JobId", "Name" }, name: "IX_HangFire_JobParameter_JobIdAndName");
            CreateIndex("Hangfire.Job", "Id");
            CreateIndex("Hangfire.State", "JobId");
            CreateIndex("Hangfire.JobQueue", new[] { "Queue", "Id" }, name: "IX_HangFire_JobQueue_QueueAndId");
            CreateIndex("Hangfire.JobQueue", "JobId");
            CreateIndex("Hangfire.Server", "LastHeartbeat", name: "IX_HangFire_Server_LastHeartbeat");
            CreateIndex("Hangfire.Set", new[] { "Key", "Score" }, name: "IX_HangFire_Set_Score");
            AddForeignKey("Products.Product", "StatusID", "Products.ProductStatus", "ID", cascadeDelete: true);
            AddForeignKey("Hangfire.State", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            AddForeignKey("Hangfire.JobParameter", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            AddForeignKey("Hangfire.JobQueue", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            DropColumn("Hangfire.AggregatedCounter", "Id");
            DropColumn("Hangfire.Counter", "Id");
            DropColumn("Hangfire.Hash", "Id");
            DropColumn("Hangfire.JobParameter", "Id");
            DropColumn("Hangfire.Set", "Id");
            Sql("UPDATE [Hangfire].[Schema] SET [Version] = 7");
        }

        public override void Down()
        {
            Sql("DROP INDEX [CX_HangFire_Counter] ON [HangFire].[Counter]");
            AddColumn("Hangfire.Set", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("Hangfire.JobParameter", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("Hangfire.Hash", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("Hangfire.Counter", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("Hangfire.AggregatedCounter", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("Hangfire.JobQueue", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.JobParameter", "JobId", "Hangfire.Job");
            DropForeignKey("Hangfire.State", "JobId", "Hangfire.Job");
            DropForeignKey("Accounts.AccountUsageBalance", "SlaveID", "Products.Product");
            DropForeignKey("Accounts.AccountUsageBalance", "MasterID", "Accounts.Account");
            DropForeignKey("Products.Product", "StatusID", "Products.ProductStatus");
            DropForeignKey("Messaging.ProductNotification", "ProductID", "Products.Product");
            DropForeignKey("Vendors.VendorAccount", "SlaveID", "Accounts.Account");
            DropForeignKey("Vendors.VendorAccount", "MasterID", "Vendors.Vendor");
            DropForeignKey("Accounts.AccountProduct", "TypeID", "Accounts.AccountProductType");
            DropForeignKey("Accounts.AccountProduct", "SlaveID", "Products.Product");
            DropForeignKey("Accounts.AccountProduct", "MasterID", "Accounts.Account");
            DropIndex("Hangfire.Set", "IX_HangFire_Set_Score");
            DropIndex("Hangfire.Server", "IX_HangFire_Server_LastHeartbeat");
            DropIndex("Hangfire.JobQueue", new[] { "JobId" });
            DropIndex("Hangfire.JobQueue", "IX_HangFire_JobQueue_QueueAndId");
            DropIndex("Hangfire.State", new[] { "JobId" });
            DropIndex("Hangfire.Job", new[] { "Id" });
            DropIndex("Hangfire.JobParameter", "IX_HangFire_JobParameter_JobIdAndName");
            DropIndex("Hangfire.Hash", "IX_HangFire_Hash_ExpireAt");
            DropIndex("Hangfire.AggregatedCounter", "[IX_HangFire_AggregatedCounter_ExpireAt]");
            DropIndex("Attributes.GeneralAttribute", new[] { "CustomKey" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "SlaveID" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "MasterID" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "Hash" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "Active" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "CustomKey" });
            DropIndex("Accounts.AccountUsageBalance", new[] { "ID" });
            DropIndex("Products.ProductStatus", new[] { "SortOrder" });
            DropIndex("Products.ProductStatus", new[] { "DisplayName" });
            DropIndex("Products.ProductStatus", new[] { "Name" });
            DropIndex("Products.ProductStatus", new[] { "Hash" });
            DropIndex("Products.ProductStatus", new[] { "Active" });
            DropIndex("Products.ProductStatus", new[] { "UpdatedDate" });
            DropIndex("Products.ProductStatus", new[] { "CreatedDate" });
            DropIndex("Products.ProductStatus", new[] { "CustomKey" });
            DropIndex("Products.ProductStatus", new[] { "ID" });
            DropIndex("Messaging.ProductNotification", new[] { "ProductID" });
            DropIndex("Messaging.ProductNotification", new[] { "Name" });
            DropIndex("Messaging.ProductNotification", new[] { "Hash" });
            DropIndex("Messaging.ProductNotification", new[] { "Active" });
            DropIndex("Messaging.ProductNotification", new[] { "UpdatedDate" });
            DropIndex("Messaging.ProductNotification", new[] { "CreatedDate" });
            DropIndex("Messaging.ProductNotification", new[] { "CustomKey" });
            DropIndex("Messaging.ProductNotification", new[] { "ID" });
            DropIndex("Vendors.VendorAccount", new[] { "SlaveID" });
            DropIndex("Vendors.VendorAccount", new[] { "MasterID" });
            DropIndex("Vendors.VendorAccount", new[] { "Hash" });
            DropIndex("Vendors.VendorAccount", new[] { "Active" });
            DropIndex("Vendors.VendorAccount", new[] { "UpdatedDate" });
            DropIndex("Vendors.VendorAccount", new[] { "CreatedDate" });
            DropIndex("Vendors.VendorAccount", new[] { "CustomKey" });
            DropIndex("Vendors.VendorAccount", new[] { "ID" });
            DropIndex("Vendors.Vendor", new[] { "MustResetPassword" });
            DropIndex("Vendors.Vendor", new[] { "SecurityToken" });
            DropIndex("Vendors.Vendor", new[] { "PasswordHash" });
            DropIndex("Vendors.Vendor", new[] { "UserName" });
            DropIndex("Accounts.AccountProductType", new[] { "SortOrder" });
            DropIndex("Accounts.AccountProductType", new[] { "DisplayName" });
            DropIndex("Accounts.AccountProductType", new[] { "Name" });
            DropIndex("Accounts.AccountProductType", new[] { "Hash" });
            DropIndex("Accounts.AccountProductType", new[] { "Active" });
            DropIndex("Accounts.AccountProductType", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountProductType", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountProductType", new[] { "CustomKey" });
            DropIndex("Accounts.AccountProductType", new[] { "ID" });
            DropIndex("Accounts.AccountProduct", new[] { "TypeID" });
            DropIndex("Accounts.AccountProduct", new[] { "SlaveID" });
            DropIndex("Accounts.AccountProduct", new[] { "MasterID" });
            DropIndex("Accounts.AccountProduct", new[] { "Hash" });
            DropIndex("Accounts.AccountProduct", new[] { "Active" });
            DropIndex("Accounts.AccountProduct", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountProduct", new[] { "CreatedDate" });
            DropIndex("Accounts.AccountProduct", new[] { "CustomKey" });
            DropIndex("Accounts.AccountProduct", new[] { "ID" });
            DropIndex("Products.Product", new[] { "StatusID" });
            DropPrimaryKey("Hangfire.Set");
            DropPrimaryKey("Hangfire.List");
            DropPrimaryKey("Hangfire.State");
            DropPrimaryKey("Hangfire.Job");
            DropPrimaryKey("Hangfire.JobParameter");
            DropPrimaryKey("Hangfire.Hash");
            // DropPrimaryKey("Hangfire.Counter"); // Uses a different constraint
            DropPrimaryKey("Hangfire.AggregatedCounter");
            AlterColumn("Hangfire.List", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("Hangfire.JobQueue", "JobId", c => c.Int(nullable: false));
            AlterColumn("Hangfire.State", "JobId", c => c.Int(nullable: false));
            AlterColumn("Hangfire.State", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Hangfire.State", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("Hangfire.Job", "StateId", c => c.Int());
            AlterColumn("Hangfire.Job", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("Hangfire.JobParameter", "JobId", c => c.Int(nullable: false));
            AlterColumn("Hangfire.Hash", "ExpireAt", c => c.DateTime());
            AlterColumn("Hangfire.Counter", "Value", c => c.Short(nullable: false));
            AlterColumn("Attributes.GeneralAttribute", "CustomKey", c => c.String(maxLength: 128, unicode: false));
            DropColumn("Pricing.PriceRule", "UnitOfMeasure");
            DropColumn("Shipping.RateQuote", "TargetShippingDate");
            DropColumn("Shipping.Shipment", "TargetShippingDate");
            DropColumn("Vendors.Vendor", "MustResetPassword");
            DropColumn("Vendors.Vendor", "SecurityToken");
            DropColumn("Vendors.Vendor", "PasswordHash");
            DropColumn("Vendors.Vendor", "UserName");
            DropColumn("Products.Product", "StatusID");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountAfter");
            DropColumn("Stores.Store", "MinimumOrderDollarAmount");
            DropColumn("Stores.Store", "MinimumOrderDollarAmountWarningMessage");
            DropColumn("Accounts.Account", "SageID");
            DropColumn("Accounts.Account", "Token");
            DropTable("Accounts.AccountUsageBalance");
            DropTable("Products.ProductStatus");
            DropTable("Messaging.ProductNotification");
            DropTable("Vendors.VendorAccount");
            DropTable("Accounts.AccountProductType");
            DropTable("Accounts.AccountProduct");
            AddPrimaryKey("Hangfire.Set", "Id");
            AddPrimaryKey("Hangfire.List", "Id");
            AddPrimaryKey("Hangfire.State", "Id");
            AddPrimaryKey("Hangfire.Job", "Id");
            AddPrimaryKey("Hangfire.JobParameter", "Id");
            AddPrimaryKey("Hangfire.Hash", "Id");
            AddPrimaryKey("Hangfire.Counter", "Id");
            AddPrimaryKey("Hangfire.AggregatedCounter", "Id");
            CreateIndex("Hangfire.Set", "Id");
            CreateIndex("Hangfire.Server", "Id");
            CreateIndex("Hangfire.List", "Key");
            CreateIndex("Hangfire.List", "Id");
            CreateIndex("Hangfire.JobQueue", "JobId");
            CreateIndex("Hangfire.JobQueue", new[] { "Queue", "FetchedAt" }, name: "IX_HangFire_JobQueue_QueueAndFetchedAt");
            CreateIndex("Hangfire.State", "JobId");
            CreateIndex("Hangfire.State", "Id");
            CreateIndex("Hangfire.Job", "Id");
            CreateIndex("Hangfire.JobParameter", "JobId");
            CreateIndex("Hangfire.JobParameter", new[] { "Id", "Name" }, name: "IX_HangFire_JobParameter_JobIdAndName");
            CreateIndex("Hangfire.JobParameter", "Id");
            CreateIndex("Hangfire.Hash", "ExpireAt", name: "IX_HangFire_Hash_ExpireAt");
            CreateIndex("Hangfire.Hash", "Id");
            CreateIndex("Hangfire.Counter", "Id");
            CreateIndex("Hangfire.AggregatedCounter", "Id");
            CreateIndex("Attributes.GeneralAttribute", "CustomKey");
            AddForeignKey("Hangfire.JobQueue", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            AddForeignKey("Hangfire.JobParameter", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            AddForeignKey("Hangfire.State", "JobId", "Hangfire.Job", "Id", cascadeDelete: true);
            Sql("UPDATE [Hangfire].[Schema] SET [Version] = 5");
        }
    }
}
