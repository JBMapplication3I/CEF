#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Threading;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Filer;
namespace JPMC.MSDK.Comm
{

    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Rameshkumar Bhaskharan
    /// @version 1.0
    ///
    /// <summary>
    /// Summary description for SFTPBatch.
    /// </summary>
    public class SFTPBatch : CommBase, ICommModule
    {
        private string failoverHost;
        private int failoverPort = 22;
        private string remoteDirectory;
        private string fileExtension;
        private bool delFromAll = true;
        private string host;
        private int port = 22;
        private int connectFailRetries;
        private int connectLoopWaitMSecs;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="Configurator"></param>
        public SFTPBatch( ConfigurationData cdata )  : base( cdata )
        {
            failoverHost = Config[ "FailoverHost" ];
            failoverPort = Config.GetInteger( "FailoverPort", failoverPort );
            remoteDirectory = Config[ "RemoteDirectory" ];
            fileExtension = Config[ "FileExtension"];
            delFromAll = Config.GetBool( "DeleteFromAll", delFromAll );
            host = Config.GetString( "Host", null );
            port = Config.GetInteger( "Port", 0 );
            connectFailRetries = Config.GetInteger("ConnectFailRetries", 3 );
            connectLoopWaitMSecs = Config.GetInteger("ConnectLoopWaitMSecs", 1000 );
            SFTPClient.Logger = Logger;
            SFTPWrapper.Logger = Logger;
        }
        /// <summary>
        /// Send the PGP file to the SFTP server
        /// </summary>
        /// <param name="ofile"></param>
        private void SendFile( IFileReader ofile )
        {
            var sftpClient = new SFTPClient( Config );

            string sftpPid=null;
            //sftpPid = ofile.ProtocolManager.PID;
            sftpPid = Config["PID"];
            if ( sftpPid == null || ofile.FilePath == null )
            {
                throw new CommException(Error.SFTPMissingAttr,"Exception while sending file  to SFTP server - Missing SFTP PID or Submission file name or null value");
            }
            //Construct the SFTP unique file name
            string remoteFileName =  null;
            if ( ! ofile.IsSFTPFileNameSet )
            {
                remoteFileName = Utils.GetUniqueFileName( fileExtension, sftpPid, ofile.FileName);
            }
            else
            {
                remoteFileName = ofile.FileName;
                if ( ! remoteFileName.StartsWith(sftpPid + ".") || ! remoteFileName.EndsWith("." +  fileExtension ) || remoteFileName.Length > 31 )
                {
                    throw new CommException(Error.SFTPMissingAttr,"Exception while sending file to SFTP server - The given SFTP file name is not a valid file name [" + remoteFileName + "]" );
                }
            }
            if (string.IsNullOrEmpty(remoteFileName) )
            {
                throw new CommException(Error.SFTPMissingAttr,"Exception while sending file to SFTP server - Error while getting a unique SFTP file name for PID[" + sftpPid + "] SubmissionName[" +  ofile.FileName + "]" );
            }

            EstablishConnection(sftpClient, false, false );

            sftpClient.PutFile(ofile.FilePath,remoteFileName );
            sftpClient.Disconnect();
            ofile.SFTPFilename = remoteFileName;
            Logger.Debug(" Successfully send sftp file [" + remoteFileName + "] for processing");
        }
        /// <summary>
        /// Receive the file from the SFTP server
        /// </summary>
        /// <param name="ifile"></param>
        /// <param name="reqMessage"></param>
        /// <returns></returns>
        private int ReceiveFile( IFileWriter ifile, byte [] reqMessage )
        {
            var sftpClient = new SFTPClient( Config );


            // get the response type
            var mode = ifile.ResponseType;
            string fileName = null;
            var filePattern = Config[ mode + "Pattern"];
            if (string.IsNullOrEmpty(filePattern) )
            {
                throw new CommException(Error.SFTPMissingAttr,"Exception while getting the response file pattern");
            }

            EstablishConnection(sftpClient, false, false);

            fileName =  sftpClient.GetFileName(filePattern);
            if (string.IsNullOrEmpty(fileName) )
            {
                Logger.Debug(" None available for  [" + filePattern + "] in SFTP server");
                sftpClient.Disconnect();
                return -1;
            }
            sftpClient.GetFile(fileName,ifile.FileName);
            ifile.SFTPFilename = fileName;
            sftpClient.Disconnect();
            Logger.Debug( " Successfully received sftp file [" + fileName + "]" );
            return (int) ifile.Size;

        }
        /// <summary>
        /// Delete a nmed file from the server
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool  DeleteFromServer( string fileName)
        {
            var sftpClient = new SFTPClient( Config );
            var priFileName = "default";
            var secFileName = "default";
            var excep=false;
            //try deleting from each server
            try
            {
                EstablishConnection(sftpClient, false, true );
                sftpClient.DeleteFile(fileName);
                // check to see the file is still there
                priFileName = sftpClient.GetFileName(fileName);
            }
            catch(CommException)
            {
                excep = true;
            }
            if ( sftpClient != null )
            {
                sftpClient.Disconnect();
            }
            try
            {
                EstablishConnection(sftpClient, true, true);

                if ( excep || delFromAll )
                {
                    sftpClient.DeleteFileFailover(fileName);
                }
                secFileName = sftpClient.GetFileName(fileName);

            }
            catch(CommException)
            {
            }
            if ( sftpClient != null )
            {
                sftpClient.Disconnect();
            }
            // files are not available
            if (priFileName == null && secFileName == null )
            {
                return true;
            }
            if ( !delFromAll && ( priFileName == null || secFileName == null ) )
            {
                return true;
            }
            Logger.Debug(" Failed to delete the sftp file [" + fileName + "]");
            return false;
        }
    /// <summary>
    /// Make a SFTP connection
    /// </summary>
    /// <param name="sftpClient"></param>
    /// <param name="secManager"></param>
    /// <param name="serverName"></param>
    /// <param name="port"></param>
        private void EstablishConnection(SFTPClient sftpClient, bool useFailover, bool noFailover )
        {
            // Get all sftp  security data
            var userName = Config[ "UserName" ];
            var priFingerPrint =  Config[ "PrimaryFingerPrint" ];
            var secFingerPrint = Config[ "SecondaryFingerPrint" ];
            var rsaPrivateKey = Config[ "RSAMerchantPrivateKey" ];
            string serverName = null;
            if ( rsaPrivateKey == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing RSAMerchantPrivateKey");
            }
            var rsaPPhrase = Config[ "RSAMerchantPassPhrase" ];
            var connected = false;
            // check for missing critical values
            if ( userName == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing UserName");
            }
            if ( priFingerPrint == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing PrimaryFingerprint");
            }
            if ( priFingerPrint.Length < 47 )
            {
                Logger.Debug("Invalid Primary finger print provided[" +  priFingerPrint + "]");
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Invalid PrimaryFingerprint length [" + priFingerPrint.Length + "]");
            }
            if ( secFingerPrint == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing SecondaryFingerprint");
            }
            if ( secFingerPrint.Length < 47 )
            {
                Logger.Debug("Invalid Secondary finger print provided[" +  secFingerPrint + "]");
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Invalid SecondaryFingerprint length [" + secFingerPrint.Length + "]");
            }

            if ( rsaPrivateKey == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing RSAMerchantPrivateKey");
            }
            if ( failoverHost == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing failoverhost");
            }
            if ( remoteDirectory == null )
            {
                throw new CommException(Error.InitializationFailure,"Exception while connecting to SFTP server - Missing remotedirectory");
            }

            var attempts = 0;
            var isInFailOverMode = false;
            sftpClient.RemoteDir = remoteDirectory;

            while ( !connected )
            {
                try
                {
                    // attempt to connect
                    if ( isInFailOverMode || useFailover )
                    {
                        serverName = sftpClient.ConnectFailover();
                    }
                    else
                    {
                        serverName = sftpClient.Connect();
                    }
                    connected = true;
                }
                catch(CommException e)
                {
                    if (e.ErrorCode.Equals(Error.UnknownSFTPDirectroy))
                    {
                        throw new CommException( Error.UnknownSFTPDirectroy,"Exception while connecting to SFTP server - unknown SFTP RemoteDirectory  ");
                    }
                    attempts ++ ;
                    if ( attempts > connectFailRetries && ! noFailover && ! isInFailOverMode)
                    {
                        // try failover
                        attempts = 0 ;
                        isInFailOverMode = true;
                        Logger.Warn("Failed to connect to the SFTP host [" + host + "] with the following exception, connecting to [" + failoverHost + "]");
                        Logger.Warn(e.ToString());
                    }
                    // tried all attempts - throw exception
                    if ( isInFailOverMode && attempts > connectFailRetries || noFailover && attempts > connectFailRetries)
                    {
                        if ( noFailover )
                        {
                            Logger.Warn("Failed to connect to the SFTP host [" + serverName + "]");
                        }
                        else
                        {
                            Logger.Warn("Failed to connect to the SFTP host [" + host + "] & failover host  [" + failoverHost + "]");
                        }
                        throw new CommException( Error.ConnectFailure,"Exception while connecting to SFTP server " + e);
                    }

                    //sleep before next attempts
                    try
                    {
                        Thread.Sleep( connectLoopWaitMSecs );
                    }
                    catch ( Exception  )
                    {
                    }
                }
            }
        }

        public CommArgs CompleteTransaction( CommArgs args )
        {
            CommArgs retVal = null;

            if ( args.FileReader != null )
            {
                SendFile( args.FileReader );
            }

            if ( args.FileWriter != null )
            {
                if ( ReceiveFile( args.FileWriter, args.Data ) > 0 )
                {
                    retVal = args;
                }
            }

            if ( args.FileName != null )
            {
                if ( DeleteFromServer( args.FileName ) )
                {
                    retVal = args;
                }
            }

            return retVal;
        }

        public void Close()
        {
        }

        protected override string ModuleName => "SFTPBatch";
    }
}
