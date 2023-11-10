// Decompiled with JetBrains decompiler
// Type: Excel.Core.Helpers
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>A helpers.</summary>
    internal static class Helpers
    {
        /// <summary>The re.</summary>
        private static readonly Regex re = new("_x([0-9A-F]{4,4})_");

        /// <summary>Adds a column handle duplicate to 'columnName'.</summary>
        /// <param name="table">     The table.</param>
        /// <param name="columnName">Name of the column.</param>
        public static void AddColumnHandleDuplicate(DataTable table, string columnName)
        {
            var str = columnName;
            var column = table.Columns[columnName];
            var num = 1;
            while (column != null)
            {
                str = string.Format("{0}_{1}", columnName, num);
                column = table.Columns[str];
                ++num;
            }
            table.Columns.Add(str, typeof(object));
        }

        /// <summary>Convert escape characters.</summary>
        /// <param name="input">The input.</param>
        /// <returns>The escape converted characters.</returns>
        public static string ConvertEscapeChars(string input)
        {
            return re.Replace(input, m => ((char)uint.Parse(m.Groups[1].Value, NumberStyles.HexNumber)).ToString());
        }

        /// <summary>Initializes this Helpers from the given convert from oa time.</summary>
        /// <param name="value">The value.</param>
        /// <returns>from converted oa time.</returns>
        public static object ConvertFromOATime(double value)
        {
            if (value >= 0.0 && value < 60.0)
            {
                ++value;
            }
            return DateTime.FromOADate(value);
        }

        /// <summary>Int 64 bits to double.</summary>
        /// <param name="value">The value.</param>
        /// <returns>A double.</returns>
        public static double Int64BitsToDouble(long value)
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(value), 0);
        }

        /// <summary>Query if 'encoding' is single byte encoding.</summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns>True if single byte encoding, false if not.</returns>
        public static bool IsSingleByteEncoding(Encoding encoding)
        {
            return encoding.IsSingleByte;
        }

        /// <summary>Fix data types.</summary>
        /// <param name="dataset">The dataset.</param>
        internal static void FixDataTypes(DataSet dataset)
        {
            var dataTableList = new List<DataTable>(dataset.Tables.Count);
            var flag = false;
            foreach (DataTable table in dataset.Tables)
            {
                if (table.Rows.Count == 0)
                {
                    dataTableList.Add(table);
                }
                else
                {
                    DataTable dataTable = null;
                    for (var index = 0; index < table.Columns.Count; ++index)
                    {
                        Type type1 = null;
                        foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                        {
                            if (!row.IsNull(index))
                            {
                                var type2 = row[index].GetType();
                                if (type2 != type1)
                                {
                                    if (type1 == null)
                                    {
                                        type1 = type2;
                                    }
                                    else
                                    {
                                        type1 = null;
                                        break;
                                    }
                                }
                            }
                        }
                        if (type1 != null)
                        {
                            flag = true;
                            if (dataTable == null)
                            {
                                dataTable = table.Clone();
                            }
                            dataTable.Columns[index].DataType = type1;
                        }
                    }
                    if (dataTable != null)
                    {
                        dataTable.BeginLoadData();
                        foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
                        {
                            dataTable.ImportRow(row);
                        }
                        dataTable.EndLoadData();
                        dataTableList.Add(dataTable);
                    }
                    else
                    {
                        dataTableList.Add(table);
                    }
                }
            }
            if (!flag)
            {
                return;
            }
            dataset.Tables.Clear();
            dataset.Tables.AddRange(dataTableList.ToArray());
        }
    }
}
