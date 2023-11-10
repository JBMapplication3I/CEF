// <copyright file="202103290016369_SalesGroupBrandsAndSubscriptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202103290016369 sales group brands and subscriptions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesGroupBrandsAndSubscriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Inventory.InventoryLocationRegion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Inventory.InventoryLocation", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.Schedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.ScheduledEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        EventEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        Memo = c.String(maxLength: 256),
                        ScheduleID = c.Int(),
                        GroupID = c.Int(),
                        RescheduledEventID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Groups.Group", t => t.GroupID)
                .ForeignKey("Scheduling.ScheduledEvent", t => t.RescheduledEventID)
                .ForeignKey("Scheduling.Schedule", t => t.ScheduleID)
                .Index(t => t.ID)
                .Index(t => t.ScheduleID)
                .Index(t => t.GroupID)
                .Index(t => t.RescheduledEventID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Stores.StoreRegion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Stores.Store", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Geography.Region", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Ordering.SalesOrder", "InventoryLocationID", c => c.Int());
            AddColumn("Stores.Store", "ScheduleID", c => c.Int());
            AddColumn("Sales.SalesGroup", "BrandID", c => c.Int());
            AddColumn("Payments.Subscription", "SalesGroupID", c => c.Int());
            AddColumn("Payments.Subscription", "ProductSubscriptionTypeID", c => c.Int());
            AddColumn("Products.ProductSubscriptionType", "SubscriptionTypeRepeatTypeID", c => c.Int(nullable: false));
            CreateIndex("Ordering.SalesOrder", "InventoryLocationID");
            CreateIndex("Stores.Store", "ScheduleID");
            CreateIndex("Sales.SalesGroup", "BrandID");
            CreateIndex("Payments.Subscription", "SalesGroupID");
            CreateIndex("Payments.Subscription", "ProductSubscriptionTypeID");
            CreateIndex("Products.ProductSubscriptionType", "SubscriptionTypeRepeatTypeID");
            AddForeignKey("Sales.SalesGroup", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Products.ProductSubscriptionType", "SubscriptionTypeRepeatTypeID", "Payments.SubscriptionTypeRepeatType", "ID", cascadeDelete: true);
            AddForeignKey("Payments.Subscription", "ProductSubscriptionTypeID", "Products.ProductSubscriptionType", "ID");
            AddForeignKey("Payments.Subscription", "SalesGroupID", "Sales.SalesGroup", "ID");
            AddForeignKey("Stores.Store", "ScheduleID", "Scheduling.Schedule", "ID");
            AddForeignKey("Ordering.SalesOrder", "InventoryLocationID", "Inventory.InventoryLocation", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Ordering.SalesOrder", "InventoryLocationID", "Inventory.InventoryLocation");
            DropForeignKey("Stores.StoreRegion", "SlaveID", "Geography.Region");
            DropForeignKey("Stores.StoreRegion", "MasterID", "Stores.Store");
            DropForeignKey("Stores.Store", "ScheduleID", "Scheduling.Schedule");
            DropForeignKey("Scheduling.ScheduledEvent", "ScheduleID", "Scheduling.Schedule");
            DropForeignKey("Scheduling.ScheduledEvent", "RescheduledEventID", "Scheduling.ScheduledEvent");
            DropForeignKey("Scheduling.ScheduledEvent", "GroupID", "Groups.Group");
            DropForeignKey("Payments.Subscription", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("Payments.Subscription", "ProductSubscriptionTypeID", "Products.ProductSubscriptionType");
            DropForeignKey("Products.ProductSubscriptionType", "SubscriptionTypeRepeatTypeID", "Payments.SubscriptionTypeRepeatType");
            DropForeignKey("Inventory.InventoryLocationRegion", "SlaveID", "Geography.Region");
            DropForeignKey("Inventory.InventoryLocationRegion", "MasterID", "Inventory.InventoryLocation");
            DropForeignKey("Sales.SalesGroup", "BrandID", "Brands.Brand");
            DropIndex("Stores.StoreRegion", new[] { "Hash" });
            DropIndex("Stores.StoreRegion", new[] { "Active" });
            DropIndex("Stores.StoreRegion", new[] { "UpdatedDate" });
            DropIndex("Stores.StoreRegion", new[] { "CreatedDate" });
            DropIndex("Stores.StoreRegion", new[] { "CustomKey" });
            DropIndex("Stores.StoreRegion", new[] { "SlaveID" });
            DropIndex("Stores.StoreRegion", new[] { "MasterID" });
            DropIndex("Stores.StoreRegion", new[] { "ID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "Hash" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "Active" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "UpdatedDate" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "CreatedDate" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "CustomKey" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "RescheduledEventID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "GroupID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "ScheduleID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "ID" });
            DropIndex("Scheduling.Schedule", new[] { "Hash" });
            DropIndex("Scheduling.Schedule", new[] { "Active" });
            DropIndex("Scheduling.Schedule", new[] { "UpdatedDate" });
            DropIndex("Scheduling.Schedule", new[] { "CreatedDate" });
            DropIndex("Scheduling.Schedule", new[] { "CustomKey" });
            DropIndex("Scheduling.Schedule", new[] { "ID" });
            DropIndex("Products.ProductSubscriptionType", new[] { "SubscriptionTypeRepeatTypeID" });
            DropIndex("Payments.Subscription", new[] { "ProductSubscriptionTypeID" });
            DropIndex("Payments.Subscription", new[] { "SalesGroupID" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "Hash" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "Active" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "UpdatedDate" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "CreatedDate" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "CustomKey" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "SlaveID" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "MasterID" });
            DropIndex("Inventory.InventoryLocationRegion", new[] { "ID" });
            DropIndex("Sales.SalesGroup", new[] { "BrandID" });
            DropIndex("Stores.Store", new[] { "ScheduleID" });
            DropIndex("Ordering.SalesOrder", new[] { "InventoryLocationID" });
            DropColumn("Products.ProductSubscriptionType", "SubscriptionTypeRepeatTypeID");
            DropColumn("Payments.Subscription", "ProductSubscriptionTypeID");
            DropColumn("Payments.Subscription", "SalesGroupID");
            DropColumn("Sales.SalesGroup", "BrandID");
            DropColumn("Stores.Store", "ScheduleID");
            DropColumn("Ordering.SalesOrder", "InventoryLocationID");
            DropTable("Stores.StoreRegion");
            DropTable("Scheduling.ScheduledEvent");
            DropTable("Scheduling.Schedule");
            DropTable("Inventory.InventoryLocationRegion");
        }
    }
}
