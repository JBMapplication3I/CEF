// <copyright file="201703231920001_Groups.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201703231920001 groups class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Groups.Group",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        GroupOwnerID = c.Int(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.GroupOwnerID)
                .ForeignKey("Groups.Group", t => t.ParentID)
                .ForeignKey("Groups.GroupStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Groups.GroupType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.GroupOwnerID)
                .Index(t => t.ParentID);

            CreateTable(
                "Groups.GroupStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
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
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Groups.GroupType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
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
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Groups.GroupUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Groups.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);

            AddColumn("Messaging.MessageRecipient", "GroupID", c => c.Int());
            AddColumn("Contacts.RoleUser", "GroupID", c => c.Int());
            CreateIndex("Messaging.MessageRecipient", "GroupID");
            CreateIndex("Contacts.RoleUser", "GroupID");
            AddForeignKey("Messaging.MessageRecipient", "GroupID", "Groups.Group", "ID");
            AddForeignKey("Contacts.RoleUser", "GroupID", "Groups.Group", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Groups.GroupUser", "UserID", "Contacts.User");
            DropForeignKey("Groups.GroupUser", "GroupID", "Groups.Group");
            DropForeignKey("Contacts.RoleUser", "GroupID", "Groups.Group");
            DropForeignKey("Messaging.MessageRecipient", "GroupID", "Groups.Group");
            DropForeignKey("Groups.Group", "TypeID", "Groups.GroupType");
            DropForeignKey("Groups.Group", "StatusID", "Groups.GroupStatus");
            DropForeignKey("Groups.Group", "ParentID", "Groups.Group");
            DropForeignKey("Groups.Group", "GroupOwnerID", "Contacts.User");
            DropIndex("Groups.GroupUser", new[] { "UserID" });
            DropIndex("Groups.GroupUser", new[] { "GroupID" });
            DropIndex("Groups.GroupUser", new[] { "Active" });
            DropIndex("Groups.GroupUser", new[] { "UpdatedDate" });
            DropIndex("Groups.GroupUser", new[] { "CustomKey" });
            DropIndex("Groups.GroupUser", new[] { "ID" });
            DropIndex("Contacts.RoleUser", new[] { "GroupID" });
            DropIndex("Groups.GroupType", new[] { "SortOrder" });
            DropIndex("Groups.GroupType", new[] { "DisplayName" });
            DropIndex("Groups.GroupType", new[] { "Name" });
            DropIndex("Groups.GroupType", new[] { "Active" });
            DropIndex("Groups.GroupType", new[] { "UpdatedDate" });
            DropIndex("Groups.GroupType", new[] { "CustomKey" });
            DropIndex("Groups.GroupType", new[] { "ID" });
            DropIndex("Groups.GroupStatus", new[] { "SortOrder" });
            DropIndex("Groups.GroupStatus", new[] { "DisplayName" });
            DropIndex("Groups.GroupStatus", new[] { "Name" });
            DropIndex("Groups.GroupStatus", new[] { "Active" });
            DropIndex("Groups.GroupStatus", new[] { "UpdatedDate" });
            DropIndex("Groups.GroupStatus", new[] { "CustomKey" });
            DropIndex("Groups.GroupStatus", new[] { "ID" });
            DropIndex("Groups.Group", new[] { "ParentID" });
            DropIndex("Groups.Group", new[] { "GroupOwnerID" });
            DropIndex("Groups.Group", new[] { "StatusID" });
            DropIndex("Groups.Group", new[] { "TypeID" });
            DropIndex("Groups.Group", new[] { "Name" });
            DropIndex("Groups.Group", new[] { "Active" });
            DropIndex("Groups.Group", new[] { "UpdatedDate" });
            DropIndex("Groups.Group", new[] { "CustomKey" });
            DropIndex("Groups.Group", new[] { "ID" });
            DropIndex("Messaging.MessageRecipient", new[] { "GroupID" });
            DropColumn("Contacts.RoleUser", "GroupID");
            DropColumn("Messaging.MessageRecipient", "GroupID");
            DropTable("Groups.GroupUser");
            DropTable("Groups.GroupType");
            DropTable("Groups.GroupStatus");
            DropTable("Groups.Group");
        }
    }
}
