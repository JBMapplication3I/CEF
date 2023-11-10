// <copyright file="SqlSchemaAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the T-SQL Schema attribute class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Attribute for holding the name of the schema for a T-SQL table. This class cannot be inherited.</summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SqlSchemaAttribute : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="SqlSchemaAttribute"/> class.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="table"> The table.</param>
        public SqlSchemaAttribute(string schema, string table)
        {
            Schema = schema;
            Table = table;
            Both = schema + "." + table;
        }

        /// <summary>Gets the schema.</summary>
        /// <value>The schema.</value>
        public string Schema { get; }

        /// <summary>Gets the table.</summary>
        /// <value>The table.</value>
        public string Table { get; }

        /// <summary>Gets the both.</summary>
        /// <value>The both.</value>
        public string Both { get; }
    }
}
