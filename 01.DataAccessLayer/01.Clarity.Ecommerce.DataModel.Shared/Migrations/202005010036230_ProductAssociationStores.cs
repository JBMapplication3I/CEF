// <copyright file="202005010036230_ProductAssociationStores.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202005010036230 product association stores class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProductAssociationStores : DbMigration
    {
        public override void Up()
        {
            AddColumn("Products.ProductAssociation", "StoreID", c => c.Int());
            CreateIndex("Products.ProductAssociation", "StoreID");
            AddForeignKey("Products.ProductAssociation", "StoreID", "Stores.Store", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Products.ProductAssociation", "StoreID", "Stores.Store");
            DropIndex("Products.ProductAssociation", new[] { "StoreID" });
            DropColumn("Products.ProductAssociation", "StoreID");
        }
    }
}
