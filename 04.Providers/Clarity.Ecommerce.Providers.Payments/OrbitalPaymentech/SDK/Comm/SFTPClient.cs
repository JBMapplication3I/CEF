#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using log4net;


namespace JPMC.MSDK.Comm
{
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// <summary>
    /// Summary description for SFTPClient.
    /// </summary>
    public class SFTPClient
{
    private SFTPWrapper sftpObj;
    private SFTPWrapper sftpObjFailover;
    private string remoteDirName;
    private string sftpHost;
    private string sftpFailoverHost;
    private ConfigurationData config;
    private static ILog logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    public SFTPClient( ConfigurationData cdata )
    {
        config = cdata;
        sftpObj = SFTPWrapper.GetInstance();
        sftpObjFailover = SFTPWrapper.GetInstance();
        sftpHost = cdata[ "Host" ];
        sftpFailoverHost = cdata["FailoverHost"];
        var timeout = cdata.GetInteger("ConnectTimeOutSecs", 10 );
        var port = cdata.GetInteger( "Port", 22 );
        var failoverPort = cdata.GetInteger( "FailoverPort", 22 );


        if (string.IsNullOrEmpty(cdata[ "ProxyHost" ] ) )
        {
            sftpObj.Initialize( sftpHost, port, cdata[ "UserName" ],
               cdata[ "RSAMerchantPrivateKey" ], cdata[ "RSAMerchantPassPhrase" ], GetFingerPrints(),
               timeout);

            sftpObjFailover.Initialize( sftpFailoverHost, failoverPort, cdata[ "UserName" ],
               cdata[ "RSAMerchantPrivateKey" ], cdata[ "RSAMerchantPassPhrase" ], GetFingerPrints(),
               timeout);
        }
        else
        {
            var proxyuser = cdata.GetString( "ProxyUserName", "" );
            var proxypass = cdata.GetString( "ProxyPassword", "" );
            var proxyport = cdata.GetInteger( "ProxyPort", 8443 );

            sftpObj.Initialize( sftpHost, port, cdata["UserName"],
                 cdata["ProxyHost"], proxyport, proxyuser, proxypass, cdata["RSAMerchantPrivateKey"],
                 cdata["RSAMerchantPassPhrase"], GetFingerPrints(), timeout);

            sftpObjFailover.Initialize( sftpFailoverHost, failoverPort, cdata["UserName"],
               cdata["ProxyHost"], proxyport, proxyuser, proxypass, cdata["RSAMerchantPrivateKey"],
               cdata["RSAMerchantPassPhrase"], GetFingerPrints(), timeout);
        }
    }

    private List<byte[]> GetFingerPrints()
    {
        var primary = config["PrimaryFingerPrint"];
        var secondary = config[ "SecondaryFingerPrint" ];
        var retVal = new List<byte[]>();

        retVal.Add(GetFingerPrint(primary));
        retVal.Add(GetFingerPrint(secondary));

        return retVal;
    }

    private byte[] GetFingerPrint(string fingerp)
    {
        var parray = fingerp.Split(':');

        var retVal = new byte[parray.Length];

        for (var i = 0; i < parray.Length; i++)
        {
            if (!string.IsNullOrEmpty(parray[i]))
            {
                retVal[i] = byte.Parse(parray[i], NumberStyles.HexNumber);
            }
            else
            {
                retVal[i] = 0;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Sftp remote directory
    /// </summary>
    public string RemoteDir
    {
        set
        {
            if (value != null)
            {
                remoteDirName = value;
            }
        }
    }

    public string Connect()
    {
        ConnectImpl(sftpObj);
        return sftpHost;
    }

    public string ConnectFailover()
    {
        ConnectImpl(sftpObjFailover);
        return sftpFailoverHost;
    }

    /// <summary>
    /// Connect to Sftp server
    /// </summary>
    private void ConnectImpl(SFTPWrapper target)
    {
        try
        {

            Logger.Debug("Connecting to the SFTP server....");

            target.Connect();

            Logger.Debug("Connected to the SFTP server... ");

            // change to remote directory name for further operations

            try
            {
                target.cd(remoteDirName);
            }
            catch (Exception e)
            {
                Logger.Info("Exception while accessing the SFTP directory [" +
                remoteDirName + "] in [" + sftpHost + "]");
                throw new CommException(
                    Error.UnknownSFTPDirectroy, e.ToString());
            }
        }
        // just for unknown directory
        catch (CommException)
        {
            Disconnect();
            throw;
        }
        catch (Exception e)
        {
            Disconnect();
            throw new CommException(Error.ConnectFailure, e.ToString());
        }
    }

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
    /// Place the file to the sftp server
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="remoteFileName"></param>
    public void PutFile(string fileName, string remoteFileName)
    {
        try
        {
            if (sftpObj.IsConnected())
            {
                sftpObj.put(fileName, remoteFileName);
            }
            else if (sftpObjFailover.IsConnected())
            {
                sftpObjFailover.put(fileName, remoteFileName);
            }
            else
            {
                throw new CommException(Error.BatchNotOpen, "No connection to sftp servers available");
            }
        }
        catch (Exception e)
        {
            Disconnect();
            throw new CommException(Error.SFTPWriteError,
            "Exception while writing the file to the SFTP server, source file name ["
            + fileName + "] remote file name [" + remoteFileName + "] in ["
            + sftpHost + "]" + e);
        }
    }
    /// <summary>
    /// Get a file from SFTP server
    /// </summary>
    /// <param name="remoteFileName"></param>
    /// <param name="localFileName"></param>
    public void GetFile(string remoteFileName, string localFileName)
    {
        try
        {
            if ( sftpObj.IsConnected() )
            {
                 sftpObj.get(remoteFileName, localFileName);
            }
            else if ( sftpObjFailover.IsConnected() )
            {
                 sftpObjFailover.get(remoteFileName, localFileName);
            }
            else
            {
                throw new CommException(Error.BatchNotOpen, "No connection to sftp servers available");
            }
        }
        catch (Exception e)
        {
            Disconnect();
            throw new CommException(Error.SFTPReadError,
            "Exception while reading file from the SFTP server, source file name ["
            + localFileName + "] remote file name [" + remoteFileName + "] in ["
            + sftpHost + "]" + e);
        }
    }

    /// <summary>
    /// Get the lastest file from SFTP server for the given pattern
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public string GetFileName(string pattern)
    {
        string fileName = null;
        List<string> objectVect = null;
        try
        {
            // Get the file listing
            if ( sftpObj.IsConnected() )
            {
                objectVect = sftpObj.ls(remoteDirName);
            }
            else if ( sftpObjFailover.IsConnected() )
            {
                objectVect = sftpObjFailover.ls(remoteDirName);
            }
            else
            {
                throw new CommException(Error.BatchNotOpen, "No connection to sftp servers available");
            }

            // get each from listing
            Logger.Debug("Available files in SFTP server");
            foreach (var entry in objectVect)
            {
                Logger.Debug("SFTP file name [" + entry + "]");

                var mt = Regex.Match(entry, pattern);

                if (mt.Success)
                {
                    fileName = entry;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Disconnect();
            throw new CommException(Error.SFTPReadError,
                "Exception while doing ls in SFTP server, file pattern ["
                + pattern + "] in [" + sftpHost + "]" + e);
        }

        return fileName;
    }

    /// <summary>
    /// Delete the named file from the sftp server
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public bool DeleteFile(string fileName)
    {
        return DeleteFileImpl(sftpObj, fileName);
    }
    public bool DeleteFileFailover(string fileName)
    {
        return DeleteFileImpl(sftpObjFailover, fileName);
    }
    public bool DeleteFileImpl(SFTPWrapper sftp, string fileName)
    {
        try
        {
            sftp.rm(fileName);
            return true;
        }
        catch (Exception e)
        {
            Logger.Info("Exception while deleting the file from SFTP server, file name ["
            + fileName + "] in [" + sftpHost + "]" + e);

            return false;
        }
    }

    private void DeleteAll(string pattern)
    {
        var target = GetFileName(pattern);
        while (target != null)
        {
            //Console.WriteLine(target);
            DeleteFileImpl(sftpObj, target);
            target = GetFileName(pattern);
        }
    }

    /// <summary>
    /// Disconnect from sftp server
    /// </summary>
    public void Disconnect()
    {
        sftpObj.Disconnect();
        sftpObjFailover.Disconnect();
    }
}
}

