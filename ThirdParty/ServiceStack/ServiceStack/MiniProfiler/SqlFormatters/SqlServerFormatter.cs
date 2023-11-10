﻿#if !NETSTANDARD1_6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack.Text;

namespace ServiceStack.MiniProfiler.SqlFormatters
{
    /// <summary>
    /// Formats SQL server queries with a DECLARE up top for parameter values
    /// </summary>
    public class SqlServerFormatter : ISqlFormatter
    {
        private static readonly Dictionary<DbType, Func<SqlTimingParameter, string>> paramTranslator;

        private static Func<SqlTimingParameter, string> GetWithLenFormatter(string native)
        {
            var capture = native;
            return p =>
            {
                if (p.Size < 1) { return capture; } else { return capture + "(" + (p.Size > 8000 ? "max" : p.Size.ToString()) + ")"; }
            };
        }

        static SqlServerFormatter()
        {
            paramTranslator = new()
            {
                {DbType.AnsiString, GetWithLenFormatter("varchar")},
                {DbType.String, GetWithLenFormatter("nvarchar")},
                {DbType.AnsiStringFixedLength, GetWithLenFormatter("char")},
                {DbType.StringFixedLength, GetWithLenFormatter("nchar")},
                {DbType.Byte, p => "tinyint"},
                {DbType.Int16, p => "smallint"},
                {DbType.Int32, p => "int"},
                {DbType.Int64, p => "bigint"},
                {DbType.DateTime, p => "datetime"},
                {DbType.Guid, p => "uniqueidentifier"},
                {DbType.Boolean, p => "bit"},
            };

        }

        /// <summary>
        /// Formats the SQL in a SQL-Server friendly way, with DECLARE statements for the parameters up top.
        /// </summary>
        /// <param name="timing">The SqlTiming to format</param>
        /// <returns>A formatted SQL string</returns>
        public string FormatSql(SqlTiming timing)
        {

            if (timing.Parameters == null || timing.Parameters.Count == 0)
            {
                return timing.CommandString;
            }

            var buffer = StringBuilderCache.Allocate();

            buffer.Append("DECLARE ");

            var first = true;
            foreach (var p in timing.Parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    buffer.AppendLine(",").Append(new string(' ', 8));
                }

                var parsed = DbType.AnsiString;
                string resolvedType = null;

                var validEnum = false;
                try
                {
                    parsed = p.DbType.ToEnum<DbType>();
                    validEnum = true;
                }
                catch { }

                if (!validEnum)
                {
                    resolvedType = p.DbType;
                }

                if (resolvedType == null)
                {
                    if (paramTranslator.TryGetValue(parsed, out var translator))
                    {
                        resolvedType = translator(p);
                    }
                    resolvedType = resolvedType ?? p.DbType;
                }

                var niceName = p.Name;
                if (!niceName.StartsWith("@"))
                {
                    niceName = "@" + niceName;
                }

                buffer.Append(niceName).Append(" ").Append(resolvedType).Append(" = ").Append(PrepareValue(p));
            }

            return StringBuilderCache.ReturnAndFree(buffer
                .AppendLine()
                .AppendLine()
                .Append(timing.CommandString));
        }

        private static readonly string[] dontQuote = new string[] { "Int16", "Int32", "Int64", "Boolean" };
        private string PrepareValue(SqlTimingParameter p)
        {
            if (p.Value == null)
            {
                return "null";
            }

            if (dontQuote.Contains(p.DbType))
            {
                return p.Value;
            }

            var prefix = "";
            if (p.DbType == "String" || p.DbType == "StringFixedLength")
            {
                prefix = "N";
            }

            return prefix + "'" + p.Value.Replace("'", "''") + "'";
        }
    }
}

#endif
