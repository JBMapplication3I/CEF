// <copyright file="202001281947047_EventLogRecordLogLevel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202001281947047 event log record log level class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EventLogRecordLogLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("System.SystemLog", "LogLevel", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("System.SystemLog", "LogLevel");
        }
    }
}
