#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Comm;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Processes all online requests.
    /// </summary>
    public interface IOnlineProcessor
    {
        /// <summary>
        /// Turns the data of the supplied Request object into the appropriate payload format
        /// and then passes it to the communication layer of the SDK.
        /// </summary>
        /// <remarks>
        /// The method will then block until the comm layer returns the server's response. The processRequest
        /// will convert the response payload into a Response object and return it to the client.
        ///
        /// The one task that is not so apparent is that it must verify that the response received belongs to
        /// the request that was sent. The converter will handle most of this by supplying a message format
        /// string for each payload, and then comparing them in the call to validateResponse.
        /// </remarks>
        /// <param name="request">The Request object created by the client.</param>
        /// <returns>A client-friendly object containing the data of the server's response.</returns>
        IResponse ProcessRequest(IRequest request);

        /// <summary>
        /// Sends a heartbeat request to the server.
        /// </summary>
        /// <remarks>
        /// This
        /// method validates the request parameter to make sure it is
        /// correct for the operation and then sends it to the appropriate
        /// OnlineProcessor.
        /// </remarks>
        /// <param name="request">A Request object that must be using the HeartBeat template.</param>
        /// <returns></returns>
        IResponse SendHeartbeat(IRequest request);

        CommArgs CompleteTransaction( byte[] payload, ConfigurationData configData, TransactionControlValues controlValues, SDKMetrics metrics );
    }
}
