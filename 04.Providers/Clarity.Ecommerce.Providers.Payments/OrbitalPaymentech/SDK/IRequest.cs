#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.InteropServices;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK
{
    /// <summary>
    /// Represents an individual request to send to the server, or an
    /// individual order to add to a batch submission file.
    /// </summary>
    /// <remarks>
    /// Access each field for the request type you specified by using
    /// the class' indexer. The available fields are based on the
    /// request type you specified when you created the IRequest object
    /// and are documented in the Developer's Guide.
    ///
    /// Example:
    ///    <code>
    ///    IRequest request = dispatcher.CreateRequest("NewTransaction");
    ///    request["Amount"] = "0300";
    ///    </code>
    /// </remarks>
    [Guid( "5935305A-6B5E-4658-B41C-FD261DE18312" )]
    [InterfaceType( ComInterfaceType.InterfaceIsIDispatch )]
    [ComVisible( true )]
    public interface IRequest
    {
        /// <summary>
        /// This indexer gives a convenient interface into the GetField and
        /// SetField methods.
        /// </summary>
        /// <remarks>
        /// This indexer only returns string values, so you will still need
        /// GetIntField and GetLongField (and their appropriate Set methods).
        /// </remarks>
        [DispId( 1 )]
        string this[string fieldName] { get; set; }

        /// <summary>
        /// Get the field and value - as 'BIN' and '0001'
        /// </summary>
        /// <param name="fieldName">The name of the field to get.</param>
        /// <returns>The specified field's value.</returns>
        [DispId( 2 )]
        string GetField( string fieldName );

        /// <summary>
        /// Get the field and value converted to an integer.
        /// </summary>
        /// <param name="fieldName">The name of the field to get.</param>
        /// <returns>The specified field's value.</returns>
        [DispId( 3 )]
        int GetIntField( string fieldName );

        /// <summary>
        /// Get the field and value converted to an long.
        /// </summary>
        /// <param name="fieldName">The name of the field to get.</param>
        /// <returns>The specified field's value.</returns>
        [DispId( 4 )]
        long GetLongField( string fieldName );

        /// <summary>
        /// Set the field and value into the fields map
        /// </summary>
        /// <param name="fieldName">The name of the field to set.</param>
        /// <param name="value">The field's value.</param>
        [DispId( 5 )]
        void SetField( string fieldName, string value );

        /// <summary>
        /// Set the field and value into the fields map
        /// </summary>
        /// <param name="fieldName">The name of the field to set.</param>
        /// <param name="value">The field's value.</param>
        [DispId( 6 )]
        void SetField( string fieldName, int value );

        /// <summary>
        /// Set the field and value into the fields map
        /// </summary>
        /// <param name="fieldName">The name of the field to set.</param>
        /// <param name="value">The field's value.</param>
        [DispId( 7 )]
        void SetField( string fieldName, long value );

        /// <summary>
        /// Gets the name of the transaction type
        /// </summary>
        [DispId( 8 )]
        string TransactionType { get; }

        /// <summary>
        /// Gets and sets the name of the host to send the transaction to.
        /// </summary>
        [DispId( 9 )]
        string Host { get; set; }

        /// <summary>
        /// Gets and sets the name of the port to send the transaction to.
        /// </summary>
        [DispId( 10 )]
        string Port { get; set; }

        /// <summary>
        /// Gets xml string of the request object
        /// with the user and default values set
        /// </summary>
        [DispId( 11 )]
        string XML { get; }

        /// <summary>
        /// Clear the value of the specified field.
        /// </summary>
        /// <param name="fieldName">The name of the field whose value is to be cleared.</param>
        [DispId( 13 )]
        void ClearField( string fieldName );

        /// <summary>
        /// Clear all the values of all fields.
        /// </summary>
        [DispId( 14 )]
        void ClearAllFields();

        /// <summary>
        /// Gets the ConfigurationData
        /// </summary>
        [DispId( 25 )]
        ConfigurationData Config { get; }

        /// <summary>
        /// Gets xml string of the request object
        /// with the user and default values set, and sensitive fields masked.
        /// </summary>
        [DispId( 29 )]
        string MaskedXML { get; }

        /// <summary>
        /// Set a field to the specified binary value.
        /// </summary>
        /// <param name="fieldName">The name of the field to set.</param>
        /// <param name="value">The binary data to set in the field.</param>
        [DispId( 30 )]
        void SetField( string fieldName, byte[] value );


        /// <summary>
        /// Gets the masked version of a field, or the actual value,
        /// if it's not supposed to be masked.
        /// </summary>
        /// <remarks>
        /// The field gets masked during conversion, which occurs
        /// when the request is being processed.
        /// So, this method will not return a masked value until
        /// after the transaction is complete.
        /// </remarks>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        [DispId( 33 )]
        string GetMaskedField( string fieldName );

        [DispId( 34 )]
        MessageFormat MessageFormat { get; }

        [DispId( 35 )]
        string DumpFieldValues( bool isMasked );

        [DispId( 36 )]
        string DumpFieldValues();

        [DispId( 37 )]
        string DumpMaskedFieldValues();

        [DispId( 38 )]
        void SetField( string fieldName, int arrayIndex, string value );
        [DispId( 39 )]
        TransactionControlValues TransactionControlValues { get; }
        [DispId( 40 )]
        void SetControlField( string name, string value );
        [DispId( 41 )]
        string DumpAllFieldValues();
        [DispId( 42 )]
        string GetField( string fieldName, int index );
    }
}
