#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.InteropServices;

namespace JPMC.MSDK
{
    /// <summary>
    /// Represents the contents of the response file returned from the Chase
    /// server for a batch submission.
    /// </summary>
    /// <remarks>
    /// This class is only used for batch processing.
    ///
    /// The entire contents of the file are not stored
    /// in memory in the class, but are read as needed from the file.
    /// </remarks>6
    [ComVisible( true )]
    public interface IResponseDescriptor
    {
        /// <summary>
        /// The type of response file this descriptor refers to.
        /// </summary>
        string ResponseFileType { get; }

        /// <summary>
        /// Gets the path to the batch file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the name of the submission from the header.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the header record from the file and returns it as a Response object.
        /// </summary>
        IResponse Header { get; }

        /// <summary>
        /// Gets the trailer record from the file and returns it as a Response object.
        /// </summary>
        IResponse Trailer { get; }

        /// <summary>
        /// Gets the total record from the file and returns it as a Response object.
        /// </summary>
        IResponse Totals { get; }

        /// <summary>
        /// Gets the total record for the batch from the file and returns it as a Response object.
        /// </summary>
        IResponse BatchTotals { get; }

        /// <summary>
        /// Resets the reader back to the beginning of the file.
        /// </summary>
        void ResetReader();

        /// <summary>
        /// Returns true if there are more orders in the file to read, and false if there are no more.
        /// </summary>
        [Obsolete("No longer supported. Use HasNext property from now on.", false)]
        bool HasNextOrder { get; }

        /// <summary>
        /// Returns true if there are more orders or records in the file to read, and false if there are no more.
        /// </summary>
        bool HasNext { get; }

        /// <summary>
        /// Returns true if there are more orders in the file before the
        /// current position, false if there are none.
        /// </summary>
        [Obsolete("No longer supported. Use HasNext property from now on.", false)]
        bool HasPreviousOrder { get; }

        /// <summary>
        /// Returns the next order from the file sequentially.
        /// </summary>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        [Obsolete( "No longer supported. Call GetNext() from now on.", false )]
        IResponse GetNextOrder();

        /// <summary>
        /// Returns the next order from the file sequentially.
        /// </summary>
        /// <remarks>
        /// This returns a response from either a batch response file or a delimited report file.
        /// There are no longer two separate methods for getting records from each.
        /// </remarks>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        IResponse GetNext();

        /// <summary>
        /// Gets a line of text from a variable length file, parses its fields
        /// into a Response object and returns it to the user.
        /// </summary>
        /// <remarks>
        /// Use <see cref="JPMC.MSDK.IResponseDescriptor.HasNextRecord">
        /// HasNextRecord</see> to see if there is another record in the file.
        /// <br/>
        /// You can use <see cref="JPMC.MSDK.IResponseDescriptor.GetNextRecord">
        /// GetNextRecord</see> to get an unparsed version of the record.
        /// </remarks>
        /// <returns>A Response object that gives easy access to the record's fields.</returns>
        [Obsolete( "No longer supported. Call GetNext() from now on.", false )]
        IResponse GetNextParsedRecord();

        /// <summary>
        /// Returns the last order before the current position in
        /// the file.
        /// </summary>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        [Obsolete("No longer supported. Iterating backward is no longer supported.", false)]
        IResponse GetPreviousOrder();

        /// <summary>
        /// Returns the last order in the file.
        /// </summary>
        /// <remarks>
        /// This method simply seeks to the end of the file and then
        /// calls GetPreviousOrder.
        /// </remarks>
        /// <returns>A response file that represents the last order in the file.</returns>
        IResponse GetLastOrder();

        /// <summary>
        /// Returns the next order from the file sequentially as a Request
        /// object. This is to make it easy to recover a corrupted
        /// Submission file.
        /// </summary>
        /// <remarks>
        /// Use this in conjunction with <see cref="JPMC.MSDK.Dispatcher.OpenDescriptor"/>
        /// to recover a corrupted Submission file. This will get the next order from the file, but
        /// will return it as an IRequest object that you can then add to a new
        /// <see cref="JPMC.MSDK.ISubmissionDescriptor"/>.
        /// </remarks>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        IRequest GetNextRequest();

        /// <summary>
        /// Returns the name of the response file as it resides on the server.
        /// </summary>
        /// <remarks>
        /// The SFTP filename is used by the merchant when calling the
        /// Dispatcher's deleteServerFile method. The CommManager will set
        /// this field using setSFTPFilename, and the Dispatcher will wrap the
        /// call to this method for the merchant to use.
        /// </remarks>
        string SFTPFilename { get; }

        /// <summary>
        /// Closes the response file.
        /// </summary>
        void Close();

        /// <summary>
        /// Reads a single record from the response file.
        /// </summary>
        /// <remarks>
        /// The record returned is in the format that the Chase Paymentech
        /// server can read. This method is typically never used, and is
        /// only useful if you need to extract the actual payload of the
        /// batch exactly as it was processed on the server.
        /// </remarks>
        /// <returns>A string represent the current record.</returns>
        string GetNextRecord();

        /// <summary>
        /// Returns true if there is another record that can be retrieved from
        /// the response file, false if there are none.
        /// </summary>
        /// <remarks>
        /// This is to be used in conjunction with <see cref="GetNextRecord"/>.
        /// These methods are typically never used. They are only made
        /// available for special cases.
        /// </remarks>
        [Obsolete("No longer supported. Use HasNext property from now on.", false)]
        bool HasNextRecord { get; }

        /// <summary>
        /// Gets the NetConnect error response data, if one exists.
        /// </summary>
        /// <remarks>Returns null if no error exists.</remarks>
        IResponse ErrorResponse { get; }

        Configurator.ConfigurationData ConfigData { get;  }
    }
}
