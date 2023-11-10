#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using log4net;

namespace JPMC.MSDK.Comm
{
    ///
    ///
    ///
    /// Title: Communication Module Base Class
    ///
    /// Description: All protocol modules extend this class
    ///
    /// Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///

    /// <summary>
    /// All protocol modules extend this class
    /// </summary>
    public abstract class CommBase
{
    protected CommBase(ConfigurationData cdata)
    {
        config = new ConfigurationData(cdata);
    }

    // only used for logging
    protected abstract string ModuleName { get; }

    private ConfigurationData config;
    public ConfigurationData Config => config;

    // Use common SDK logger.
    private static ILog logger;

    public static ILog Logger
    {
        get
        {
            if ( logger == null )
            {
                logger = new LoggingWrapper().EngineLogger;
            }
            return logger;
        }
        set => logger = value;
    }
}
}
