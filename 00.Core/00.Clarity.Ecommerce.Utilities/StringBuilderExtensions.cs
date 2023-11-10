// <copyright file="StringBuilderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the string extensions class</summary>
namespace Clarity.Ecommerce
{
    using System.Text;
    using Utilities;

    /// <summary>A string builder extensions.</summary>
    public static class StringBuilderExtensions
    {
        /// <summary>A string builder extension method that appends a string only if the condition is met.</summary>
        /// <param name="builder">    The string builder.</param>
        /// <param name="condition">  The condition to meet.</param>
        /// <param name="trueAppend"> The string to append if the condition is true.</param>
        /// <param name="falseAppend">The string to append if the condition is false (defaults to null, nothing to append).</param>
        /// <returns>The string builder that was passed in.</returns>
        public static StringBuilder AppendIf(
            this StringBuilder builder,
            bool condition,
            string trueAppend,
            string? falseAppend = null)
        {
            if (condition)
            {
                builder.Append(trueAppend);
            }
            else if (Contract.CheckValidKey(falseAppend))
            {
                builder.Append(falseAppend);
            }
            return builder;
        }
    }
}
