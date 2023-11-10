// <copyright file="201810222046204_SalesGroupLinks.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201810222046204 sales group links class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    public partial class SalesGroupLinks : CEFDbMigrationBase
    {
        public override void Up()
        {
            AlterColumn("Ordering.SalesOrder", "PaymentTransactionID", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("Ordering.SalesOrder", "TaxTransactionID", c => c.String(maxLength: 256, unicode: false));

            this.DropConstraintWithoutKnowingFullName(schema: "Ordering", table: "SalesOrder", column: "SalesGroupAsMastear");
            this.DropConstraintWithoutKnowingFullName(schema: "Quoting", table: "SalesQuote", column: "SalesGroupAsMaster");
            DropForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Ordering.SalesOrder", dependentColumn: "SalesGroupAsMaster");
            DropForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Quoting.SalesQuote", dependentColumn: "SalesGroupAsMaster");
            DropForeignKey(principalTable: "Ordering.SalesOrder", dependentTable: "Sales.SalesGroup", dependentColumn: "MasterSalesOrderID");
            DropForeignKey(principalTable: "Quoting.SalesQuote",  dependentTable: "Sales.SalesGroup", dependentColumn: "MasterSalesQuoteID");
            DropIndex(table: "Ordering.SalesOrder", name: "IX_MasterSalesOrderID");
            DropIndex(table: "Quoting.SalesQuote", name: "IX_MasterSalesQuoteID");
            DropColumn("Sales.SalesGroup", "MasterSalesQuoteID");
            DropColumn("Sales.SalesGroup", "MasterSalesOrderID");
            DropColumn("Ordering.SalesOrder", "MasterSalesOrderID");
            DropColumn("Quoting.SalesQuote", "MasterSalesQuoteID");
            AddForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Ordering.SalesOrder", dependentColumn: "SalesGroupAsMasterID");
            AddForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Quoting.SalesQuote", dependentColumn: "SalesGroupAsMasterID");
        }

        public override void Down()
        {
            DropForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Ordering.SalesOrder", dependentColumn: "SalesGroupAsMasterID");
            DropForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Quoting.SalesQuote", dependentColumn: "SalesGroupAsMasterID");
            AddColumn("Quoting.SalesQuote", "MasterSalesQuoteID", c => c.Int());
            AddColumn("Ordering.SalesOrder", "MasterSalesOrderID", c => c.Int());
            AddColumn("Sales.SalesGroup", "MasterSalesOrderID", c => c.Int());
            AddColumn("Sales.SalesGroup", "MasterSalesQuoteID", c => c.Int());
            CreateIndex(table: "Ordering.SalesOrder", column: "MasterSalesOrderID", name: "IX_MasterSalesOrderID");
            CreateIndex(table: "Quoting.SalesQuote", column: "MasterSalesQuoteID", name: "IX_MasterSalesQuoteID");
            AddForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Ordering.SalesOrder", dependentColumn: "MasterSalesOrderID");
            AddForeignKey(principalTable: "Sales.SalesGroup", dependentTable: "Quoting.SalesQuote", dependentColumn: "MasterSalesQuoteID");

            AlterColumn("Ordering.SalesOrder", "TaxTransactionID", c => c.String(maxLength: 256));
            AlterColumn("Ordering.SalesOrder", "PaymentTransactionID", c => c.String(maxLength: 256));
        }
    }
}
