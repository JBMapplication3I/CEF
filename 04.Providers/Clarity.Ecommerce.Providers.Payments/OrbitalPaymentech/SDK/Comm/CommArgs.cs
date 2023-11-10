#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Net;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Filer;
using JPMC.MSDK.Framework;


namespace JPMC.MSDK.Comm
{
    //
    // Title: Arguments to Communications Layer
    //
    // Description: Arguements to be passed to comm modules
    //
    // Copyright (c)2018, Paymentech, LLC. All rights reserved
    //
    // Company: J. P. Morgan
    //
    // @author Frank McCanna
    // @version 3.0
    /// <summary>
    /// This class contains data that is passed to the CommManager which then
    /// passes it on to a comm module
    /// </summary>
    public class CommArgs
{
    /**
     * Base constructor, called by all the other constructors
     * @param rdr - when sending a file this is a reader of the file to send
     * @param wtr - when receiving a file, this is a writer to the file saved locally
     * @param fname - when deleting a file, this is the name of the file to delete
     * @param pro - protocol manager
     * @param msg - the data either arriving or going out
     * @param cdata - configuration data
     */
    public CommArgs( IFileReader rdr, IFileWriter wtr, string fname,
        byte[] msg, ConfigurationData cdata,
        TransactionControlValues ctrlValues,
        WebHeaderCollection mHeaders )
    {
        reader = rdr;
        writer = wtr;
        fileName = fname;
        data = msg;
        configData = cdata;
        controlValues = ctrlValues;
        mimeHeaders = mHeaders;
    }

    /**
     * Constructor used for sending a batch file
     * @param srcFile
     * @param pro
     * @param cdata
     */
    public CommArgs( IFileReader srcFile, ConfigurationData cdata ) :
        this( srcFile, null, null, null, cdata, null, null )
    {
    }


    /**
     * Constructor used for receiving a batch file or report
     * @param destFile
     * @param pro
     * @param rfrdata - this is the RFR data
     * @param cdata
     */
    public CommArgs( IFileWriter destFile,
        byte[] rfrdata, ConfigurationData cdata ) :
        this( null, destFile, null, rfrdata, cdata, null, null )
    {
    }

    /**
     * Constructor used when deleting a file on the remote server
     * @param fname
     * @param pro
     * @param cdata
     */
    public CommArgs( string fname,
        ConfigurationData cdata ) :
        this( null, null, fname, null, cdata, null, null )
    {
    }

    /**
     * Constructor for online transaction
     * @param msg
     * @param pro
     * @param cdata
     */
    public CommArgs( byte[] msg,
        ConfigurationData cdata, TransactionControlValues controlValues ) :
        this( null, null, null, msg, cdata, controlValues, null )
    {
    }

    /**
     * Constructor for response
     * @param msg
     */
    public CommArgs( byte[] response ) :
        this( null, null, null, response, null, null, null )
    {
    }

    /**
     * Constructor for response
     * @param msg
     */
    public CommArgs( byte[] response, WebHeaderCollection mimeHeaders ) :
        this( null, null, null, response, null, null, mimeHeaders )
    {
    }

    /// <summary>
    /// raw data that we are either sending or have received
    /// </summary>
    private byte[] data;

    /// <summary>
    /// Special pieces of info that we need to send a message
    /// </summary>
    private ConfigurationData configData;

    /// <summary>
    /// Special pieces of info that we need to send a message
    /// </summary>
    private TransactionControlValues controlValues;

    /// <summary>
    /// Various pieces of info to allow us to send a message
    /// </summary>
    public ConfigurationData Config
    {
        get => configData;
        set => configData = value;
    }

    /// <summary>
    /// Various pieces of info to allow us to send a message
    /// </summary>
    public TransactionControlValues ControlValues
    {
        get => controlValues;
        set => controlValues = value;
    }

    /// <summary>
    /// property to access the raw data to be sent or has been received
    /// </summary>
    public byte[] Data
    {
        get => data;
        set => data = value;
    }

    /// <summary>
    /// Length of data in the data buffer
    /// </summary>
    /// <returns></returns>
    public int GetDataLength()
    {
        var retVal = 0;

        if ( data != null )
        {
            retVal = data.Length;
        }
        return retVal;
    }

    // file to write to for incoming batch response or report
    private IFileWriter writer;
    public IFileWriter FileWriter => writer;

    // file to read from when sending a batch
    private IFileReader reader;
    public IFileReader FileReader => reader;

    // name of the file on the remote host that needs to be deleted
    private string fileName;
    public string FileName => fileName;

    // in case merchant needs it, we return the mime headers
    private WebHeaderCollection mimeHeaders;
    public WebHeaderCollection MimeHeaders => mimeHeaders;
}
}

