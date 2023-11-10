namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class EndDateOnAccountContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("Accounts.AccountContact", "EndDate", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("Accounts.AccountContact", "EndDate");
        }
    }
}
