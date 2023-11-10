// <copyright file="201806202052536_FixSalesGroupMasterLinks.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201806202052536 fix sales group master links class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixSalesGroupMasterLinks : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Ordering.SalesOrder", name: "SalesGroupAsMaster", newName: "MasterSalesOrderID");
            RenameColumn(table: "Quoting.SalesQuote", name: "SalesGroupAsMaster", newName: "MasterSalesQuoteID");
            RenameIndex(table: "Ordering.SalesOrder", name: "IX_SalesGroupAsMaster", newName: "IX_MasterSalesOrderID");
            RenameIndex(table: "Quoting.SalesQuote", name: "IX_SalesGroupAsMaster", newName: "IX_MasterSalesQuoteID");
        }

        public override void Down()
        {
            RenameIndex(table: "Quoting.SalesQuote", name: "IX_MasterSalesQuoteID", newName: "IX_SalesGroupAsMaster");
            RenameIndex(table: "Ordering.SalesOrder", name: "IX_MasterSalesOrderID", newName: "IX_SalesGroupAsMaster");
            RenameColumn(table: "Quoting.SalesQuote", name: "MasterSalesQuoteID", newName: "SalesGroupAsMaster");
            RenameColumn(table: "Ordering.SalesOrder", name: "MasterSalesOrderID", newName: "SalesGroupAsMaster");
        }
    }
}
