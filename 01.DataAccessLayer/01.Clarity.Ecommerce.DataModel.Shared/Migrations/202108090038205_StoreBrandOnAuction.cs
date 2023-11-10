// <copyright file="202108090038205_StoreBrandOnAuction.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202108090038205 store brand on auction class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class StoreBrandOnAuction : DbMigration
    {
        public override void Up()
        {
            AddColumn("Auctions.Auction", "BrandID", c => c.Int());
            AddColumn("Auctions.Auction", "StoreID", c => c.Int());
            CreateIndex("Auctions.Auction", "BrandID");
            CreateIndex("Auctions.Auction", "StoreID");
            AddForeignKey("Auctions.Auction", "BrandID", "Brands.Brand", "ID");
            AddForeignKey("Auctions.Auction", "StoreID", "Stores.Store", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Auctions.Auction", "StoreID", "Stores.Store");
            DropForeignKey("Auctions.Auction", "BrandID", "Brands.Brand");
            DropIndex("Auctions.Auction", new[] { "StoreID" });
            DropIndex("Auctions.Auction", new[] { "BrandID" });
            DropColumn("Auctions.Auction", "StoreID");
            DropColumn("Auctions.Auction", "BrandID");
        }
    }
}
