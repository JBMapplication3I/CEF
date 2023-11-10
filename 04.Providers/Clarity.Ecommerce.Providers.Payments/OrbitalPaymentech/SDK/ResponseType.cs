#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.InteropServices;

namespace JPMC.MSDK
{
    /// <summary>
    /// Lists the valid types of response files that can be retrieved
    /// from the server by MSDK.
    /// </summary>
    [Guid("34AD6B09-C819-42eb-9543-B46482ED3976")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("JPMC.MSDK.ResponseType")]
    [ComVisible( true )]
    public class ResponseType
    {
        /// <summary>
        /// Represents a standard response file.
        /// </summary>
        public const string Response = "Response";

        /// <summary>
        /// Represents a HBC BIN file
        /// </summary>
        public const string HBCBIN = "HBCBIN";

        /// <summary>
        /// Represents a ChaseNet BIN file.
        /// </summary>
        public const string CHNBIN = "CHNBIN";

        /// <summary>
        ///  Represents a delimited HBC BIN file.
        /// </summary>
        public const string DelimitedHBCBIN = "HBCBINDelim";

        /// <summary>
        /// Represents a delimited ChaseNet BIN file.
        /// </summary>
        public const string DelimitedCHNBIN = "CHNBINDelim";

        /// <summary>
        /// Represents a US Mastercard acccount updater.
        /// </summary>
        public const string USMCAccountUpdater = "USMCAccountUpdater";

        /// <summary>
        /// Represents a US Visa acccount updater.
        /// </summary>
        public const string USVIAccountUpdater = "USVIAccountUpdater";

        /// <summary>
        /// Represents a EU Mastercard acccount updater.
        /// </summary>
        public const string EUMCAccountUpdater = "EUMCAccountUpdater";

        /// <summary>
        /// Represents a EU Visa acccount updater.
        /// </summary>
        public const string EUVIAccountUpdater = "EUVIAccountUpdater";

        /// <summary>
        /// Represents a US Discover acccount updater.
        /// </summary>
        public const string USDIAccountUpdater = "USDIAccountUpdater";

        /// <summary>
        /// Represents a delimited report file.
        /// </summary>
        public const string DelimitedFileReport = "DelimitedFileReport";

        /// <summary>
        /// Represents a Pinless Debit BIN file.
        /// </summary>
        public const string PinlessDebitBIN = "PinlessDebitBIN";

        /// <summary>
        /// Represents a Debit BIN.
        /// </summary>
        public const string PinDebitBIN = "PinDebitBIN";

        /// <summary>
        /// Represents an error response file.
        /// These files are retrieved using
        /// <see cref="JPMC.MSDK.Dispatcher.ReceiveNetConnectError()"/>.
        /// </summary>
        internal const string Error = "Error";

        /// <summary>
        /// Represents an no data response from Frame/VPN communication.
        /// </summary>
        internal const string NoData = "NoData";

        public const string CommercialBIN = "CommercialBIN";
    }
}
