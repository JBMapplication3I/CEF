// <copyright file="202011260008217_MessageAttachmentsSeoAndSTRT.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202011260008217 message attachments seo and strt class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MessageAttachmentsSeoAndSTRT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Payments.SubscriptionTypeRepeatType",
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
                .ForeignKey("Payments.SubscriptionType", t => t.MasterID)
                .ForeignKey("Payments.RepeatType", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Contacts.User", "DateOfBirth", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Contacts.User", "Gender", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Contacts.User", "IsSMSAllowed", c => c.Boolean(nullable: false));
            AddColumn("Messaging.MessageAttachment", "SeoUrl", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Messaging.MessageAttachment", "SeoKeywords", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Messaging.MessageAttachment", "SeoPageTitle", c => c.String(maxLength: 75, unicode: false));
            AddColumn("Messaging.MessageAttachment", "SeoDescription", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Messaging.MessageAttachment", "SeoMetaData", c => c.String(maxLength: 512, unicode: false));
            AddColumn("Messaging.MessageAttachment", "FileAccessTypeID", c => c.Int(nullable: false));
            AddColumn("Messaging.MessageAttachment", "SortOrder", c => c.Int());
            AddColumn("Payments.Subscription", "IsAutoRefill", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropForeignKey("Payments.SubscriptionTypeRepeatType", "SlaveID", "Payments.RepeatType");
            DropForeignKey("Payments.SubscriptionTypeRepeatType", "MasterID", "Payments.SubscriptionType");
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "Hash" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "Active" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "UpdatedDate" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "CreatedDate" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "CustomKey" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "SlaveID" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "MasterID" });
            DropIndex("Payments.SubscriptionTypeRepeatType", new[] { "ID" });
            DropColumn("Payments.Subscription", "IsAutoRefill");
            DropColumn("Messaging.MessageAttachment", "SortOrder");
            DropColumn("Messaging.MessageAttachment", "FileAccessTypeID");
            DropColumn("Messaging.MessageAttachment", "SeoMetaData");
            DropColumn("Messaging.MessageAttachment", "SeoDescription");
            DropColumn("Messaging.MessageAttachment", "SeoPageTitle");
            DropColumn("Messaging.MessageAttachment", "SeoKeywords");
            DropColumn("Messaging.MessageAttachment", "SeoUrl");
            DropColumn("Contacts.User", "IsSMSAllowed");
            DropColumn("Contacts.User", "Gender");
            DropColumn("Contacts.User", "DateOfBirth");
            DropTable("Payments.SubscriptionTypeRepeatType");
        }
    }
}
