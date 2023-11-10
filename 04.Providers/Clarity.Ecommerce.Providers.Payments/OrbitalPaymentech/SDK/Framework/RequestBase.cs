#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;

namespace JPMC.MSDK.Framework
{
    public abstract class RequestBase : FrameworkBase
    {
        public string TransactionType { get; protected set; }
        public ConfigurationData Config { get; protected set; }
        protected OrbitalSchema schema = null;
        protected TransactionControlValues controlValues = new TransactionControlValues();
        public SDKMetrics Metrics { get; set; }

        // Virtual method
        public abstract string GetField( string fieldName );


        // Virtual method
        public abstract void SetField( string fieldName, string val );


        /**
         * Gets the default values from the DefaultValues.xml file
         * and sets them in their appropriate objects.
         *
         * @throws RequestException
         */
        public void SetDefaultValues()
        {
            try
            {
                var defaults = factory.Config.GetDefaultValues( Config );

                if ( defaults == null )
                {
                    return;
                }

                for ( var i = 0; i < defaults.Length; i++ )
                {
                    if ( defaults[ i ].destination.Equals( "ConfigurationData" ) )
                    {
                        if ( IsEmpty( Config[ defaults[ i ].name ] ) )
                        {
                            Config[ defaults[ i ].name ] = defaults[ i ].value;
                        }
                    }
                    else if ( defaults[ i ].destination.Equals( "TransactionControlValues" ) )
                    {
                        if ( IsEmpty( controlValues[ defaults[ i ].name ] ) )
                        {
                            controlValues[ defaults[ i ].name ] = defaults[ i ].value;
                        }
                    }
                    else if ( schema != null )
                    {
                        if ( schema.HasField( TransactionType, defaults[ i ].name ) )
                        {
                            if ( IsEmpty( GetField( defaults[ i ].name ) ) )
                            {
                                SetField( defaults[ i ].name, defaults[ i ].value );
                            }
                        }
                    }
                    else if ( IsEmpty( GetField( defaults[ i ].name ) ) )
                    {
                        SetField( defaults[ i ].name, defaults[ i ].value );
                    }
                }
            }
            catch ( DispatcherException ex )
            {
                Logger.Error( "Failed to set default values.", ex );
                throw new RequestException( Error.InitializationFailure, "Failed to set default values.", ex );
            }
        }

    }
}