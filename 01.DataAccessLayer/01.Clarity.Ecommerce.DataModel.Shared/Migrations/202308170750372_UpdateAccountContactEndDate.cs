namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateAccountContactEndDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Accounts.AccountContact", "EndDate", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("Accounts.AccountContact", "EndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}
