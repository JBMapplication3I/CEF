// Decompiled with JetBrains decompiler
// Type: Excel.IExcelDataReader
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel
{
    using System;
    using System.Data;
    using System.IO;

    /// <summary>Interface for excel data reader.</summary>
    public interface IExcelDataReader : IDataReader, IDisposable, IDataRecord
    {
        /// <summary>Gets a message describing the exception.</summary>
        /// <value>A message describing the exception.</value>
        string ExceptionMessage { get; }

        /// <summary>Gets or sets a value indicating whether this IExcelDataReader is first row as column names.</summary>
        /// <value>True if this IExcelDataReader is first row as column names, false if not.</value>
        bool IsFirstRowAsColumnNames { get; set; }

        /// <summary>Gets a value indicating whether this IExcelDataReader is valid.</summary>
        /// <value>True if this IExcelDataReader is valid, false if not.</value>
        bool IsValid { get; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>Gets the number of results.</summary>
        /// <value>The number of results.</value>
        int ResultsCount { get; }

        /// <summary>Converts this IExcelDataReader to a data set.</summary>
        /// <returns>A DataSet.</returns>
        DataSet AsDataSet();

        /// <summary>Converts a convertOADateTime to a data set.</summary>
        /// <param name="convertOADateTime">True to convert oa date time.</param>
        /// <returns>A DataSet.</returns>
        DataSet AsDataSet(bool convertOADateTime);

        /// <summary>Initializes this IExcelDataReader.</summary>
        /// <param name="fileStream">The file stream.</param>
        void Initialize(Stream fileStream);
    }
}
