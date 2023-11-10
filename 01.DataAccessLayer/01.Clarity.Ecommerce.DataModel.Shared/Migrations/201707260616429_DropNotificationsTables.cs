// <copyright file="201707260616429_DropNotificationsTables.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201707260616429 drop notifications tables class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropNotificationsTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Notifications.NotificationMessage", "AccountID", "Accounts.Account");
            DropForeignKey("Notifications.NotificationMessage", "UserID", "Contacts.User");
            DropForeignKey("Notifications.Notification", "UserID", "Contacts.User");
            DropIndex("Notifications.NotificationMessage", new[] { "ID" });
            DropIndex("Notifications.NotificationMessage", new[] { "CustomKey" });
            DropIndex("Notifications.NotificationMessage", new[] { "UpdatedDate" });
            DropIndex("Notifications.NotificationMessage", new[] { "Active" });
            DropIndex("Notifications.NotificationMessage", new[] { "Hash" });
            DropIndex("Notifications.NotificationMessage", new[] { "AccountID" });
            DropIndex("Notifications.NotificationMessage", new[] { "UserID" });
            DropIndex("Notifications.Notification", new[] { "ID" });
            DropIndex("Notifications.Notification", new[] { "CustomKey" });
            DropIndex("Notifications.Notification", new[] { "UpdatedDate" });
            DropIndex("Notifications.Notification", new[] { "Active" });
            DropIndex("Notifications.Notification", new[] { "Hash" });
            DropIndex("Notifications.Notification", new[] { "UserID" });
            DropIndex("Notifications.Action", new[] { "ID" });
            DropIndex("Notifications.Action", new[] { "CustomKey" });
            DropIndex("Notifications.Action", new[] { "UpdatedDate" });
            DropIndex("Notifications.Action", new[] { "Active" });
            DropIndex("Notifications.Action", new[] { "Hash" });
            DropTable("Notifications.NotificationMessage");
            DropTable("Notifications.Notification");
            DropTable("Notifications.Action");
        }

        public override void Down()
        {
            CreateTable(
                "Notifications.Action",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        SystemMessage = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        ActionType = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Notifications.Notification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        MonitorInstance = c.String(unicode: false),
                        MessageTemplate = c.String(unicode: false),
                        LastMessage = c.Int(),
                        ScheduledDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        WorkflowInstance = c.String(unicode: false),
                        ActionCodeMask = c.Int(),
                        Filter = c.String(unicode: false),
                        DistributionList = c.String(unicode: false),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Notifications.NotificationMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Message = c.String(unicode: false),
                        WorkflowInstance = c.String(unicode: false),
                        ActionID = c.Int(),
                        EntityID = c.Int(),
                        AccountID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Notifications.Action", "Hash");
            CreateIndex("Notifications.Action", "Active");
            CreateIndex("Notifications.Action", "UpdatedDate");
            CreateIndex("Notifications.Action", "CustomKey");
            CreateIndex("Notifications.Action", "ID");
            CreateIndex("Notifications.Notification", "UserID");
            CreateIndex("Notifications.Notification", "Hash");
            CreateIndex("Notifications.Notification", "Active");
            CreateIndex("Notifications.Notification", "UpdatedDate");
            CreateIndex("Notifications.Notification", "CustomKey");
            CreateIndex("Notifications.Notification", "ID");
            CreateIndex("Notifications.NotificationMessage", "UserID");
            CreateIndex("Notifications.NotificationMessage", "AccountID");
            CreateIndex("Notifications.NotificationMessage", "Hash");
            CreateIndex("Notifications.NotificationMessage", "Active");
            CreateIndex("Notifications.NotificationMessage", "UpdatedDate");
            CreateIndex("Notifications.NotificationMessage", "CustomKey");
            CreateIndex("Notifications.NotificationMessage", "ID");
            AddForeignKey("Notifications.Notification", "UserID", "Contacts.User", "ID");
            AddForeignKey("Notifications.NotificationMessage", "UserID", "Contacts.User", "ID");
            AddForeignKey("Notifications.NotificationMessage", "AccountID", "Accounts.Account", "ID");
        }
    }
}
