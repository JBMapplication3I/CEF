#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.IO;
using JPMC.MSDK.Common;
using log4net;
using Renci.SshNet;

namespace JPMC.MSDK.Comm
{
    ///
    ///
    /// Title: SFTP Web Object Wrapper
    ///
    /// Description: Encapsulates use of JSch and SFTP
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 1.0
    ///


    /// <summary>
    ///
    /// </summary>
    public class SFTPWrapper : WrapperBase<SFTPWrapper>, IDisposable
{

    // Use common SDK logger by default, normally this will be reset
    private static ILog logger;

    private SftpClient sftpObj;


    /// <summary>
    /// This constructor needs to be public and parameterless to be
    /// compatible with WrapperBase generic base class
    /// </summary>
    public SFTPWrapper()
    {
    }

    public virtual void Initialize(string sftpHostName, int sftpHostPort, string ftpUserName,
        string keyFile, string keyFilePassword, List<byte[]> fingerprints, int timeout)
    {
        Initialize(sftpHostName, sftpHostPort, ftpUserName, string.Empty, 0, string.Empty, string.Empty,
            keyFile, keyFilePassword, fingerprints, timeout);
    }

    public virtual void Initialize(string sftpHostName, int sftpHostPort, string ftpUserName,
        string ftpProxyHost, int ftpProxyPort, string ftpProxyUser,
        string ftpProxyPassword, string keyFile, string keyFilePassword,
        List<byte[]> fingerprints, int timeout)
    {
        var farray = new PrivateKeyFile[1];

        farray[0] = new PrivateKeyFile(keyFile, keyFilePassword);

        var ptype = string.IsNullOrEmpty(ftpProxyHost) ?
            ProxyTypes.None : ProxyTypes.Http;

        var cinfo = new PrivateKeyConnectionInfo(sftpHostName,
            sftpHostPort, ftpUserName, ptype, ftpProxyHost, ftpProxyPort,
            ftpProxyUser, ftpProxyPassword, farray);

        cinfo.Timeout = new TimeSpan(timeout * TimeSpan.TicksPerSecond);

        sftpObj = new SftpClient(cinfo);

        sftpObj.HostKeyReceived += (sender, e) =>
        {
            var success = false;
            foreach (var expectedFingerPrint in fingerprints)
            {
                if (expectedFingerPrint.Length == e.FingerPrint.Length)
                {
                    var fail = false; // we only detect failure

                    for (var i = 0; i < expectedFingerPrint.Length; i++)
                    {
                        if (expectedFingerPrint[i] != e.FingerPrint[i])
                        {
                            fail = true; // we need to know if we broke out early because of non-matching byte
                            break;
                        }
                    }

                    if (!fail) // if we did not break out of the loop
                    {
                        success = true; // one match is all it takes
                        break;          // so get out, we are done
                    }
                }
            }
            if (!success)
            {
                e.CanTrust = false;
            }
        };
    }


    /// <summary>
    ///
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

    // Make all methods virtual so that they can easily be overridden by a stub class
    // for nunit testing
    /// <summary>
    ///
    /// </summary>
    /// <param name="timeOut"></param>
    /// <param name="protocol"></param>
    public virtual void Connect()
    {
        sftpObj.Connect();
    }

    public virtual bool IsConnected()
    {
        return sftpObj.IsConnected;
    }

    /// <summary>
    /// Disconnect from sftp server
    /// </summary>
    public virtual void Disconnect()
    {
        sftpObj.Disconnect();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="remoteDirName"></param>
    public virtual void cd(string remoteDirName)
    {
        sftpObj.ChangeDirectory(remoteDirName);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="srcFile"></param>
    /// <param name="sftpDestName"></param>
    public virtual void put(string srcFile, string sftpDestName)
    {

        Stream ostream = new FileStream(srcFile, FileMode.Open);

        try
        {
            sftpObj.UploadFile(ostream, sftpDestName);
        }
        finally
        {
            try { ostream.Close(); }
            catch (Exception) { }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="src"></param>
    /// <param name="destFile"></param>
    public virtual void get(string src, string destFile)
    {
        Stream stream = new FileStream(destFile, FileMode.Create);

        try
        {
            sftpObj.DownloadFile(src, stream, null);
        }
        finally
        {
            try { stream.Close(); }
            catch (Exception) { }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="remoteDirName"></param>
    /// <returns></returns>
    public virtual List<string> ls(string remoteDirName)
    {
        var retVal = new List<string>();

        var sshList = sftpObj.ListDirectory(remoteDirName);

        foreach (var item in sshList)
        {
            if (item.IsRegularFile)
            {
                retVal.Add(item.Name);
            }
        }

        return retVal;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="fileName"></param>
    public virtual void rm(string fileName)
    {
        sftpObj.DeleteFile(fileName);
    }

    // Implement IDisposable.
    // Do not make this method virtual.
    // A derived class should not be able to override this method.
    public void Dispose()
    {
        Dispose(true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SupressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!this.disposed)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                if (sftpObj != null)
                {
                    sftpObj.Dispose();
                }
            }

            // Note disposing has been done.
            disposed = true;

        }
    }
    private bool disposed;
}
}
