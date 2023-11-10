// Decompiled with JetBrains decompiler
// Type: Excel.ExcelReaderFactory
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel
{
    using System.IO;

    /// <summary>An excel reader factory.</summary>
    public static class ExcelReaderFactory
    {
        /// <summary>Creates binary reader.</summary>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The new binary reader.</returns>
        public static IExcelDataReader CreateBinaryReader(Stream fileStream)
        {
            IExcelDataReader excelDataReader = new ExcelBinaryReader();
            excelDataReader.Initialize(fileStream);
            return excelDataReader;
        }

        /// <summary>Creates binary reader.</summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="option">    The option.</param>
        /// <returns>The new binary reader.</returns>
        public static IExcelDataReader CreateBinaryReader(Stream fileStream, ReadOption option)
        {
            IExcelDataReader excelDataReader = new ExcelBinaryReader(option);
            excelDataReader.Initialize(fileStream);
            return excelDataReader;
        }

        /// <summary>Creates binary reader.</summary>
        /// <param name="fileStream">   The file stream.</param>
        /// <param name="convertOADate">True to convert oa date.</param>
        /// <returns>The new binary reader.</returns>
        public static IExcelDataReader CreateBinaryReader(Stream fileStream, bool convertOADate)
        {
            var binaryReader = CreateBinaryReader(fileStream);
            ((ExcelBinaryReader)binaryReader).ConvertOaDate = convertOADate;
            return binaryReader;
        }

        /// <summary>Creates binary reader.</summary>
        /// <param name="fileStream">   The file stream.</param>
        /// <param name="convertOADate">True to convert oa date.</param>
        /// <param name="readOption">   The read option.</param>
        /// <returns>The new binary reader.</returns>
        public static IExcelDataReader CreateBinaryReader(Stream fileStream, bool convertOADate, ReadOption readOption)
        {
            var binaryReader = CreateBinaryReader(fileStream, readOption);
            ((ExcelBinaryReader)binaryReader).ConvertOaDate = convertOADate;
            return binaryReader;
        }

        /// <summary>Creates open XML reader.</summary>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>The new open XML reader.</returns>
        public static IExcelDataReader CreateOpenXmlReader(Stream fileStream)
        {
            IExcelDataReader excelDataReader = new ExcelOpenXmlReader();
            excelDataReader.Initialize(fileStream);
            return excelDataReader;
        }
    }
}
