#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Threading;
using JPMC.MSDK.Common;
using log4net;

namespace JPMC.MSDK.Comm
{

    ///
    /// Title: TCP Connect Monitor
    ///
    /// Description: A singleton object of this class, is a thread that
    /// monitors the "TCPConnect" module and closes connections that are idle
    /// for a specfied time period
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///

    /// <summary>
    /// A singleton object of this class, is a thread that
    /// monitors the "TCPConnect" module and closes connections that are idle
    /// for a specfied time period
    /// </summary>
    public class TCPConnectMonitor
{
    //
    private static int closeIdleConnectionSecs;
    private static SafeDictionary<Thread,ClientInfo> clientList;
    private static ILog logger;
    private Thread monitorThread;

    /// <summary>
    /// Logger used for all logging
    /// </summary>
    public static ILog Logger
    {
        get
        {
            if (logger == null)
            {
                logger = new LoggingWrapper().EngineLogger;
            }
            return logger;
        }
        set => logger = value;
    }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="closeIdleConnectionSecsVal">number of seconds until we close a connection</param>
    /// <param name="clientListVal">list of clients</param>
    /// <param name="log"></param>
    public TCPConnectMonitor( int closeIdleConnectionSecsVal,
        SafeDictionary<Thread,ClientInfo> clientListVal, ILog log )
    {
        Logger = log;
        closeIdleConnectionSecs = closeIdleConnectionSecsVal;
        clientList = clientListVal;
        var entryPoint = new ThreadStart( Run );
        monitorThread = new Thread( entryPoint );
        monitorThread.IsBackground = true;
        monitorThread.Name = "TCPConnect Idle Socket Monitor";
        monitorThread.Start();
    }

    /// <summary>
    /// Return whether internal thread is active or not
    /// </summary>
    /// <returns>returns true if internal thread is active.</returns>
    public bool IsAlive()
    {
        var retVal = false;

        if ( monitorThread != null && monitorThread.IsAlive )
        {
            retVal = true;
        }
        return retVal;
    }

    /// <summary>
    /// close any connections that have been idle for
    /// "closeIdleMSecs milliseconds
    /// </summary>
    /// <param name="closeIdleMSecs"></param>
    /// <returns></returns>
    private long RemoveIdle( long closeIdleMSecs )
    {
        long itemAccessMSecs;
        long elapsedMSecs;
        long largestElapsedMSecs;

        // if we don't synchronize this code then we will get an
        // inconsistent list exception because other threads may
        // be adding or deleting from the list while we are.
        lock( clientList )
        {
            // save the victims (to be closed separately) in a separate
            // array list because otherwise we get an iterator
            // inconsistency exception
            var marked =
                new SafeDictionary<Thread,ClientInfo>();

            largestElapsedMSecs = 0;

            // check to see if this object is still in the client list
            foreach( var entry in clientList )
            {
                var cInfo = ( ClientInfo ) entry.Value;

                // when was this guy last used
                itemAccessMSecs = cInfo.LastAccessTime;

                // each "tick" is 100 microseconds so must divide by 10000
                // to get milliseconds
                elapsedMSecs = Utils.GetCurrentMilliseconds() - itemAccessMSecs;

                var remaining = closeIdleMSecs - elapsedMSecs;

                // if under 1 second, remove it
                if ( remaining < 1000 )
                {
                    marked.Add( entry.Key, entry.Value );
                }
                else
                {
                    // save it if it is the largest elapsed time
                    // because we only want to sleep the length
                    // of time for the oldest one to time out
                    if ( elapsedMSecs > largestElapsedMSecs )
                    {
                        largestElapsedMSecs = elapsedMSecs;
                    }
                }
            }

            // Now go through and close the ones that we found
            foreach ( var entry in marked )
            {
                try
                {
                    var key = ( Thread ) entry.Key;

                    var each = ( ClientInfo ) entry.Value;

                    clientList.Remove( key );

                    Logger.Debug( "TCPConnectMonitor closing connection with "
                            + each.Host );

                    each.Channel.CloseConnection();

                }
                catch( Exception e )
                {
                    Logger.Warn(
                        "While closing an idle socket got exception: "
                        + e.GetType().Name
                        + " with message: " + e.Message );

                }
            }
        }
        return largestElapsedMSecs;
    }

    /// <summary>
    /// main thread method
    /// </summary>
    private void Run()
    {
        long largestElapsedMSecs = 0;
        var closeIdleMSecs = ( long ) closeIdleConnectionSecs * ( long ) 1000;
        Logger.Debug("TCPConnectMonitor() starting");

        while( true )
        {

            try
            {
                var shortestWait = closeIdleMSecs - largestElapsedMSecs;

                if ( shortestWait <= 0 )
                {
                    shortestWait = closeIdleMSecs;
                }

                // we only want to sleep until the next expire time
                Thread.Sleep( (int ) shortestWait );
            }
            catch ( Exception e )
            {
                Logger.Warn( "While sleeping got exception: "
                        + e.GetType().Name
                        + " with message: " + e.Message );
            }

            if ( clientList.Count > 0 )
            {
                largestElapsedMSecs = RemoveIdle( closeIdleMSecs );
            }
            else
            {
                largestElapsedMSecs = 0;
            }
        }
    }
}
}
