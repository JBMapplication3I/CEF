// Decompiled with JetBrains decompiler
// Type: Excel.Errors
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel
{
    /// <summary>An errors.</summary>
    internal static class Errors
    {
        /// <summary>Size of the error biff buffer.</summary>
        public const string ErrorBIFFBufferSize = "BIFF Stream error: Buffer size is less than entry length.";

        /// <summary>The error biff ilegal after.</summary>
        public const string ErrorBIFFIlegalAfter = "BIFF Stream error: Moving after stream end.";

        /// <summary>The error biff ilegal before.</summary>
        public const string ErrorBIFFIlegalBefore = "BIFF Stream error: Moving before stream start.";

        /// <summary>Size of the error biff record.</summary>
        public const string ErrorBIFFRecordSize = "Error: Buffer size is less than minimum BIFF record size.";

        /// <summary>Array of error directory entries.</summary>
        public const string ErrorDirectoryEntryArray = "Directory Entry error: Array is too small.";

        /// <summary>The error fat bad sector.</summary>
        public const string ErrorFATBadSector = "Error reading as FAT table : There's no such sector in FAT.";

        /// <summary>The error fat read.</summary>
        public const string ErrorFATRead = "Error reading stream from FAT area.";

        /// <summary>The error header order.</summary>
        public const string ErrorHeaderOrder = "Error: Invalid byte order specified in header.";

        /// <summary>The error header signature.</summary>
        public const string ErrorHeaderSignature = "Error: Invalid file signature.";

        /// <summary>The error stream workbook not found.</summary>
        public const string ErrorStreamWorkbookNotFound =
            "Error: Neither stream 'Workbook' nor 'Book' was found in file.";

        /// <summary>Information describing the error workbook globals invalid.</summary>
        public const string ErrorWorkbookGlobalsInvalidData =
            "Error reading Workbook Globals - Stream has invalid data.";

        /// <summary>The error workbook is not stream.</summary>
        public const string ErrorWorkbookIsNotStream = "Error: Workbook directory entry is not a Stream.";
    }
}
