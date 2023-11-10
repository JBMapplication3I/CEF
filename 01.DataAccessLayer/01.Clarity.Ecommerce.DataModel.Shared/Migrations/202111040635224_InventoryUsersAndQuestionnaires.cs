// <copyright file="202111040635224_InventoryUsersAndQuestionnaires.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory users and questionnaires class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InventoryUsersAndQuestionnaires : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Inventory.InventoryLocationUser",
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
                .ForeignKey("Contacts.User", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Questionnaire.Answer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdditionalInformation = c.String(),
                        UserID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        OptionID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Questionnaire.QuestionOption", t => t.OptionID)
                .ForeignKey("Questionnaire.Question", t => t.QuestionID)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UserID)
                .Index(t => t.QuestionID)
                .Index(t => t.OptionID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Questionnaire.QuestionOption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OptionTranslationKey = c.String(maxLength: 512, unicode: false),
                        RequiresAdditionalInformation = c.Boolean(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        FollowUpQuestionID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Questionnaire.Question", t => t.FollowUpQuestionID)
                .ForeignKey("Questionnaire.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.QuestionID)
                .Index(t => t.FollowUpQuestionID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Questionnaire.Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        BrandID = c.Int(),
                        QuestionTranslationKey = c.String(maxLength: 128, unicode: false),
                        NextQuestionID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Brands.Brand", t => t.BrandID)
                .ForeignKey("Questionnaire.Question", t => t.NextQuestionID)
                .ForeignKey("Questionnaire.QuestionType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.BrandID)
                .Index(t => t.NextQuestionID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Questionnaire.QuestionType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Messaging.UserSupportRequest",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        AuthKey = c.String(maxLength: 256, unicode: false),
                        ChannelName = c.String(maxLength: 256, unicode: false),
                        Status = c.String(maxLength: 64, unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UserID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Payments.Subscription", "SalesOrderID", c => c.Int());
            CreateIndex("Payments.Subscription", "SalesOrderID");
            AddForeignKey("Payments.Subscription", "SalesOrderID", "Ordering.SalesOrder", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Messaging.UserSupportRequest", "UserID", "Contacts.User");
            DropForeignKey("Questionnaire.Answer", "UserID", "Contacts.User");
            DropForeignKey("Questionnaire.Answer", "QuestionID", "Questionnaire.Question");
            DropForeignKey("Questionnaire.Answer", "OptionID", "Questionnaire.QuestionOption");
            DropForeignKey("Questionnaire.Question", "TypeID", "Questionnaire.QuestionType");
            DropForeignKey("Questionnaire.QuestionOption", "QuestionID", "Questionnaire.Question");
            DropForeignKey("Questionnaire.Question", "NextQuestionID", "Questionnaire.Question");
            DropForeignKey("Questionnaire.QuestionOption", "FollowUpQuestionID", "Questionnaire.Question");
            DropForeignKey("Questionnaire.Question", "BrandID", "Brands.Brand");
            DropForeignKey("Payments.Subscription", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("Inventory.InventoryLocationUser", "SlaveID", "Contacts.User");
            DropForeignKey("Inventory.InventoryLocationUser", "MasterID", "Inventory.InventoryLocation");
            DropIndex("Messaging.UserSupportRequest", new[] { "Hash" });
            DropIndex("Messaging.UserSupportRequest", new[] { "Active" });
            DropIndex("Messaging.UserSupportRequest", new[] { "UpdatedDate" });
            DropIndex("Messaging.UserSupportRequest", new[] { "CreatedDate" });
            DropIndex("Messaging.UserSupportRequest", new[] { "CustomKey" });
            DropIndex("Messaging.UserSupportRequest", new[] { "UserID" });
            DropIndex("Messaging.UserSupportRequest", new[] { "ID" });
            DropIndex("Questionnaire.QuestionType", new[] { "Hash" });
            DropIndex("Questionnaire.QuestionType", new[] { "Active" });
            DropIndex("Questionnaire.QuestionType", new[] { "UpdatedDate" });
            DropIndex("Questionnaire.QuestionType", new[] { "CreatedDate" });
            DropIndex("Questionnaire.QuestionType", new[] { "CustomKey" });
            DropIndex("Questionnaire.QuestionType", new[] { "Name" });
            DropIndex("Questionnaire.QuestionType", new[] { "SortOrder" });
            DropIndex("Questionnaire.QuestionType", new[] { "DisplayName" });
            DropIndex("Questionnaire.QuestionType", new[] { "ID" });
            DropIndex("Questionnaire.Question", new[] { "Hash" });
            DropIndex("Questionnaire.Question", new[] { "Active" });
            DropIndex("Questionnaire.Question", new[] { "UpdatedDate" });
            DropIndex("Questionnaire.Question", new[] { "CreatedDate" });
            DropIndex("Questionnaire.Question", new[] { "CustomKey" });
            DropIndex("Questionnaire.Question", new[] { "NextQuestionID" });
            DropIndex("Questionnaire.Question", new[] { "BrandID" });
            DropIndex("Questionnaire.Question", new[] { "TypeID" });
            DropIndex("Questionnaire.Question", new[] { "ID" });
            DropIndex("Questionnaire.QuestionOption", new[] { "Hash" });
            DropIndex("Questionnaire.QuestionOption", new[] { "Active" });
            DropIndex("Questionnaire.QuestionOption", new[] { "UpdatedDate" });
            DropIndex("Questionnaire.QuestionOption", new[] { "CreatedDate" });
            DropIndex("Questionnaire.QuestionOption", new[] { "CustomKey" });
            DropIndex("Questionnaire.QuestionOption", new[] { "FollowUpQuestionID" });
            DropIndex("Questionnaire.QuestionOption", new[] { "QuestionID" });
            DropIndex("Questionnaire.QuestionOption", new[] { "ID" });
            DropIndex("Questionnaire.Answer", new[] { "Hash" });
            DropIndex("Questionnaire.Answer", new[] { "Active" });
            DropIndex("Questionnaire.Answer", new[] { "UpdatedDate" });
            DropIndex("Questionnaire.Answer", new[] { "CreatedDate" });
            DropIndex("Questionnaire.Answer", new[] { "CustomKey" });
            DropIndex("Questionnaire.Answer", new[] { "OptionID" });
            DropIndex("Questionnaire.Answer", new[] { "QuestionID" });
            DropIndex("Questionnaire.Answer", new[] { "UserID" });
            DropIndex("Questionnaire.Answer", new[] { "ID" });
            DropIndex("Payments.Subscription", new[] { "SalesOrderID" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "Hash" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "Active" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "UpdatedDate" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "CreatedDate" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "CustomKey" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "SlaveID" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "MasterID" });
            DropIndex("Inventory.InventoryLocationUser", new[] { "ID" });
            DropColumn("Payments.Subscription", "SalesOrderID");
            DropTable("Messaging.UserSupportRequest");
            DropTable("Questionnaire.QuestionType");
            DropTable("Questionnaire.Question");
            DropTable("Questionnaire.QuestionOption");
            DropTable("Questionnaire.Answer");
            DropTable("Inventory.InventoryLocationUser");
        }
    }
}
