// <copyright file="201904240227151_AddressCompanyFieldNotRequired.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201904240227151 address company field not required class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddressCompanyFieldNotRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("Geography.Address", new[] { "Name" });
            DropIndex("Geography.Address", new[] { "Phone" });
            DropIndex("Geography.Address", new[] { "Fax" });
            DropIndex("Geography.Address", new[] { "Email" });
            AlterColumn("Geography.Address", "Name", c => c.String(maxLength: 255, unicode: false));
            DropColumn("Geography.Address", "Description");
            DropColumn("Geography.Address", "Phone");
            DropColumn("Geography.Address", "Fax");
            DropColumn("Geography.Address", "Email");
            DropColumn("Geography.Address", "Phone2");
            DropColumn("Geography.Address", "Phone3");
        }

        public override void Down()
        {
            AddColumn("Geography.Address", "Phone3", c => c.String(maxLength: 32));
            AddColumn("Geography.Address", "Phone2", c => c.String(maxLength: 32));
            AddColumn("Geography.Address", "Email", c => c.String(maxLength: 256, unicode: false));
            AddColumn("Geography.Address", "Fax", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Geography.Address", "Phone", c => c.String(maxLength: 64, unicode: false));
            AddColumn("Geography.Address", "Description", c => c.String(unicode: false));
            AlterColumn("Geography.Address", "Name", c => c.String(maxLength: 256, unicode: false));
            CreateIndex("Geography.Address", "Email");
            CreateIndex("Geography.Address", "Fax");
            CreateIndex("Geography.Address", "Phone");
            CreateIndex("Geography.Address", "Name");
        }
    }
}
