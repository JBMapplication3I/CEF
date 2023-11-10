// <copyright file="201612202054553_DropAttributeFilterValuesAndAccountAddresses.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201612202054553 drop attribute filter values and account addresses class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DropAttributeFilterValuesAndAccountAddresses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Products.ProductAttributeFilterValue", "AttributeID", "Attributes.GeneralAttribute");
            DropForeignKey("Products.ProductAttributeFilterValue", "MasterID", "Products.Product");
            DropForeignKey("Accounts.AccountAddress", "AccountID", "Accounts.Account");
            DropForeignKey("Accounts.AccountAddress", "AddressID", "Geography.Address");
            DropIndex("Accounts.AccountAddress", new[] { "ID" });
            DropIndex("Accounts.AccountAddress", new[] { "CustomKey" });
            DropIndex("Accounts.AccountAddress", new[] { "UpdatedDate" });
            DropIndex("Accounts.AccountAddress", new[] { "Active" });
            DropIndex("Accounts.AccountAddress", new[] { "Name" });
            DropIndex("Accounts.AccountAddress", new[] { "AccountID" });
            DropIndex("Accounts.AccountAddress", new[] { "AddressID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "ID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "CustomKey" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "UpdatedDate" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "Active" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "AttributeID" });
            DropIndex("Products.ProductAttributeFilterValue", new[] { "MasterID" });
            DropTable("Accounts.AccountAddress");
            DropTable("Products.ProductAttributeFilterValue");
        }

        public override void Down()
        {
            CreateTable(
                "Products.ProductAttributeFilterValue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        SortOrder = c.Int(),
                        Value = c.String(nullable: false, maxLength: 1000, unicode: false),
                        UnitOfMeasure = c.String(maxLength: 64, unicode: false),
                        AttributeID = c.Int(nullable: false),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Accounts.AccountAddress",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        IsPrimary = c.Boolean(nullable: false),
                        IsBilling = c.Boolean(nullable: false),
                        TransmittedToERP = c.Boolean(nullable: false),
                        AccountID = c.Int(nullable: false),
                        AddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateIndex("Products.ProductAttributeFilterValue", "MasterID");
            CreateIndex("Products.ProductAttributeFilterValue", "AttributeID");
            CreateIndex("Products.ProductAttributeFilterValue", "Active");
            CreateIndex("Products.ProductAttributeFilterValue", "UpdatedDate");
            CreateIndex("Products.ProductAttributeFilterValue", "CustomKey");
            CreateIndex("Products.ProductAttributeFilterValue", "ID");
            CreateIndex("Accounts.AccountAddress", "AddressID");
            CreateIndex("Accounts.AccountAddress", "AccountID");
            CreateIndex("Accounts.AccountAddress", "Name");
            CreateIndex("Accounts.AccountAddress", "Active");
            CreateIndex("Accounts.AccountAddress", "UpdatedDate");
            CreateIndex("Accounts.AccountAddress", "CustomKey");
            CreateIndex("Accounts.AccountAddress", "ID");
            AddForeignKey("Accounts.AccountAddress", "AddressID", "Geography.Address", "ID", cascadeDelete: true);
            AddForeignKey("Accounts.AccountAddress", "AccountID", "Accounts.Account", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAttributeFilterValue", "MasterID", "Products.Product", "ID", cascadeDelete: true);
            AddForeignKey("Products.ProductAttributeFilterValue", "AttributeID", "Attributes.GeneralAttribute", "ID", cascadeDelete: true);
        }
    }
}
