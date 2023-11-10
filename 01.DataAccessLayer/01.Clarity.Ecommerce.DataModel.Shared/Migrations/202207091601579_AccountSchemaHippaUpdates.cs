// <copyright file="202207091601579_AccountSchemaHippaUpdates.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202202041920550 fix lot group assignment class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AccountSchemaHippaUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("Accounts.Account", "BusinessType", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "DEANumber", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "DunsNumber", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "EIN", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "MedicalLicenseHolderName", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "MedicalLicenseNumber", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "MedicalLicenseState", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "PreferredInvoiceMethod", c => c.String(maxLength: 50, unicode: false));
            AddColumn("Accounts.Account", "SalesmanCode", c => c.String(maxLength: 50, unicode: false));
        }

        public override void Down()
        {
            DropColumn("Accounts.Account", "SalesmanCode");
            DropColumn("Accounts.Account", "PreferredInvoiceMethod");
            DropColumn("Accounts.Account", "MedicalLicenseState");
            DropColumn("Accounts.Account", "MedicalLicenseNumber");
            DropColumn("Accounts.Account", "MedicalLicenseHolderName");
            DropColumn("Accounts.Account", "EIN");
            DropColumn("Accounts.Account", "DunsNumber");
            DropColumn("Accounts.Account", "DEANumber");
            DropColumn("Accounts.Account", "BusinessType");
        }
    }
}
