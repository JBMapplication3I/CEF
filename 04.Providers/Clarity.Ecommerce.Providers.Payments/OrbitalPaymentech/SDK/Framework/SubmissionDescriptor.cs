#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.IO;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Filer;
using log4net;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Provides the API for creating and manipulating batch submission files.
    /// </summary>
    /// <remarks>
    /// This class wraps the encrypted submission files and provides the
    /// methods and properties necessary to create submissions and to add
    /// orders them.
    ///
    /// This class provides write-only access to the batch file. The
    /// <see cref="JPMC.MSDK.IResponseDescriptor">
    /// ResponseDescriptor</see> is used to read submission and response files.
    /// </remarks>
    public class SubmissionDescriptor : FrameworkBase, ISubmissionDescriptor
    {
        private static object submissionLock = new object();
        private IFileWriter writer;
        private bool batchClosed;
        private Amounts amounts = new Amounts();
        private ILog detailLogger;
        public SDKMetrics Metrics { get; set; }

        private class Amounts
        {
            public long totals;
            public long orders;
            public long records;
            public long refunds;
            public long sales;

            public Amounts()
            {
            }
        }

        /// <exclude />
        /// <summary>
        /// Constructs a new SubmissionDescriptor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="configData"></param>
        public SubmissionDescriptor( string name, string password, ConfigurationData configData )
            : this( name, password, configData, new DispatcherFactory() )
        {
        }

        /// <exclude />
        /// <summary>
        /// Constructs a new SubmissionDescriptor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="configData"></param>
        /// <param name="factory"></param>
        public SubmissionDescriptor( string name, string password, ConfigurationData configData, IDispatcherFactory factory )
        {
            this.Password = password;
            this.factory = factory;
            this.Config = configData;
            var path = Utils.FormatDirectoryPath( name );
            this.detailLogger = factory.DetailLogger;
            TestForNull( name, Error.NullFileName, "submission name" );

            if ( Utils.IsAbsolutePath( path ) )
            {
                this.FileName = path;
                Name = JPMC.MSDK.Common.FileName.GetName( path );
            }
            else
            {
                Name = path.Trim();
            }

            this.writer = factory.MakeFileWriter( configData );

            Metrics = new SDKMetrics( SDKMetrics.Service.Dispatcher, SDKMetrics.ServiceFormat.API );
            Metrics.PointOfEntryMetric = SDKMetrics.PointOfEntry.Stratus;
            Metrics.MessageOriginMetric = SDKMetrics.MessageOrigin.Local;
            Metrics.SetCommMode( configData.Protocol );

        }

        /// <summary>
        /// Gets or sets the name that the submission file will have when
        /// it's sent to the payment processing server.
        /// </summary>
        /// <remarks>
        /// When you receive an error response (by calling
        /// <see cref="JPMC.MSDK.IDispatcher.ReceiveNetConnectError()"/>
        /// ), the error response will specify the filename of the submission
        /// that has the error. But the filename it gives is for the
        /// file that is on the server. You can use this property to match
        /// the errored file to the appropriate submission.
        /// </remarks>
        public string SFTPFileName { get; set; }

        /// <summary>
        /// Gets and sets the ConfigurationData to be used by the submission.
        /// </summary>
        public ConfigurationData Config { get; private set; }

        /// <summary>
        /// Opens a batch file for reading.
        /// </summary>
        public void OpenBatch()
        {
            FileName = this.GetFileName( Name, FileName, Config[ "OutgoingBatchDirectory" ] );
            if ( FileName == null )
            {
                FileName = GetFileName( Name, FileName, Config[ "IncomingBatchDirectory" ] );
                if ( FileName == null )
                {
                    var msg = $"Cannot find the submission file \"{Name}\".";
                    Logger.Error( msg );
                    throw new SubmissionException( Error.FileNotFound, msg );
                }
            }

            string headerRecord = null;
            var reader = factory.MakeFileReader( this.FileName, Password, null );
            OrderSeparator sep = null;
            try
            {
                var bytes = reader.GetNextRecord();
                headerRecord = Utils.ByteArrayToString( bytes );
                sep = reader.OrderSeparator;
            }
            catch ( FilerException ex )
            {
                var msg = "Failed to open the submission. Make sure your password is correct.";
                throw new SubmissionException( ex.ErrorCode, msg, ex );
            }
            catch ( Exception ex )
            {
                var msg = "Failed to open the submission. Make sure your password is correct.";
                Logger.Error( msg );
                throw new SubmissionException( Error.InvalidOperation, msg, ex );
            }
            finally
            {
                reader.Close();
            }


            IResponse headerResp = null;
            try
            {
                var converter = factory.MakeBatchConverter( Config );
                var args = converter.ConvertResponse( headerRecord, false );
                headerResp = args.ResponseData;
            }
            catch ( ConverterException ex )
            {
                throw new SubmissionException( ex.ErrorCode, "Failed to read the header record.", ex );
            }

            writer.OpenFile( FileName, Password );
            var fileHeader = writer.FileHeader;
            batchClosed = fileHeader.BatchClosed;
            writer.SeekToEnd();

            Header = factory.MakeRequest( "SubmissionHeader", Config );
            Header[ "PID" ] = headerResp[ "PID" ];
            Header[ "PIDPassword" ] = headerResp[ "PIDPassword" ];
            Header[ "SID" ] = headerResp[ "SID" ];
            Header[ "SIDPassword" ] = headerResp[ "SIDPassword" ];
            factory.RequestToImpl( Header ).SetField( "FileName", headerResp[ "FileName" ], true );

            this.amounts.orders = sep.OrderCount;
            this.amounts.records = sep.RecordCount;
            this.amounts.refunds = sep.TotalRefunds;
            this.amounts.sales = sep.TotalSales;
            this.amounts.totals = sep.Totals;

            Logger.InfoFormat( "Batch file {0} is now open for sending.", FileName );
        }


        #region ISubmissionDescriptor Members

        /// <summary>
        /// Gets the header record for the submission.
        /// </summary>
        public IRequest Header { get; private set; }

        /// <summary>
        /// Returns true if the submission's batch has been closed, otherwise
        /// it returns false.
        /// </summary>
        /// <remarks>
        /// This method does not test if the file itself is open. Instead it
        /// only tests if the batch has been closed and the submission is
        /// reading for sending. Use the
        /// <see cref="JPMC.MSDK.ISubmissionDescriptor.Open">
        /// Open</see> property to tell if the file is open.
        /// </remarks>
        public bool BatchClosed
        {
            get
            {
                lock ( submissionLock )
                {
                    return batchClosed;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the submission is still open.
        /// </summary>
        /// <remarks>
        /// A submission is "Closed" when its totals records and trailer record
        /// have been written to it. Once that has happened, no more records
        /// are supposed to be written to the batch.
        ///
        /// The submission is marked as Closed when the
        /// <see cref="JPMC.MSDK.ISubmissionDescriptor.CloseBatch">
        /// CloseBatch</see> method is called.
        /// </remarks>
        /// <value>True if CloseBatch was called on this submission, false
        /// if it hasn't.</value>
        public bool Open
        {
            get
            {
                lock ( submissionLock )
                {
                    return writer.Open;
                }
            }
        }

        /// <summary>
        /// Retuns the absolute path to the submission file that this class wraps.
        /// </summary>
        /// <value>The absolute path to the submission file.</value>
        public string FileName { get; private set; }

        /// <summary>
        /// The password used to encrypt the records when they are written to the
        /// file.
        /// </summary>
        /// <value>A string representing the encryption password.</value>
        public string Password { get; private set; }

        /// <summary>
        /// A symbolic name for the submission.
        /// </summary>
        /// <remarks>
        /// This name is specified by the client when creating the submission,
        /// and is stored in the submission's header record. It will also be
        /// in the header record of the response file that belongs to this
        /// submission. This allows the client to compare the submission name
        /// to the response's name in order to match responses to submissions.
        /// The client can compare this property to the
        /// <see cref="JPMC.MSDK.IResponseDescriptor.Name">
        /// IResponseDescriptor.Name</see> property to do the matching.
        ///
        /// The value of this property is limited to eight characters in
        /// length. Names can be shorter than this limit, but cannot be
        /// longer.
        /// </remarks>
        /// <value>An 8-character or less string that uniquely identifies this submission.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Add a new Request to the submission. It will be stored in the file
        /// in the order that it's added.
        /// </summary>
        /// <param name="request">The request to add to the batch.</param>
        public void Add( IRequest request )
        {
            lock ( submissionLock )
            {
                try
                {
                    Add( request, false );
                }
                catch ( SubmissionException )
                {
                    throw;
                }
                catch ( Exception ex )
                {
                    Logger.Error( ex.Message, ex );
                    throw new SubmissionException( Error.AddToSubmissionFailed, ex.Message, ex );
                }
            }
        }

        /// <summary>
        /// Returns the number of order records in the submission.
        /// </summary>
        /// <value>The number of orders added to the submission file.</value>
        public long OrderCount => amounts.orders;

        /// <summary>
        /// Writes the trailer records and marks the Submission as closed.
        /// </summary>
        /// <remarks>
        /// At this point, no more Requests can be added to it. Also, the
        /// trailer will be built at this time. There will be some fields in the trailer that contain a sum
        /// of a specific field in the various Order Requests that have been added to the Submission. The
        /// client has the option of calculating these sums himself and setting them in his Trailer Request
        /// object. If he chose not to set them, this method will set them for him.
        /// </remarks>
        public void CloseBatch()
        {
            if ( !writer.Open && !this.BatchClosed )
            {
                var message = "The batch file is not open. You must call CreateBatch first.";
                Logger.Error( message );
                throw new SubmissionException( Error.BatchNotOpen, message );
            }

            if ( writer.FileHeader != null && writer.FileHeader.BatchClosed
                || this.BatchClosed )
            {
                writer.Close();
                return;
            }

            lock ( submissionLock )
            {
                var template = factory.Config.SpecialTemplateNames.BatchTotal;
                var batchTotals = factory.MakeRequest( template, Config );
                batchTotals.SetField( "BatchRecordCount", amounts.records );
                batchTotals.SetField( "BatchOrderCount", amounts.orders );
                batchTotals.SetField( "BatchAmountTotal", amounts.totals );
                batchTotals.SetField( "BatchAmountSales", amounts.sales );
                batchTotals.SetField( "BatchAmountRefunds", amounts.refunds );
                Add( batchTotals, true );

                template = factory.Config.SpecialTemplateNames.Totals;
                var totals = factory.MakeRequest( template, Config );
                totals.SetField( "FileRecordCount", amounts.records );
                totals.SetField( "FileOrderCount", amounts.orders );
                totals.SetField( "FileAmountTotal", amounts.totals );
                totals.SetField( "FileAmountSales", amounts.sales );
                totals.SetField( "FileAmountRefunds", amounts.refunds );
                Add( totals, true );

                template = factory.Config.SpecialTemplateNames.Trailer;
                var trailer = factory.MakeRequest( template, Config );
                trailer[ "PID" ] = Header[ "PID" ];
                trailer[ "PIDPassword" ] = Header[ "PIDPassword" ];
                trailer[ "SID" ] = Header[ "SID" ];
                trailer[ "SIDPassword" ] = Header[ "SIDPassword" ];
                factory.RequestToImpl( trailer ).SetField( "CreationDate", Header[ "CreationDate" ], true );
                Add( trailer, true );

                writer.Seek( 0, SeekOrigin.Begin );
                writer.Write( 1 );

                writer.Close();
                this.batchClosed = true;

                factory.RemoveSubmission( Name );

                Logger.InfoFormat( "Batch file {0} now closed.", FileName );
            }
        }

        /// <summary>
        /// Closes the file, but does not complete the batch.
        /// </summary>
        /// <remarks>
        /// Close() differs from CloseBatch() in that CloseBatch() writes
        /// the trailer records to the file and prepares the batch for
        /// sending. Close() simply closes the stream to the file so that
        /// other objects can access it.
        /// </remarks>
        public void Close()
        {
            writer.Close( false );
        }

        /// <summary>
        /// Creates a new submission file and stores the filename.
        /// </summary>
        /// <remarks>
        /// The submission file is created in the subdirectory specified in
        /// the MSDKConfig.xml file by the PropertyList/BatchDirectories/outgoing
        /// tag.
        ///
        /// Once this method has been called, closeBatch must be called to
        /// close the file.
        /// </remarks>
        /// <param name="header">The Request object that defines the header record.</param>
        /// <returns>True if the batch was created successfully, false if not.</returns>
        public void CreateBatch( IRequest header )
        {
            lock ( submissionLock )
            {
                if ( writer.Open )
                {
                    return;
                }

                TestForNull( header, Error.NullRequest, "header request" );

                if ( header.TransactionType != Configurator.SpecialTemplateNames.Header )
                {
                    Logger.Error( "The Request is not a valid header." );
                    throw new SubmissionException( Error.InvalidHeaderRequest, "The Request is not a valid header." );
                }

                var outgoingDir = Config[ "OutgoingBatchDirectory" ];
                if ( FileName != null )
                {
                    if ( factory.MakeFileManager().Exists( FileName ) )
                    {
                        var msg = $"The file \"{FileName}\" already exists.";
                        Logger.Error( msg );
                        throw new SubmissionException( Error.FileExists, msg );
                    }
                }
                else
                {
                    FileName = this.GetFileName( Name, FileName, outgoingDir );
                    if ( FileName != null )
                    {
                        var msg = $"The file \"{FileName}\" already exists.";
                        Logger.Error( msg );
                        throw new SubmissionException( Error.FileExists, msg );
                    }
                }

                try
                {
                    factory.RequestToImpl( header ).SetField( "FileName", Name, true );
                }
                catch
                {
                }

                try
                {
                    factory.RequestToImpl( header ).SetField( "CreationDate", GetCurrentDateString(), true );
                }
                catch
                {
                    Logger.Warn( "Failed to set CreationDate." );
                }

                var incomingFile = GetFileName( Name, FileName, Config[ "IncomingBatchDirectory" ] );
                if ( incomingFile != null )
                {
                    var msg = $"A response file with the name \"{Name}\" already exists.";
                    Logger.Error( msg );
                    throw new SubmissionException( Error.FileExists, msg );
                }

                if ( FileName == null )
                {
                    if ( !Utils.IsAbsolutePath( outgoingDir ) )
                    {
                        FileName = $"{factory.HomeDirectory}\\{outgoingDir}\\{Name}";
                    }
                    else
                    {
                        FileName = $"{outgoingDir}\\{Name}";
                    }
                }

                Logger.InfoFormat( "Creating the batch file {0}.", FileName );

                var converter = factory.MakeBatchConverter( Config );

                var args = converter.GetResponseRecordInfo( null, ResponseType.Response, Config.Protocol );
                var fileHeader = new FileHeader();
                fileHeader.BatchClosed = false;
                fileHeader.RecordDelimiter = args.RecordDelimiter;
                fileHeader.FileType = "Batch"; // User never sets "Batch", so it's not in ResponseType.
                fileHeader.FormatName = args.Format;
                fileHeader.FieldDelimiter = args.FieldDelimiter;
                fileHeader.LineLength = converter.BatchRecordLength;

                try
                {
                    writer.CreateFile( FileName, Password, fileHeader );
                }
                catch ( FilerException ex )
                {
                    throw new SubmissionException( ex.ErrorCode, "Failed to create batch", ex );
                }

                factory.RequestToImpl( header ).SetField( "VersionInfo", Metrics.ToBase64(), true );

                Add( header, true );

                Header = header;
            }
        }

        /// <summary>
        /// Creates a new submission file and stores the filename.
        /// </summary>
        /// <remarks>
        /// The submission file is created in the subdirectory specified in
        /// the MSDKConfig.xml file by the PropertyList/BatchDirectories/outgoing
        /// tag.
        ///
        /// Once this method has been called, closeBatch must be called to
        /// close the file.
        /// </remarks>
        public void CreateBatch()
        {
            lock ( submissionLock )
            {
                var request = factory.MakeRequest( factory.Config.SpecialTemplateNames.Header, Config );
                CreateBatch( request );
            }
        }


        /// <summary>
        /// Return the total number of records added to the submission.
        /// </summary>
        public long TotalRecords => amounts.records;

        /// <summary>
        /// Return the total of all amounts for the submission.
        /// </summary>
        public long TotalAmounts => amounts.totals;

        /// <summary>
        /// Return  the total of all sales amounts for the submission.
        /// </summary>
        public long TotalSalesAmounts => amounts.sales;

        /// <summary>
        /// Return the refund amount for the given order.
        /// </summary>
        public long TotalRefundAmounts => amounts.refunds;
        #endregion


        #region Private Methods

        private void TestForNull( object testObject, Error error, string name )
        {
            if ( testObject == null )
            {
                var msg = $"The {name} is null.";
                Logger.Error( msg );
                throw new SubmissionException( error, msg );
            }
        }

        private void Add( IRequest request, bool skipTotals )
        {
            if ( BatchClosed )
            {
                var message = "The batch has already been closed. You must create a new batch.";
                Logger.Error( message );
                throw new SubmissionException( Error.BatchNotOpen, message );
            }

            if ( writer == null || !writer.Open )
            {
                var message = "The batch is not open. Call CreateBatch first.";
                Logger.Error( message );
                throw new SubmissionException( Error.BatchNotOpen, message );
            }

            Logger.DebugFormat( "Added request [{0}] to the batch.", request.TransactionType );

            var args = new ConverterArgs();
//            args.Request = factory.RequestToImpl( request ).RawData;
            var recordLength = 121;
            IBatchConverter converter = null;
            if ( args.Request == null )
            {
                converter = factory.MakeBatchConverter( Config );
                args = converter.ConvertRequest( factory.RequestToImpl( request ) );
                if ( detailLogger.IsDebugEnabled )
                {
                    var maskedPayload = args.MaskedRequest;
                    detailLogger.Debug( "[" + maskedPayload + "]" );
                    detailLogger.Debug( request.DumpMaskedFieldValues() );
                }
                recordLength = converter.BatchRecordLength;
            }

            var orderPosition = 1;
            var skippedCount = 0;
            var parts = args.Request.Split( '\r' );
            for ( var i = 0; i < args.Request.Length; i += recordLength )
            {
                string record = null;
                if ( i + recordLength > args.Request.Length )
                {
                    record = args.Request.Substring( i );
                }
                else
                {
                    record = args.Request.Substring( i, recordLength );
                }

                try
                {
                    if ( writer.WriteRecord( Utils.StringToByteArray( record ) ) )
                    {
                        amounts.records++;
                    }
                    else
                    {
                        skippedCount++;
                    }

                    orderPosition++;
                }
                catch ( FilerException ex )
                {
                    Logger.Error( ex.Message, ex );
                    throw new SubmissionException( ex.ErrorCode, ex.Message, ex );
                }
            }

            if ( !skipTotals )
            {
                amounts.orders++;
            }

            UpdateAmounts( args );

            try
            {
                var sep = new OrderSeparator();
                if ( skipTotals )
                {
                    sep.Type = "Sp";
                }
                sep.Size = orderPosition - skippedCount - 1;
                sep.OrderCount = amounts.orders;
                sep.RecordCount = amounts.records;
                sep.TotalRefunds = amounts.refunds;
                sep.TotalSales = amounts.sales;
                sep.Totals = amounts.totals;
                writer.WriteRecord( Utils.StringToByteArray( sep.ToString() ), false );
                writer.Flush();
            }
            catch ( FilerException ex )
            {
                throw new SubmissionException( ex.ErrorCode, "Failed to update the header.", ex );
            }
        }

        private void UpdateAmounts( ConverterArgs args )
        {
            amounts.totals += args.LongData;
            switch ( args.OrderType )
            {
                case ConverterArgs.AmtType.Refund:
                    amounts.refunds += args.LongData;
                    break;
                case ConverterArgs.AmtType.Sales:
                    amounts.sales += args.LongData;
                    break;
            }
        }

        #endregion
    }
}
