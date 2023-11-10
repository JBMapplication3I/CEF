namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductRequiresRolesLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Products.Product", "RequiresRoles", c => c.String(maxLength: 2048));
        }
        
        public override void Down()
        {
            AlterColumn("Products.Product", "RequiresRoles", c => c.String(maxLength: 512));
        }
    }
}
