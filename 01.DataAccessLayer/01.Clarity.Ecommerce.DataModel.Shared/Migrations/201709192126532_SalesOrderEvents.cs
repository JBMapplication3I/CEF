// <copyright file="201709192126532_SalesOrderEvents.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201709192126532 sales order events class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SalesOrderEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Ordering.SalesOrderEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        JsonAttributes = c.String(),
                        OldStateID = c.Int(),
                        NewStateID = c.Int(),
                        OldStatusID = c.Int(),
                        NewStatusID = c.Int(),
                        OldTypeID = c.Int(),
                        NewTypeID = c.Int(),
                        OldBalanceDue = c.Decimal(precision: 18, scale: 2),
                        NewBalanceDue = c.Decimal(precision: 18, scale: 2),
                        OldHash = c.Long(),
                        NewHash = c.Long(),
                        OldRecordSerialized = c.String(),
                        NewRecordSerialized = c.String(),
                        SalesOrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.SalesOrderID);
        }

        public override void Down()
        {
            DropForeignKey("Ordering.SalesOrderEvent", "SalesOrderID", "Ordering.SalesOrder");
            DropIndex("Ordering.SalesOrderEvent", new[] { "SalesOrderID" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "Name" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "Hash" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "Active" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "UpdatedDate" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "CustomKey" });
            DropIndex("Ordering.SalesOrderEvent", new[] { "ID" });
            DropTable("Ordering.SalesOrderEvent");
        }
    }
}
