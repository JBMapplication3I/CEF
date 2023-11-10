// <copyright file="MigrationExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the migration extensions class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    public static class MigrationExtensions
    {
        /// <summary>A CEFDbMigration extension method that drop constraint without knowing full name.</summary>
        /// <param name="migration">The migration to act on.</param>
        /// <param name="schema">   The schema.</param>
        /// <param name="table">    The table.</param>
        /// <param name="column">   The column.</param>
        public static void DropConstraintWithoutKnowingFullName(this CEFDbMigrationBase migration, string schema, string table, string column)
        {
            // Clear constraint from column without knowing the full name of it
            // EF appends randomized hex keys at the end of their constraints
            migration.RunSql($@"
DECLARE @Command nvarchar(1000)
SELECT @Command = 'ALTER TABLE [{schema}].[{table}] drop constraint ' + [d].[name]
FROM [sys].[tables] t
    JOIN [sys].[default_constraints] d ON [d].[parent_object_id] = [t].[object_id]
    JOIN [sys].[columns] c ON [c].[object_id] = [t].[object_id] AND [c].[column_id] = [d].[parent_column_id]
WHERE [t].[name] = '{table}' AND [c].[name] = '{column}'
EXECUTE (@Command)");
        }
    }
}
