using System;
using System.Collections.Generic;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Configurator;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// 
	/// </summary>
	public class BatchConverter : Converter, IBatchConverter
	{
        private static KeySafeDictionary<string, string> cachedValue = new KeySafeDictionary<string, string>();

		/// <summary>
		/// 
		/// </summary>
        public BatchConverter( ConfigurationData configData ) : this( configData, new ConverterFactory() )
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="factory"></param>
        public BatchConverter( ConfigurationData configData, IConverterFactory factory )
            : base( configData, factory )
		{
            LoadTemplates();
		}

		private void LoadTemplates()
		{
			if ( requestTemplate == null )
			{
                requestTemplate = factory.GetRequestTemplate( RequestType.Batch );
                responseTemplate = factory.GetResponseTemplate( RequestType.Batch );
			}
		}

        public static void Reset()
        {
            cachedValue.Clear();
        }

		public byte[] RequestFileTerminator 
		{
			get 
            {
                if ( requestTemplate.FileTerminator == null )
                {
                    return new byte[ 0 ];
                }
                return requestTemplate.FileTerminator.GetBytes(); 
            }
		}

		public int BatchRecordLength => requestTemplate.DefaultFormatLength + requestTemplate.FormatDelimiter.Length;

        public int MinBytesToRead => responseTemplate.MinBytesToRead;

        /// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="isMask"></param>
		/// <returns></returns>
		public new ConverterArgs ConvertRequest( IRequestImpl request )
		{
            LoadTemplates();
            var req = (IRequestImpl) request;
            var args = base.ConvertRequest( (IRequestImpl) request );
			args.FileTerminator = requestTemplate.FileTerminator;
            args.OrderType = GetOrderType( req, ConverterArgs.AmtType.Refund );
			if ( args.OrderType == ConverterArgs.AmtType.None )
			{
                args.OrderType = GetOrderType( req, ConverterArgs.AmtType.Sales );
			}

            return SetTotals( request, args );
		}

        private ConverterArgs SetTotals( IRequest request, ConverterArgs args )
        {
            args.OrderType = ConverterArgs.AmtType.None;
            args.LongData = 0;

            if ( request.TransactionType != "SubmissionOrder" )
            {
                return args;
            }

            var order = requestTemplate.GetOrder();

            // check it's order record
            args.LongData = request.GetLongField( "Amount" );
            var actionCode = request[ order.ActionCodeFieldName ];
            args.OrderType = ConverterArgs.AmtType.None;
            if ( args.LongData > 0 && actionCode != null )
            {
                if ( order.Lists[ "Refund" ].ActionCodeIn.Contains( actionCode ) )
                {
                    args.OrderType = ConverterArgs.AmtType.Refund;
                    return args;
                }

                if ( order.Lists[ "Sales" ].ActionCodeIn.Contains( actionCode ) )
                {
                    args.OrderType = ConverterArgs.AmtType.Sales;
                    return args;
                }

                if ( !order.Lists[ "Sales" ].ActionCodeNotIn.Contains( actionCode ) )
                {
                    args.OrderType = ConverterArgs.AmtType.Sales;
                    return args;
                }
            }

            return args;
        }

		public new ConverterArgs ConvertResponse( string resp, bool isPNS )
		{
            LoadTemplates();
            var isDelimited = IsDelimitedFormat( resp );
            if ( isDelimited ) 
			{
				return ConvertDelimitedResponse( resp, RecordInfo.FieldDelimiter );
			}

            this.MethodOfPayment = GetMOPFromOrderRecord( resp );
            this.record = resp;

			return base.ConvertResponse( resp, isPNS );
		}

        private bool IsDelimitedFormat(string resp )
	    {
		    if ( RecordInfo == null )
		    {
			    return false;
		    }
		
		    if ( RecordInfo.FieldDelimiter != null )
		    {
			    return true;
		    }
		
		    if ( RecordInfo.Format != null )
		    {
			    Format format = null;
			    try
			    {
				    format = responseTemplate.GetFormat( RecordInfo.Format );
			    } 
			    catch ( ConverterException )
			    {
				    return false;
			    }
			
			    try
			    {
				    if ( format.DelimitedResponseType != null )
				    {
					    if ( GetFieldDelimiter( resp, null, false ) != null )
                        {
						    return true;
					    }
				    }
			    }
			    catch ( ConverterException )
			    {
				    return false;
			    }
		    }
		
		    return false;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameValue"></param>
        /// <returns></returns>
        public IResponse ConvertSFTPErrorResponse( string data, string sftpFileName )
		{
            // Normalize the data.
            data = data.Replace( '\r', '\n' );
            data = data.Replace( "\n\n", "\n" );

            var delimiter = configData[ "ErrorDelimiter" ];
			if ( delimiter == null )
			{
                logger.Error( "The Error Delimiter is null." );
                throw new ConverterException( Error.NullDelimiter, "The Error Delimiter is null." );
			}

            var list = new List<DataElement>();

			var response = new Response();
			var element = new DataElement( "SFTPFile_ID", sftpFileName, response );
            response.DataElements.Add( element );
            list.Add( element );

			var lines = data.Split( '\n' );
			foreach ( var line in lines )
			{
				var fields = line.Split( delimiter[ 0 ] );

                if ( fields.Length < 2 )
                {
                    continue;
                }

				var name = fields[ 0 ].Replace( " ", "_" ).Trim();
				var val = fields[ 1 ].Replace( "[", "" ).Replace( "]", "" ).Trim();

                // If the key is numeric, split it into two error code and 
                // error description.
                if ( Utils.IsNumeric( name ) )
                {
                    element = new DataElement( "Error_Code", name, response );
                    response.DataElements.Add( element );
                    list.Add( element );
                    element = new DataElement( "Error_Description", val, response );
                    response.DataElements.Add( element );
                    list.Add( element );
                }
                else
                {
                    element = new DataElement( name, val, response );
                    response.DataElements.Add( element );
                    list.Add( element );
                }
            }

            response.DataElements = list;
			return response;
		}

        public ConverterArgs GetResponseRecordInfo( string record, string responseType )
        {
            var args = new ConverterArgs();
            args.Response = record;
            args.StrData = responseType;
            return GetResponseRecordInfo( args );
        }

        public ConverterArgs GetResponseRecordInfo( string record, string responseType, CommModule module )
		{
			var args = new ConverterArgs();
            args.Response = record;
            args.StrData = responseType;
            args.CommModule = module;
			return GetResponseRecordInfo( args );
		}

        /// <summary>
        /// Gets information about the record to be used to determine what 
        /// format the record represents.
        /// </summary>
        /// <param name="incomingArgs"></param>
        /// <returns></returns>
		public ConverterArgs GetResponseRecordInfo( ConverterArgs incomingArgs )
        {
            LoadTemplates();
            var args = new ConverterArgs();
            this.RecordInfo = args;
            args.Format = args.StrData;
            args.StrData = incomingArgs.StrData != null ? incomingArgs.StrData : incomingArgs.Response;
            args.CommModule = incomingArgs.CommModule != CommModule.Unknown ? incomingArgs.CommModule : configData.Protocol;

            var finder = FindFormat( incomingArgs.Response, null );
            if ( finder != null )
            {
                args.Format = finder.FormatName;
                args.StrData = finder.ResponseType;

                var format = responseTemplate.GetFormat( args.Format );
                if ( format.IsEmpty )
                {
                    return args;
                }

                args.FileTerminator = format.GetFileTerminator( configData.Protocol, responseTemplate.FileTerminator );
                var fieldDelimiter = responseTemplate.GetFieldDelimiter( incomingArgs.Response, format, configData );
                if ( fieldDelimiter != null && incomingArgs.Response.Contains( fieldDelimiter ) )
                {
                    args.FieldDelimiter = fieldDelimiter;
                }

                var modeToUse = args.CommModule != CommModule.Unknown ? args.CommModule : configData.Protocol;
                args.RecordDelimiter = responseTemplate.GetRecordDelimiter( format, modeToUse );

                SetIntAndStrData( args, format );

                args.SkipWrite = format.SkipWrite;
                args.Format = format.GetResponseFormat();

                return args;
            }

            return args;
        }

        private void SetIntAndStrData( ConverterArgs args, Format format )
        {
            if ( args.FieldDelimiter == null )
            {
                var length = format.Length != 0 ? format.Length : responseTemplate.DefaultFormatLength;
                args.IntData = length + args.RecordDelimiter.Length;
                if ( args.StrData == null )
                {
                    args.StrData = format.GetResponseType( false );
                }
            }
            else
            {
                args.IntData = 0;
                if ( args.StrData == null )
                {
                    args.StrData = format.GetResponseType( true );
                    if ( args.StrData == null )
                    {
                        args.StrData = format.GetResponseType( true );
                    }
                }
            }
        }


        /// <summary>
        /// Gets the Order Amount Type for the order. 
        /// </summary>
        /// <remarks>
        /// <para>When a batch is closed, the SubmissionDescriptor creates a 
        /// series of Totals records that contain running totals of both sales 
        /// and refunds that have been added to the batch. These totals are 
        /// kept by the SubmissionOrder as a running count based on the orders 
        /// that have been converted and added to the batch.  
        /// </para>
        /// <para>
        /// The Converter must determine if an order represents a Sales, a 
        /// Refund, or neither. It sets the result in the OrderType property
        /// of the ConverterArgs it returns to the caller. 
        /// </para>
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="actionCodeType"></param>
        /// <returns></returns>
        private ConverterArgs.AmtType GetOrderType( IRequestImpl request, ConverterArgs.AmtType actionCodeType )
		{
			var order = requestTemplate.GetOrder();
            if ( !request.TransactionType.Equals( order.FormatName ) )
            {
				return ConverterArgs.AmtType.None;
			}
			var actionCode = request[ order.ActionCodeFieldName ];
			var amount = request[ order.AmountFieldName ];

			var list = order.Lists[ actionCodeType.ToString() ];

			var validOrderType = false;
			if ( list.Defaults != null && list.Defaults.Contains( actionCode ) )
			{
				validOrderType = true;
			}
			else if ( list.ActionCodeIn != null && list.ActionCodeIn.Contains( actionCode ) )
			{
				validOrderType = true;
			}
			else if ( list.ActionCodeNotIn != null && list.ActionCodeNotIn.Count > 0 )
			{
				validOrderType = !list.ActionCodeNotIn.Contains( actionCode );
			}

			if ( validOrderType && Utils.StringToInt( amount ) > 0 )
			{
				return actionCodeType;
			}

			return ConverterArgs.AmtType.None;
		}

       	private string GetFieldDelimiter( string resp, string fieldDelimiter, bool throwOnFailure )
	    {
		    if ( fieldDelimiter == null )
		    {
			    fieldDelimiter = configData[ "DFRDelimiter" ];
			    if ( fieldDelimiter == null && RecordInfo != null )
			    {
				    var recordFormat = responseTemplate.GetFormat( RecordInfo.Format );
				    if ( recordFormat != null )
				    {
					    fieldDelimiter = responseTemplate.GetFieldDelimiter( recordFormat.DelimitedResponseType, recordFormat, configData );
				    }
			    }
                fieldDelimiter = FindFieldDelimiter( fieldDelimiter, resp );
			    if ( fieldDelimiter == null && throwOnFailure )
			    {
				    logger.Error( "The field delimiter could not be found." );
				    throw new ConverterException( Error.NullDelimiter, "The field delimiter could not be found." );
			    }
		    }
		    return fieldDelimiter;
	    }

        private string FindFieldDelimiter( string delim, string resp )
        {
            var fieldDelimiter = delim;

            if ( fieldDelimiter == null || resp.Contains( fieldDelimiter ) )
            {
                if ( resp.Contains( "," ) )
                {
                    fieldDelimiter = ",";
                }
                else if ( resp.Contains( "|" ) )
                {
                    fieldDelimiter = "|";
                }
            }
           

            return fieldDelimiter;
        }

        private ConverterArgs ConvertDelimitedResponse( string resp, string fieldDelimiter )
		{
            logger.Debug( "Converting delimited response record." );
            logger.DebugFormat( "Field delimiter: [{0}]", fieldDelimiter );

            fieldDelimiter = GetFieldDelimiter( resp, fieldDelimiter, true );

            logger.DebugFormat( "Field delimiter: [{0}]", fieldDelimiter );

            var rawData = resp.GetBytes();
            var responseArgs = new ConverterArgs();

            var response = factory.MakeResponse();

			var containerFormat = GetContainerFormat();
            var format = GetFormatFromFirstField( containerFormat, resp, fieldDelimiter );
            if ( format == null )
            {
                format = containerFormat.GetFormatByData( resp );
            }

            CheckForConversionError( format, response, resp, fieldDelimiter, responseArgs );
            if ( response.IsConversionError )
            {
                return responseArgs;
            }

            var removeList = responseTemplate.DFRStrToRemove != null ? responseTemplate.DFRStrToRemove.Split( ',' ) : new string[ 0 ];

			var recordDelimiter = responseTemplate.GetRecordDelimiter( containerFormat, configData.Protocol );
			if ( resp.EndsWith( recordDelimiter ) )
			{
				resp = resp.Remove( resp.Length - recordDelimiter.Length );
			}

            response.RawData = rawData;
            var element = new DataElement( format.Alias, null, response );
            var elements = new List<DataElement>();
            elements.Add( element );

			var fieldData = BuildFieldDataList( resp.Split( fieldDelimiter[ 0 ] ) );
            var maskedFieldData = new List<string>( fieldData );

            var totalNumberOfFields = 0;
            for ( var i = 0; i < format.Fields.Count; i++ )
            {
                var field = format.Fields[ i ];
                if ( field == null )
                {
                    break;
                }

                logger.DebugFormat( "Converting field \"{0}\"", field.Name );

                var value = i < fieldData.Count ? fieldData[ i ] : field.DefaultValue;

                value = StripUnwantedText( value, removeList );

                if ( i < fieldData.Count || field.DefaultValue == null )
                {
                    ValidateMissingFields( format, field, fieldData, i, response );
                }

                totalNumberOfFields++;

                var fieldElement = new DataElement( field.Aliases[ 0 ], null, element );

                SetFieldValue( field, value, fieldElement, response, element );

                fieldElement.MaskedValue = MaskValue( field, fieldElement.Value, maskedFieldData, i );

                // This is tricky...
                // If a field is set to suppress and no value is set for the field, 
                // then we must not include the field.
                // However, if a value is present for the field, then we always 
                // include the field, even if it's suppressed.
                var suppress = field.IsSuppress && fieldElement.Value == null ;

                AddDelimitedField( field, suppress, element, fieldElement );
            }

            AddExtraFields( totalNumberOfFields, fieldData, response, element );

            response.DataElements = elements;
            responseArgs.Response = resp;
            responseArgs.ResponseData = response;
            responseArgs.MaskedResponse = FieldDataToString( maskedFieldData, fieldDelimiter[ 0 ] );
            return responseArgs;
		}

        private void SetFieldValue( Field field, string value, DataElement fieldElement, IFullResponse response, DataElement element )
        {
            if ( field.MultiColumn )
            {
                ParseMultiColumnField( field, value, fieldElement, element, response );
            }
            else
            {
                fieldElement.Value = value != null ? value.Trim() : null;
            }
        }

        private void AddDelimitedField( Field field, bool suppress, DataElement element, DataElement fieldElement )
        {
            if ( !field.Hide && !suppress )
            {
                element.Elements.Add( fieldElement );
                for ( var count = 1; count < field.Aliases.Count; count++ )
                {
                    var clone = new DataElement( fieldElement );
                    clone.Name = field.Aliases[ count ];
                    UpdateSubFieldFieldIDs( clone );
                    element.Elements.Add( clone );
                }
            }
            else
            {
                if ( !field.Hide )
                {
                    logger.DebugFormat( "Skipping suppressed field [{0}].", field.Name );
                }
                fieldElement.HideFromFieldID = true;
            }
        }

        private void UpdateSubFieldFieldIDs( DataElement clone )
        {
            var baseField = clone.FieldID;

            foreach ( var child in clone.Elements )
            {
                child.FieldID = baseField + "." + child.Name;
                child.IsAlias = true;
            }
        }

        private void CheckForConversionError( Format format, IFullResponse response, string resp, string fieldDelimiter, ConverterArgs responseArgs )
        {
            if ( format.IsEmpty || !resp.Contains( fieldDelimiter ) )
            {
                response.ErrorDescription = "The format could not be found. Ensure your field delimiter is set correctly.";
                response.IsConversionError = true;
                response.LeftoverData = resp;
                responseArgs.Response = resp;
                responseArgs.ResponseData = response;
            }
        }

        private Format GetFormatFromFirstField( Format containerFormat, string resp, string fieldDelimiter )
        {
            var fields = resp.Split( fieldDelimiter[ 0 ] );
            if ( fields == null || fields.Length == 0 )
            {
                return new Format();
            }
            return containerFormat.Formats[ fields[ 0 ] ];
        }

        /// <summary>
        /// Strip unwanted text from a DFR field value.
        /// </summary>
        /// <remarks>
        /// Some DFR fields contain prefix characters that belong in the 
        /// message, but are not meant to be part of the data to return to the
        /// merchant. An example is for the Frequency field, the value looks
        /// like "FREQ=DAILY", where the merchant only cares about the "DAILY"
        /// There is a finite set of text that needs to be stripped from 
        /// the data, and this method removes it.
        /// </remarks>
        /// <param name="value">The value to strip the unwanted text from</param>
        /// <param name="removeList">A list of unwanted text retrieved from 
        /// DFRStrToRemove in the converter template.</param>
        /// <returns></returns>
        private string StripUnwantedText( string value, string[] removeList )
        {
            if ( IsEmpty( value ) )
            {
                return value;
            }

            foreach ( var remove in removeList )
            {
                value = value.Replace( remove, "" );
            }
            return value;
        }

        private void ValidateMissingFields( Format format, Field field, List<string> fieldData, int currentFieldCount, IFullResponse response )
        {
            if ( currentFieldCount >= fieldData.Count && !field.IsSuppress && !format.IgnoreShort )
            {
                response.ErrorDescription =
                    $"Delimited record is missing data. Expected {format.Fields.Count}, received {fieldData.Count}.";
                response.IsConversionError = true;
                response.LeftoverData = null;
            }
        }

        private void AddExtraFields( int totalNumberOfFields, List<string> fieldData, IFullResponse response, DataElement element )
        {
            var sftpDelim = responseTemplate.SFTPRecordDelimiter;
            var tcpDelim = responseTemplate.TCPRecordDelimiter;

            if ( totalNumberOfFields < fieldData.Count )
            {
                response.NumExtraFields = 0;
                var count = 1;
                for ( var i = totalNumberOfFields; i < fieldData.Count; i++ )
                {
                    var name = $"ExtraField{count++}";
                    var extraElement = new DataElement( name, null, element );
                    
                    var value = fieldData[ i ];
                    if ( value != null && ( value.EndsWith( sftpDelim ) || value.EndsWith( tcpDelim ) ) )
                    {
                        value = value.Remove( value.Length - 1, 1 );
                    }

                    extraElement.Value = value;

                    extraElement.MaskedValue = extraElement.Value;
                    element.Elements.Add( extraElement );
                    response.NumExtraFields++;
                }
            }
        }

        private string FieldDataToString( List<string> maskedFieldData, char fieldDelimiter )
        {
            var data = new StringBuilder();
            for ( var i = 0; i < maskedFieldData.Count; i++ )
            {
                if ( i > 0 )
                {
                    data.Append( fieldDelimiter );
                }
                data.Append( maskedFieldData[ i ] );
            }
            return data.ToString();
        }

        private string MaskValue( Field field, string value, List<string> fieldData, int index )
        {
            if ( field.MaskWith == null )
            {
                return value;
            }

            if ( value == null )
            {
                value = "";
            }

            var maskLength = field.MaskLength == -1 ? value.Length : field.MaskLength;
            if ( field.MaskJustification == TextJustification.Left )
            {
                var padded = Utils.PadLeft( "", field.MaskWith[ 0 ], maskLength );
                var diff = value.Length - padded.Length;
                if ( diff > 0 )
                {
                    padded += value.Substring( value.Length - diff );
                }
                if ( fieldData.Count > index )
                {
                    fieldData[ index ] = padded;
                }
                return padded;
            }

            return Utils.PadRight( value, field.MaskWith[ 0 ], maskLength );
        }

        private void ParseMultiColumnField( Field field, string value, DataElement parentElement, DataElement rootElement, IFullResponse response )
        {
            logger.DebugFormat( "Parsing multi-column field [{0}]", field.Name );

            foreach ( var child in field.Fields )
            {
                var fieldElement = new DataElement( child.Name, null, parentElement );
				var pos = child.Position - 1;
                if ( child.Length > value.Length - pos )
                {
                    if ( value.Length >= pos )
                    {
                        var val = value.Substring( pos );
                        fieldElement.Value = val != null ? val.Trim() : val;
                    }
                }
                else
                {
                    var val = value.Substring( pos, child.Length );
                    fieldElement.Value = val != null ? val.Trim() : val;
                }

                fieldElement.MaskedValue = fieldElement.Value;

                if ( !child.Hide )
                {
                    parentElement.Elements.Add( fieldElement );
                    for ( var count = 1; count < child.Aliases.Count; count++ )
                    {
                        var clone = new DataElement( fieldElement );
                        clone.Name = child.Aliases[ count ];
                        parentElement.Elements.Add( clone );
                    }
                }
            }
        }

		private List<string> BuildFieldDataList( string[] parts )
		{
			var fieldData = new List<string>();
			string data = null;
			for ( var i = 0; i < parts.Length; i++ )
			{
				if ( data == null )
				{
					data = parts[ i ];
					if ( data.Trim().StartsWith( "\"" ) && !data.Trim().EndsWith( "\"" ) )
					{
						continue;
					}
					fieldData.Add( data );
					data = null;
				}
				else
				{
					data += "," + parts[ i ];
					if ( !data.Trim().EndsWith( "\"" ) )
					{
						continue;
					}

					data = data.Trim();
					fieldData.Add( data );
					data = null;
				}
			}

			return fieldData;
		}

	}
}
