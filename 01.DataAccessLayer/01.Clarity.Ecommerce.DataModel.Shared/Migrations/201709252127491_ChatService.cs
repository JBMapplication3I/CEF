// <copyright file="201709252127491_ChatService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709252127491 chat service class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChatService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Messaging.ConversationUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        LastHeartbeat = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsTyping = c.Boolean(),
                        ConversationID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Messaging.Conversation", t => t.ConversationID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.ConversationID)
                .Index(t => t.UserID);

            CreateTable(
                "Contacts.UserOnlineStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            AddColumn("Contacts.User", "UserOnlineStatusID", c => c.Int());
            AddColumn("Messaging.Conversation", "HasEnded", c => c.Boolean());
            AddColumn("Messaging.Conversation", "CopyUserWhenEnded", c => c.Boolean());
            AddColumn("Messaging.MessageRecipient", "ReadAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Messaging.MessageRecipient", "ArchivedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("Contacts.User", "UserOnlineStatusID");
            AddForeignKey("Contacts.User", "UserOnlineStatusID", "Contacts.UserOnlineStatus", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Contacts.User", "UserOnlineStatusID", "Contacts.UserOnlineStatus");
            DropForeignKey("Messaging.ConversationUser", "UserID", "Contacts.User");
            DropForeignKey("Messaging.ConversationUser", "ConversationID", "Messaging.Conversation");
            DropIndex("Contacts.UserOnlineStatus", new[] { "SortOrder" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "DisplayName" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "Name" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "Hash" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "Active" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "UpdatedDate" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "CustomKey" });
            DropIndex("Contacts.UserOnlineStatus", new[] { "ID" });
            DropIndex("Messaging.ConversationUser", new[] { "UserID" });
            DropIndex("Messaging.ConversationUser", new[] { "ConversationID" });
            DropIndex("Messaging.ConversationUser", new[] { "Hash" });
            DropIndex("Messaging.ConversationUser", new[] { "Active" });
            DropIndex("Messaging.ConversationUser", new[] { "UpdatedDate" });
            DropIndex("Messaging.ConversationUser", new[] { "CustomKey" });
            DropIndex("Messaging.ConversationUser", new[] { "ID" });
            DropIndex("Contacts.User", new[] { "UserOnlineStatusID" });
            DropColumn("Messaging.MessageRecipient", "ArchivedAt");
            DropColumn("Messaging.MessageRecipient", "ReadAt");
            DropColumn("Messaging.Conversation", "CopyUserWhenEnded");
            DropColumn("Messaging.Conversation", "HasEnded");
            DropColumn("Contacts.User", "UserOnlineStatusID");
            DropTable("Contacts.UserOnlineStatus");
            DropTable("Messaging.ConversationUser");
        }
    }
}
