// <copyright file="202104201836548_SalesQuotesMastersAndSubs.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202104201836548 sales quotes masters and subs class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesQuotesMastersAndSubs : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Quoting.SalesQuote", name: "SalesGroupAsMasterID", newName: "SalesGroupAsRequestMasterID");
            RenameColumn(table: "Quoting.SalesQuote", name: "SalesGroupAsResponseID", newName: "SalesGroupAsResponseMasterID");
            // RenameIndex(table: "Quoting.SalesQuote", name: "IX_SalesGroupAsMasterID", newName: "IX_SalesGroupAsRequestMasterID");
            CreateIndex("Quoting.SalesQuote", "SalesGroupAsRequestMasterID");
            RenameIndex(table: "Quoting.SalesQuote", name: "IX_SalesGroupAsResponseID", newName: "IX_SalesGroupAsResponseMasterID");
            AddColumn("Quoting.SalesQuote", "SalesGroupAsRequestSubID", c => c.Int());
            AddColumn("Quoting.SalesQuote", "SalesGroupAsResponseSubID", c => c.Int());
            AddColumn("Sampling.SampleRequest", "SalesGroupID", c => c.Int());
            CreateIndex("Quoting.SalesQuote", "SalesGroupAsRequestSubID");
            CreateIndex("Quoting.SalesQuote", "SalesGroupAsResponseSubID");
            CreateIndex("Sampling.SampleRequest", "SalesGroupID");
            AddForeignKey("Quoting.SalesQuote", "SalesGroupAsRequestSubID", "Sales.SalesGroup", "ID");
            AddForeignKey("Quoting.SalesQuote", "SalesGroupAsResponseSubID", "Sales.SalesGroup", "ID");
            AddForeignKey("Sampling.SampleRequest", "SalesGroupID", "Sales.SalesGroup", "ID");
        }

        public override void Down()
        {
            DropForeignKey("Sampling.SampleRequest", "SalesGroupID", "Sales.SalesGroup");
            DropForeignKey("Quoting.SalesQuote", "SalesGroupAsResponseSubID", "Sales.SalesGroup");
            DropForeignKey("Quoting.SalesQuote", "SalesGroupAsRequestSubID", "Sales.SalesGroup");
            DropIndex("Sampling.SampleRequest", new[] { "SalesGroupID" });
            DropIndex("Quoting.SalesQuote", new[] { "SalesGroupAsResponseSubID" });
            DropIndex("Quoting.SalesQuote", new[] { "SalesGroupAsRequestSubID" });
            DropColumn("Sampling.SampleRequest", "SalesGroupID");
            DropColumn("Quoting.SalesQuote", "SalesGroupAsResponseSubID");
            DropColumn("Quoting.SalesQuote", "SalesGroupAsRequestSubID");
            RenameIndex(table: "Quoting.SalesQuote", name: "IX_SalesGroupAsResponseMasterID", newName: "IX_SalesGroupAsResponseID");
            DropIndex("Quoting.SalesQuote", new[] { "SalesGroupAsRequestMasterID" });
            // RenameIndex(table: "Quoting.SalesQuote", name: "IX_SalesGroupAsRequestMasterID", newName: "IX_SalesGroupAsMasterID");
            RenameColumn(table: "Quoting.SalesQuote", name: "SalesGroupAsResponseMasterID", newName: "SalesGroupAsResponseID");
            RenameColumn(table: "Quoting.SalesQuote", name: "SalesGroupAsRequestMasterID", newName: "SalesGroupAsMasterID");
        }
    }
}
