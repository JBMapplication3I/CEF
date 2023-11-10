// <copyright file="201905162113393_SubscriptionsOverhaul.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201905162113393 subscriptions overhaul class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SubscriptionsOverhaul : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Payments.Subscription", "SubscriptionStatusID", "Payments.SubscriptionStatus");
            DropForeignKey("Payments.Subscription", "SubscriptionTypeID", "Payments.SubscriptionType");
            DropIndex("Payments.Subscription", new[] { "SubscriptionStatusID" });
            DropIndex("Payments.Subscription", new[] { "SubscriptionTypeID" });
            RenameColumn(table: "Payments.Subscription", name: "SubscriptionStatusID", newName: "StatusID");
            RenameColumn(table: "Payments.Subscription", name: "SubscriptionTypeID", newName: "TypeID");
            CreateTable(
                "Products.ProductMembershipLevel",
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
                        MembershipRepeatTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Products.Product", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Payments.MembershipRepeatType", t => t.MembershipRepeatTypeID, cascadeDelete: true)
                .ForeignKey("Payments.MembershipLevel", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.MembershipRepeatTypeID);

            CreateTable(
                "Payments.MembershipRepeatType",
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
                .ForeignKey("Payments.Membership", t => t.MasterID)
                .ForeignKey("Payments.RepeatType", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Payments.Membership",
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
                        IsContractual = c.Boolean(nullable: false),
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

            CreateTable(
                "Payments.MembershipLevel",
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
                        RolesApplied = c.String(maxLength: 512, unicode: false),
                        MembershipID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.Membership", t => t.MembershipID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.MembershipID);

            CreateTable(
                "Payments.MembershipAdZoneAccessByLevel",
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
                        SubscriberCountThreshold = c.Int(nullable: false),
                        UniqueAdLimit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments.MembershipAdZoneAccess", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Payments.MembershipLevel", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            CreateTable(
                "Payments.MembershipAdZoneAccess",
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
                .ForeignKey("Payments.Membership", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Advertising.AdZoneAccess", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID);

            AddColumn("Payments.Subscription", "ProductMembershipLevelID", c => c.Int());
            AddColumn("Payments.RepeatType", "RepeatableBillingPeriods", c => c.Int());
            AddColumn("Payments.RepeatType", "InitialBonusBillingPeriods", c => c.Int());
            AlterColumn("Payments.Subscription", "StatusID", c => c.Int(nullable: false));
            AlterColumn("Payments.Subscription", "TypeID", c => c.Int(nullable: false));
            CreateIndex("Payments.Subscription", "TypeID");
            CreateIndex("Payments.Subscription", "StatusID");
            CreateIndex("Payments.Subscription", "ProductMembershipLevelID");
            AddForeignKey("Payments.Subscription", "ProductMembershipLevelID", "Products.ProductMembershipLevel", "ID");
            AddForeignKey("Payments.Subscription", "StatusID", "Payments.SubscriptionStatus", "ID", cascadeDelete: true);
            AddForeignKey("Payments.Subscription", "TypeID", "Payments.SubscriptionType", "ID", cascadeDelete: true);
            DropColumn("Payments.Subscription", "FilterKey1");
            DropColumn("Payments.Subscription", "FilterKey2");
            DropColumn("Payments.Subscription", "FilterKey3");
        }

        public override void Down()
        {
            AddColumn("Payments.Subscription", "FilterKey3", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.Subscription", "FilterKey2", c => c.String(maxLength: 128, unicode: false));
            AddColumn("Payments.Subscription", "FilterKey1", c => c.String(maxLength: 128, unicode: false));
            DropForeignKey("Payments.Subscription", "TypeID", "Payments.SubscriptionType");
            DropForeignKey("Payments.Subscription", "StatusID", "Payments.SubscriptionStatus");
            DropForeignKey("Products.ProductMembershipLevel", "SlaveID", "Payments.MembershipLevel");
            DropForeignKey("Products.ProductMembershipLevel", "MembershipRepeatTypeID", "Payments.MembershipRepeatType");
            DropForeignKey("Payments.MembershipRepeatType", "SlaveID", "Payments.RepeatType");
            DropForeignKey("Payments.MembershipRepeatType", "MasterID", "Payments.Membership");
            DropForeignKey("Payments.MembershipAdZoneAccessByLevel", "SlaveID", "Payments.MembershipLevel");
            DropForeignKey("Payments.MembershipAdZoneAccessByLevel", "MasterID", "Payments.MembershipAdZoneAccess");
            DropForeignKey("Payments.MembershipAdZoneAccess", "SlaveID", "Advertising.AdZoneAccess");
            DropForeignKey("Payments.Subscription", "ProductMembershipLevelID", "Products.ProductMembershipLevel");
            DropForeignKey("Payments.MembershipAdZoneAccess", "MasterID", "Payments.Membership");
            DropForeignKey("Payments.MembershipLevel", "MembershipID", "Payments.Membership");
            DropForeignKey("Products.ProductMembershipLevel", "MasterID", "Products.Product");
            DropIndex("Payments.Subscription", new[] { "ProductMembershipLevelID" });
            DropIndex("Payments.Subscription", new[] { "StatusID" });
            DropIndex("Payments.Subscription", new[] { "TypeID" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "SlaveID" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "MasterID" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "Hash" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "Active" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "UpdatedDate" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "CreatedDate" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "CustomKey" });
            DropIndex("Payments.MembershipAdZoneAccess", new[] { "ID" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "SlaveID" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "MasterID" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "Hash" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "Active" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "UpdatedDate" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "CreatedDate" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "CustomKey" });
            DropIndex("Payments.MembershipAdZoneAccessByLevel", new[] { "ID" });
            DropIndex("Payments.MembershipLevel", new[] { "MembershipID" });
            DropIndex("Payments.MembershipLevel", new[] { "SortOrder" });
            DropIndex("Payments.MembershipLevel", new[] { "DisplayName" });
            DropIndex("Payments.MembershipLevel", new[] { "Name" });
            DropIndex("Payments.MembershipLevel", new[] { "Hash" });
            DropIndex("Payments.MembershipLevel", new[] { "Active" });
            DropIndex("Payments.MembershipLevel", new[] { "UpdatedDate" });
            DropIndex("Payments.MembershipLevel", new[] { "CreatedDate" });
            DropIndex("Payments.MembershipLevel", new[] { "CustomKey" });
            DropIndex("Payments.MembershipLevel", new[] { "ID" });
            DropIndex("Payments.Membership", new[] { "SortOrder" });
            DropIndex("Payments.Membership", new[] { "DisplayName" });
            DropIndex("Payments.Membership", new[] { "Name" });
            DropIndex("Payments.Membership", new[] { "Hash" });
            DropIndex("Payments.Membership", new[] { "Active" });
            DropIndex("Payments.Membership", new[] { "UpdatedDate" });
            DropIndex("Payments.Membership", new[] { "CreatedDate" });
            DropIndex("Payments.Membership", new[] { "CustomKey" });
            DropIndex("Payments.Membership", new[] { "ID" });
            DropIndex("Payments.MembershipRepeatType", new[] { "SlaveID" });
            DropIndex("Payments.MembershipRepeatType", new[] { "MasterID" });
            DropIndex("Payments.MembershipRepeatType", new[] { "Hash" });
            DropIndex("Payments.MembershipRepeatType", new[] { "Active" });
            DropIndex("Payments.MembershipRepeatType", new[] { "UpdatedDate" });
            DropIndex("Payments.MembershipRepeatType", new[] { "CreatedDate" });
            DropIndex("Payments.MembershipRepeatType", new[] { "CustomKey" });
            DropIndex("Payments.MembershipRepeatType", new[] { "ID" });
            DropIndex("Products.ProductMembershipLevel", new[] { "MembershipRepeatTypeID" });
            DropIndex("Products.ProductMembershipLevel", new[] { "SlaveID" });
            DropIndex("Products.ProductMembershipLevel", new[] { "MasterID" });
            DropIndex("Products.ProductMembershipLevel", new[] { "Hash" });
            DropIndex("Products.ProductMembershipLevel", new[] { "Active" });
            DropIndex("Products.ProductMembershipLevel", new[] { "UpdatedDate" });
            DropIndex("Products.ProductMembershipLevel", new[] { "CreatedDate" });
            DropIndex("Products.ProductMembershipLevel", new[] { "CustomKey" });
            DropIndex("Products.ProductMembershipLevel", new[] { "ID" });
            AlterColumn("Payments.Subscription", "TypeID", c => c.Int());
            AlterColumn("Payments.Subscription", "StatusID", c => c.Int());
            DropColumn("Payments.RepeatType", "InitialBonusBillingPeriods");
            DropColumn("Payments.RepeatType", "RepeatableBillingPeriods");
            DropColumn("Payments.Subscription", "ProductMembershipLevelID");
            DropTable("Payments.MembershipAdZoneAccess");
            DropTable("Payments.MembershipAdZoneAccessByLevel");
            DropTable("Payments.MembershipLevel");
            DropTable("Payments.Membership");
            DropTable("Payments.MembershipRepeatType");
            DropTable("Products.ProductMembershipLevel");
            RenameColumn(table: "Payments.Subscription", name: "TypeID", newName: "SubscriptionTypeID");
            RenameColumn(table: "Payments.Subscription", name: "StatusID", newName: "SubscriptionStatusID");
            CreateIndex("Payments.Subscription", "SubscriptionTypeID");
            CreateIndex("Payments.Subscription", "SubscriptionStatusID");
            AddForeignKey("Payments.Subscription", "SubscriptionTypeID", "Payments.SubscriptionType", "ID");
            AddForeignKey("Payments.Subscription", "SubscriptionStatusID", "Payments.SubscriptionStatus", "ID");
        }
    }
}
