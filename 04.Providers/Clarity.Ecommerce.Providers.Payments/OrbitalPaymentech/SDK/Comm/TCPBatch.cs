#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Comm
{
    using System;
    using System.Net.Sockets;
    using System.Threading;
    using JPMC.MSDK.Common;
    using JPMC.MSDK.Configurator;
    using JPMC.MSDK.Filer;

    ///
    ///
    /// Title: TCP Batch Payment Processing Module
    ///
    /// Description: This module provides communication methods for transmitting
    /// payment processing transactions via TCP Batch specification
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///

    /// <summary>
    /// This module provides communication methods for transmitting
    /// payment processing transactions via TCP Batch specification
    /// </summary>
    public sealed class TCPBatch : TCPBase
    {
        protected override int MaxConnectionsDefault => 5;

        protected override int CloseLingerSecsDefault => 60;

        protected override bool VerifyHostDefault => true;

        protected override int ConnectLoopWaitMSecsDefault => 1000;

        protected override int SocketTimeoutSecsDefault => 0;

        protected override int SocketReceiveBufferSizeDefault => 1024;

        protected override int SocketSendBufferSizeDefault => 484;

        protected override int ConnectTimeoutSecsDefault => 60;

        protected override int ReadLoopWaitMSecsDefault => 100;

        protected override int ReadTimeoutSecsDefault => 60;

        protected override int WriteLoopWaitMSecsDefault => 1000;

        protected override int WriteTimeoutSecsDefault => 60;

        protected override bool BlockingConnectDefault => true;

        protected override bool SocketTcpNoDelayDefault => true;

        protected override bool SendCloseOnNextReadDefault => false;

        protected override bool BlockingReadDefault => false;

        protected override bool BlockingWriteDefault => true;

        /// <summary>
        /// Constructor, only used by CommManager
        /// </summary>
        /// <param name="cdata"></param>
        public TCPBatch( ConfigurationData cdata )
            : base( cdata )
        {
            ReadBufferSize = cdata.GetInteger("ReadBufferSize", 1024 );
            FileSendDelaySecs = cdata.GetInteger("FileSendDelaySecs", 10 );
            FileReadDebugTriggerMSecs = cdata.GetInteger("FileReadDebugTriggerMSecs", 1000);
            FileWriteDebugTriggerMSecs = cdata.GetInteger("FileWriteDebugTriggerMSecs", 1000);
            CommReadDebugTriggerMSecs = cdata.GetInteger("CommReadDebugTriggerMSecs", 1000);
            CommWriteDebugTriggerMSecs = cdata.GetInteger("CommWriteDebugTriggerMSecs", 1000);
            if ( CloseLingerSecs < 1 )
            {
                Logger.Warn("The <CloseLingerSecs> element in TCPBatchConfig.xml must be set to"
                    + " a number greater than zero so that connections to the batch "
                    + "server are closed properly");
            }

        }

        /// <summary>
        /// Value can be overridden in the TCPBatchConfig.xml file
        /// </summary>
        public int ReadBufferSize { get; private set; }

        public int FileSendDelaySecs { get; private set; }

        /// <summary>
        /// File reading lasting longer than milliseconds generates debug messages
        /// </summary>
        public int FileReadDebugTriggerMSecs { get; private set; }

        /// <summary>
        /// File writing lasting longer than milliseconds generates debug messages
        /// </summary>
        public int FileWriteDebugTriggerMSecs { get; private set; }

        /// <summary>
        /// The following are debug log thresholds use to determine
        /// when to log I/O performance problems
        /// </summary>
        public int CommWriteDebugTriggerMSecs { get; private set; }

        /// <summary>
        /// same as above but for reading
        /// </summary>
        public int CommReadDebugTriggerMSecs { get; private set; }

        /// <summary>
        /// dummy interface implementation - used by sftp RKB
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool  DeleteFromServer( ConfigurationData sec, string fileName )
        {
            return true;
        }

        /// <summary>
        /// Send a batch file to server
        /// </summary>
        /// <param name="ofile">file to send</param>
        private void SendFile( IFileReader ofile )
        {
            ClientInfo cInfo = null;

            // This try is only so we can do the "finally"
            try
            {
                try
                {
                    cInfo = DoConnect();
                }
                catch ( Exception ex )
                {
                    if ( ex is CommException )
                    {
                        var cex = ( CommException ) ex;
                        throw new CommException( cex.ErrorCode,
                            cex.Message + " while connecting to batch server",
                            ex.InnerException );
                    }

                    throw new CommException( Error.ConnectFailure,
                        "Failed to connect to batch server", ex );
                }

                byte[] inbuf = null;
                ByteBuffer outbuf = null;
                long startFileReadTime = 0;
                long startCommWriteTime = 0;
                long duration = 0;
                var firstTime = true;

                if ( cInfo != null )
                {
                    // Sleep here a few seconds so that the server has time to
                    // accept the connection and close it.  This will cause our
                    // write to the socket to fail so we will know if we are
                    // being rejected.
                    if ( FileSendDelaySecs > 0 )
                    {
                        Logger.Debug( "Waiting " + FileSendDelaySecs +
                            " seconds for server to recognize the connection . . ." );
                        try
                        {
                            Thread.Sleep( FileSendDelaySecs * 1000 );
                        }
                        catch ( Exception e )
                        {
                            // TODO Auto-generated catch block
                            Logger.Debug( e.StackTrace );
                        }
                        Logger.Debug( "Now sending the file." );
                    }

                    // copy each record from a file to the connection with
                    // server
                    while ( ofile.HasNextRecord )
                    {
                        if ( Logger.IsDebugEnabled )
                        {
                            startFileReadTime =
                                Utils.GetCurrentMilliseconds();
                        }

                        // get the next file record
                        // let this methods exceptions go to caller
                        inbuf = ofile.GetNextRecord();

                        if ( Logger.IsDebugEnabled )
                        {
                            duration = Utils.GetCurrentMilliseconds() -
                                startFileReadTime;

                            if ( duration >
                                FileReadDebugTriggerMSecs )
                            {
                                Logger.Debug(
                                    "Unusually long file record " +
                                    "access of " + duration +
                                    " milliseconds" );
                            }
                        }

                        if ( inbuf == null || inbuf.Length == 0 )
                        {
                            break;
                        }

                        outbuf = ByteBuffer.Wrap( inbuf );

                        //Console.WriteLine( "Sending bytes: " +
                        //	Utils.ByteArrayToString( inbuf ) );
                        if ( Logger.IsDebugEnabled )
                        {
                            startCommWriteTime =
                                Utils.GetCurrentMilliseconds();
                        }

                        try
                        {
                            // copy it to server
                            // send everything all at once for efficiency
                            CommUtils.WriteMinimum( cInfo.Channel, outbuf,
                                WriteTimeoutSecs, WriteLoopWaitMSecs );

                            if ( firstTime )
                            {
                                firstTime = false;
                            }
                        }
                        catch ( Exception ex )
                        {
                            if ( firstTime )
                            {
                                FirstTimeFailure( ( int ) outbuf.Position, cInfo.LocalHost );
                            }

                            if ( ex is CommException )
                            {
                                var cex = ( CommException ) ex;
                                throw new CommException( cex.ErrorCode,
                                    cex.Message + " after writing " + outbuf.Position
                                    + " bytes", cex.InnerException );
                            }

                            throw new CommException( Error.WriteFailure,
                                "Write failure after writing " + outbuf.Position +
                                " bytes", ex );
                        }

                        if ( Logger.IsDebugEnabled )
                        {
                            duration =
                                Utils.GetCurrentMilliseconds() -
                                startCommWriteTime;

                            if ( duration >
                                CommWriteDebugTriggerMSecs )
                            {
                                Logger.Debug(
                                    "Unusually long TCP " +
                                    "write of " + duration +
                                    " milliseconds for connection: "
                                    + cInfo.ConnStr() );
                            }
                        }
                    }

                    outbuf = ByteBuffer.Wrap( ofile.FileTerminator );

                    try
                    {
                        // copy it to server
                        CommUtils.WriteMinimum( cInfo.Channel, outbuf,
                            WriteTimeoutSecs, WriteLoopWaitMSecs );
                    }
                    catch ( Exception ex )
                    {
                        if ( ex is CommException )
                        {
                            throw;
                        }

                        throw new CommException(
                            Error.WriteFailure,
                            "Failure while writing batch file trailer to network", ex );
                    }
                }
            }
            finally
            {
                ofile.Close();

                if ( cInfo != null )
                {
                     cInfo.Channel.CloseConnection( WriteTimeoutSecs, CloseLingerSecs );
                }
            }
        }

        /// <summary>Special exception message when it fails on the first write.</summary>
        /// <param name="numbytes"> The numbytes.</param>
        /// <param name="localhost">The localhost.</param>
        private void FirstTimeFailure( int numbytes, string localhost )
        {
            var msg = "There was a failure while writing " +
                numbytes + " bytes.  The server may have rejected the " +
                "connection,\n make sure IP address: " +
                localhost + " is registered with the server and all identity data is correct.";

            throw new CommException( Error.WriteFailure, msg );
        }

        /// <summary>
        /// Send the RFR
        /// </summary>
        /// <param name="reqMessage"></param>
        /// <returns></returns>
        private ClientInfo SendRFR( byte [] reqMessage )
        {
            ClientInfo cInfo = null;

            try
            {
                cInfo = DoConnect();
            }
            catch ( Exception ex )
            {
                if ( ex is CommException )
                {
                    var cex = ( CommException ) ex;
                    throw new CommException( cex.ErrorCode,
                        cex.Message + " while connecting to batch server",
                        ex.InnerException );
                }

                throw new CommException( Error.ConnectFailure,
                    "Failed to connect to batch server", ex );
            }

            // buffer up the RFR
            var outbuf = ByteBuffer.Allocate( reqMessage.Length );

            outbuf.Put( reqMessage );
            outbuf.Flip();

            // send the RFR
            try
            {
                CommUtils.WriteMinimum( cInfo.Channel, outbuf,
                    WriteTimeoutSecs, WriteLoopWaitMSecs );
            }
            catch ( Exception ex )
            {
                if ( ex is CommException )
                {
                    throw;
                }
                else
                {
                    throw new CommException( Error.WriteFailure,
                        "Error encountered writing RFR to network", ex );
                }
            }

            return cInfo;
        }

        /// <summary>
        /// Receive a batch response file
        /// </summary>
        /// <param name="ifile">where to put data read</param>
        /// <param name="reqMessage">RFR, sent in order to get response</param>
        /// <returns>number of records read.</returns>
        private int ReceiveFile( IFileWriter ifile, byte [] reqMessage )
        {
            var readCount = 0;
            ClientInfo cInfo = null;
            ByteBuffer inbuf = null;
            var numBytes = 0;
            var totalBytes = 0;
            var nl = Environment.NewLine;

            // this try is only for the "finally"
            try
            {
                cInfo = SendRFR( reqMessage );

                // first time through we use a smaller buffer
                // because the interface needs to determine
                // the file type so it can watch for the EOF
                inbuf = new ByteBuffer( ifile.MinBytesToRead );

                var channel = cInfo.Channel;

                var startTime = Utils.GetCurrentMilliseconds();

                long startFileWriteTime = 0;
                long startCommReadTime = 0;
                long duration = 0;
                var doSpecialEx = false;

                if ( Encrypt )
                {
                    channel.SSLStream.ReadTimeout = ReadTimeoutSecs * 1000;
                }

                while ( !ifile.EOF )
                {
                    if ( Logger.IsDebugEnabled )
                    {
                        startCommReadTime =
                            Utils.GetCurrentMilliseconds();
                    }

                    try
                    {
                        numBytes = CommUtils.ReadBuffer( channel,
                            inbuf );

                    }
                    catch ( Exception ex )
                    {
                        // make sure we are always throwing a CommException for
                        // comm code that fails
                        if ( ex is CommException )
                        {
                            var cex = ( CommException ) ex;

                            // check if it was an SSL socket close, if so,
                            // just break out so special error code prevails
                            if ( readCount == 0 &&
                                cex.ErrorCode == Error.EndOfFile )
                            {
                                doSpecialEx = true;
                            }
                            else
                            {
                                throw new CommException( cex.ErrorCode,
                                    cex.Message + " after reading " + totalBytes
                                    + " bytes", cex.InnerException );
                            }
                        }
                        else
                        {
                            throw new CommException(
                                Error.ReadFailure,
                                "Error reading batch response from network" +
                                " after reading " + totalBytes + " bytes", ex );
                        }
                    }

                    if ( Logger.IsDebugEnabled )
                    {
                        duration = Utils.GetCurrentMilliseconds() -
                            startCommReadTime;

                        if ( duration >
                            CommReadDebugTriggerMSecs )
                        {
                            Logger.Debug(
                                "Unusually long network read of " +
                                + duration +
                                " milliseconds for connection: " + cInfo.ConnStr() );
                        }
                    }

                    if ( numBytes > 0 )
                    {
                        totalBytes += numBytes;

                        // if we got some bytes then reset the timer
                        // timeout should only occur while we are not
                        // receiving any bytes
                        startTime = Utils.GetCurrentMilliseconds();

                        readCount++;

                        var sbuf = inbuf.ExtractBytes();

                        if ( Logger.IsDebugEnabled )
                        {
                            startFileWriteTime =
                                Utils.GetCurrentMilliseconds();

                            // only dump data if <LogRawData> is set to true in TCPBaseConfig.xml
                            if (LogRawData == true)
                            {
                                // convert each byte to a 2 digit hex value
                                var hbuf = Utils.ByteArrayToHex(sbuf).GetBytes();

                                // create byte array with just one blank in it
                                var addBytes = new byte[1];
                                addBytes[0] = ( byte ) ' ';

                                // use our method for inserting bytes at repeated locations in an array of bytes
                                var fbuf = Utils.InsertBytes(hbuf, 2, addBytes);

                                Logger.Debug(nl + "Raw Data:" + nl + Utils.ByteArrayToString( fbuf ) + nl);
                            }
                        }

                        // just let it's exceptions throw to caller
                        ifile.WriteRecord( inbuf.ExtractBytes() );

                        if ( Logger.IsDebugEnabled )
                        {
                            duration =
                                Utils.GetCurrentMilliseconds() -
                                startFileWriteTime;

                            if ( duration >
                                FileWriteDebugTriggerMSecs )
                            {
                                Logger.Debug(
                                    "Unusually long write to file of "
                                    + duration + " milliseconds" );
                            }
                        }

                        // we wrote what was in the old buffer so
                        // we need a new one
                        var len = ifile.MinBytesToRead - totalBytes;

                        if ( len > 0 )
                        {
                            inbuf = new ByteBuffer( len );
                        }
                        else
                        {
                            inbuf = new ByteBuffer( ReadBufferSize );
                        }
                    }
                    else // numBytes <= 0
                    {
                        // check to see if connection was rejected
                        if ( doSpecialEx ||
                            readCount == 0 &&
                            channel.Available == 0 &&
                            channel.Poll( 0, SelectMode.SelectRead ) )
                        {
                            var ip = cInfo.LocalHost;

                            if ( ip == null )
                            {
                                ip = "<local machine ip address>";
                            }

                            var msg = "Failed to read any data from server. " +
                                "The server may have rejected the " +
                                "connection,\n make sure IP address: " +
                                ip + " is registered with the server and all identity data is correct.";

                            throw new CommException( Error.ReadFailure, msg, null );

                        }

                        CheckTimeout( startTime );
                    }
                }
            }
            finally
            {
                // we have to do these closes even when we throw an
                // exception
                ifile.Close();

                if ( cInfo != null )
                {
                    cInfo.Channel.CloseConnection(
                        WriteTimeoutSecs, CloseLingerSecs );
                }
            }

            // No matter what happens, if we didn't throw an exception but
            // there is no end-of-file, we have to indicate a problem
            if ( ! ifile.EOF  )
            {
                throw new CommException(
                    Error.ResponseFailure,
                    "End of File not reached" );
            }

            //Console.WriteLine( "Got " + totalBytes + " bytes in " + readCount + " packets" );
            return readCount;
        }

        /// <summary>
        /// Throw an exception if there is a timeout
        /// </summary>
        /// <param name="startTime"></param>
        private void CheckTimeout( long startTime )
        {
            long timeOut = ReadTimeoutSecs * 1000;

            // if we have a timeout
            if ( timeOut > 0 )
            {
                // check to see if we timed out
                var elapsedTime = Utils.GetCurrentMilliseconds() -
                    startTime;

                var remainingTime = timeOut - elapsedTime;

                if ( remainingTime < 0 )
                {
                    throw new CommException( Error.
                        ReadTimeoutFailure,
                        "read timeout" );
                }
            }
        }

        protected override CommArgs CompleteTransactionImpl(CommArgs args)
        {
            CommArgs retVal = null;

            if (args.FileReader != null)
            {
                SendFile(args.FileReader);
            }

            if (args.FileWriter != null)
            {
                if (ReceiveFile(args.FileWriter, args.Data) > 0)
                {
                    retVal = args;
                }
            }

            if (args.FileName != null)
            {
                if (DeleteFromServer(args.Config, args.FileName))
                {
                    retVal = args;
                }
            }

            return retVal;
        }

        public override void Close()
        {
        }

        protected override void SendMessage(ClientInfo cInfo, byte[] data, int timeout, int loopwait) { }

        protected override byte[] RecvMessage(ClientInfo cInfo, int timeout, int loopwait) { return null; }

        protected override string ModuleName => "TCPBatch";
    }
}
