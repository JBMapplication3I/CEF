// <copyright file="201801252013363_AttributesIsComparibleAndHideFromStorefront.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201801252013363 attributes is comparible and hide from storefront class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AttributesIsComparibleAndHideFromStorefront : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [Attributes].[GeneralAttribute] SET [IsMarkup] = 0 WHERE [IsMarkup] IS NULL");
            AddColumn("Attributes.GeneralAttribute", "HideFromStorefront", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("Attributes.GeneralAttribute", "IsComparable", c => c.Boolean(nullable: false, defaultValue: false));
            AlterColumn("Attributes.GeneralAttribute", "IsMarkup", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            AlterColumn("Attributes.GeneralAttribute", "IsMarkup", c => c.Boolean());
            DropColumn("Attributes.GeneralAttribute", "IsComparable");
            DropColumn("Attributes.GeneralAttribute", "HideFromStorefront");
        }
    }
}
