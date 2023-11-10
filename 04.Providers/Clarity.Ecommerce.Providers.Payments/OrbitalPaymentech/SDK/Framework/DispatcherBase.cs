#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using JPMC.MSDK.Converter;

namespace JPMC.MSDK.Framework
{
    /// <exclude />
    /// <summary>
    /// Summary description for DispatcherBase.
    /// </summary>
    public abstract class DispatcherBase : FrameworkBase
    {
        /// <exclude />
        /// <summary>
        /// Flag stating if the Dispatcher was initialized.
        /// </summary>
        protected bool initialized = false;

        /// <exclude />
        /// <summary>
        /// Log and throw an exception if the specified object is null.
        /// </summary>
        /// <param name="testObject">Object to test.</param>
        /// <param name="error">The error code to set in the exception.</param>
        /// <param name="name">The name of the object.</param>
        protected void TestForNull(object testObject, Error error, string name)
        {
            if (testObject == null)
            {
                var msg = $"The {name} is null.";
                Logger.Error( msg );
                throw new DispatcherException( error, msg );
            }
        }

        protected void LogResponseAndPayload( ConverterArgs args )
        {
            var detailLogger = factory.DetailLogger;

            if ( !detailLogger.IsDebugEnabled )
            {
                return;
            }

            try
            {
                // The request payload that is sent to the server.
                detailLogger.Debug( "Logging Response XML:" );
                detailLogger.Debug( "[" + args.ResponseData.MaskedXML + "]" );

                // The response object given back to the client.
                detailLogger.Debug( "Logging Response fields:" );
                detailLogger.Debug( "[" + args.ResponseData.DumpMaskedFieldValues() + "]" );
            }
            catch ( Exception ex )
            {
                Logger.Warn( "Failed to log the masked response.", ex );
            }
        }

    }
}
