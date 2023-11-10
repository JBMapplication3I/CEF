// <copyright file="202002011534012_CurrencyConvAndWalletCurrency.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202002011534012 currency convert and wallet currency class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CurrencyConvAndWalletCurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Currencies.CurrencyConversion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 24, scale: 20),
                        StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        StartingCurrencyID = c.Int(nullable: false),
                        EndingCurrencyID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Currencies.Currency", t => t.EndingCurrencyID)
                .ForeignKey("Currencies.Currency", t => t.StartingCurrencyID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.StartingCurrencyID)
                .Index(t => t.EndingCurrencyID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("Payments.Wallet", "CurrencyID", c => c.Int());
            CreateIndex("Payments.Wallet", "CurrencyID");
            AddForeignKey("Payments.Wallet", "CurrencyID", "Currencies.Currency", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Payments.Wallet", "CurrencyID", "Currencies.Currency");
            DropForeignKey("Currencies.CurrencyConversion", "StartingCurrencyID", "Currencies.Currency");
            DropForeignKey("Currencies.CurrencyConversion", "EndingCurrencyID", "Currencies.Currency");
            DropIndex("Payments.Wallet", new[] { "CurrencyID" });
            DropIndex("Currencies.CurrencyConversion", new[] { "Hash" });
            DropIndex("Currencies.CurrencyConversion", new[] { "Active" });
            DropIndex("Currencies.CurrencyConversion", new[] { "UpdatedDate" });
            DropIndex("Currencies.CurrencyConversion", new[] { "CreatedDate" });
            DropIndex("Currencies.CurrencyConversion", new[] { "CustomKey" });
            DropIndex("Currencies.CurrencyConversion", new[] { "EndingCurrencyID" });
            DropIndex("Currencies.CurrencyConversion", new[] { "StartingCurrencyID" });
            DropIndex("Currencies.CurrencyConversion", new[] { "ID" });
            DropColumn("Payments.Wallet", "CurrencyID");
            DropTable("Currencies.CurrencyConversion");
        }
    }
}
