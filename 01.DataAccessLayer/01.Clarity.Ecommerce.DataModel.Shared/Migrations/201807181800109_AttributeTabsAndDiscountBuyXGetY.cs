// <copyright file="201807181800109_AttributeTabsAndDiscountBuyXGetY.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201807181800109 attribute tabs and discount buy x coordinate get y coordinate class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AttributeTabsAndDiscountBuyXGetY : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Attributes.AttributeGroup",
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
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "Attributes.AttributeTab",
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
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            AddColumn("Discounts.Discount", "BuyXValue", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Discounts.Discount", "GetYValue", c => c.Decimal(precision: 18, scale: 4));
            AddColumn("Attributes.GeneralAttribute", "AttributeTabID", c => c.Int());
            AddColumn("Attributes.GeneralAttribute", "AttributeGroupID", c => c.Int());
            AlterColumn("Discounts.Discount", "ThresholdAmount", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            CreateIndex("Attributes.GeneralAttribute", "AttributeTabID");
            CreateIndex("Attributes.GeneralAttribute", "AttributeGroupID");
            AddForeignKey("Attributes.GeneralAttribute", "AttributeGroupID", "Attributes.AttributeGroup", "ID");
            AddForeignKey("Attributes.GeneralAttribute", "AttributeTabID", "Attributes.AttributeTab", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Attributes.GeneralAttribute", "AttributeTabID", "Attributes.AttributeTab");
            DropForeignKey("Attributes.GeneralAttribute", "AttributeGroupID", "Attributes.AttributeGroup");
            DropIndex("Attributes.GeneralAttribute", new[] { "AttributeGroupID" });
            DropIndex("Attributes.GeneralAttribute", new[] { "AttributeTabID" });
            DropIndex("Attributes.AttributeTab", new[] { "SortOrder" });
            DropIndex("Attributes.AttributeTab", new[] { "DisplayName" });
            DropIndex("Attributes.AttributeTab", new[] { "Name" });
            DropIndex("Attributes.AttributeTab", new[] { "Hash" });
            DropIndex("Attributes.AttributeTab", new[] { "Active" });
            DropIndex("Attributes.AttributeTab", new[] { "UpdatedDate" });
            DropIndex("Attributes.AttributeTab", new[] { "CustomKey" });
            DropIndex("Attributes.AttributeTab", new[] { "ID" });
            DropIndex("Attributes.AttributeGroup", new[] { "SortOrder" });
            DropIndex("Attributes.AttributeGroup", new[] { "DisplayName" });
            DropIndex("Attributes.AttributeGroup", new[] { "Name" });
            DropIndex("Attributes.AttributeGroup", new[] { "Hash" });
            DropIndex("Attributes.AttributeGroup", new[] { "Active" });
            DropIndex("Attributes.AttributeGroup", new[] { "UpdatedDate" });
            DropIndex("Attributes.AttributeGroup", new[] { "CustomKey" });
            DropIndex("Attributes.AttributeGroup", new[] { "ID" });
            AlterColumn("Discounts.Discount", "ThresholdAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("Attributes.GeneralAttribute", "AttributeGroupID");
            DropColumn("Attributes.GeneralAttribute", "AttributeTabID");
            DropColumn("Discounts.Discount", "GetYValue");
            DropColumn("Discounts.Discount", "BuyXValue");
            DropTable("Attributes.AttributeTab");
            DropTable("Attributes.AttributeGroup");
        }
    }
}
