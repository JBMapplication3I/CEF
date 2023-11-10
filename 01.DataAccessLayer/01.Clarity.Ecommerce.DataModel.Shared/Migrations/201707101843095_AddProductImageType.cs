// <copyright file="201707101843095_AddProductImageType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201707101843095 add product image type class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddProductImageType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Products.ProductImageType",
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

            AddColumn("Products.ProductImage", "TypeID", c => c.Int());
            CreateIndex("Products.ProductImage", "TypeID");
            AddForeignKey("Products.ProductImage", "TypeID", "Products.ProductImageType", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Products.ProductImage", "TypeID", "Products.ProductImageType");
            DropIndex("Products.ProductImageType", new[] { "SortOrder" });
            DropIndex("Products.ProductImageType", new[] { "DisplayName" });
            DropIndex("Products.ProductImageType", new[] { "Name" });
            DropIndex("Products.ProductImageType", new[] { "Active" });
            DropIndex("Products.ProductImageType", new[] { "UpdatedDate" });
            DropIndex("Products.ProductImageType", new[] { "CustomKey" });
            DropIndex("Products.ProductImageType", new[] { "ID" });
            DropIndex("Products.ProductImage", new[] { "TypeID" });
            DropColumn("Products.ProductImage", "TypeID");
            DropTable("Products.ProductImageType");
        }
    }
}
