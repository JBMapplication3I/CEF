#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.IO;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Converts files from AES to PGP and from PGP to AES.
    /// </summary>
    /// <remarks>
    /// When converting from PGP to AES, PGPFileConverter will first
    /// treat the file as though it is a signed PGP file. If the
    /// signed conversion fails, it will then try to convert it as
    /// though it is an unsigned PGP file.
    /// </remarks>
    public class PGPFileConverter : FrameworkBase, IFileConverter
    {
        private ConfigurationData configData;
        private bool verifySignature;
        private string fileType;

        /// <summary>
        ///
        /// </summary>
        public string FileType
        {
            get => fileType;
            set => fileType = value;
        }

        /// <summary>
        /// Constructory.
        /// </summary>
        /// <param name="configData"></param>
        /// <param name="factory"></param>
        public PGPFileConverter( ConfigurationData configData, IDispatcherFactory factory )
        {
            this.configData = configData;
            this.factory = factory;

            verifySignature = configData.GetBool( "VerifyPGPSignature", false );
            if ( configData[ "PGPMerchantPassPhrase" ] == null )
            {
                configData[ "PGPMerchantPassPhrase" ] = "";
            }
        }

        #region IFileConverter Members

        /// <summary>
        /// Converts a file into a PGP encrypted file.
        /// </summary>
        /// <remarks>
        /// The resulting PGP file will not be compressed, as the
        /// SFTP servers will fail to decrypt a
        /// compressed PGP file.
        /// </remarks>
        /// <param name="reader"></param>
        /// <param name="filename"></param>
        public void ConvertTo( IFileReader reader, string filename )
        {

            Stream oStream = new FileStream( filename, FileMode.Create, FileAccess.Write,
                FileShare.Write );
            Stream outputStream = new ArmoredOutputStream( oStream );
            Stream cOut = null;
            PgpPublicKey encKey = null;

            try
            {
                encKey = CreateKey( configData[ "PGPServerPublicKey" ] );
                var cPk = new PgpEncryptedDataGenerator
                    ( SymmetricKeyAlgorithmTag.Cast5, true, new SecureRandom() );

                cPk.AddMethod( encKey );

                cOut = cPk.Open( outputStream, new byte[ 1 << 16 ] );
                WriteFileToLiteralData( cOut, PgpLiteralData.Binary, reader );

            }
            catch ( Exception e )
            {
                Logger.Error( e.Message, e );
                throw new FilerException( Error.EncryptionFailed, e.Message, e );
            }
            finally
            {
                CloseStream( cOut );
                CloseStream( outputStream );
                CloseStream( oStream );
            }
        }

        /// <summary>
        /// Get the JPMC public key and return a Stream to it
        /// </summary>
        /// <returns></returns>
        private Stream GetPublicKey()
        {
            Stream instream = null;
            try
            {
                var keyName = configData[ "PGPServerPublicKey" ];

                var path = Utils.GetFilePath( keyName, "config", Configurator.HomeDirectory );

                if ( path == null )
                {
                    var msg = "The CPS PGP public key: " + keyName + " could not be found.";
                    Logger.Error( msg );
                    throw new FilerException( Error.FileNotFound, msg );
                }

                instream = File.OpenRead( path );

                if ( instream == null )
                {
                    var msg = "The CPS PGP public key could not be found for TCPBatch";
                    Logger.Error( msg );
                    throw new FilerException( Error.FileNotFound, msg );
                }
            }
            catch ( FileNotFoundException ex )
            {
                var msg = "The CPS PGP public key could not be found for TCPBatch";
                Logger.Error( msg, ex );
                throw new FilerException( Error.EncryptionFailed, msg, ex );
            }
            return instream;
        }

        /// <summary>
        /// Convert a PGP file to an AES file.
        /// </summary>
        /// <param name="pgpFile">The PGP file to convert.</param>
        /// <param name="aesFile">The AES file to write to.</param>
        /// <returns></returns>
        public void ConvertFrom( string pgpFile, string aesFile )
        {
            var aesPassword = configData[ "SubmissionFilePassword" ];
            string keyFile = null;
            Stream fileIn = null;

            try
            {
                fileIn = File.OpenRead( pgpFile );
            }
            catch ( Exception ex )
            {
                CloseStream( fileIn );

                var msg = pgpFile + ": " + ex.Message;
                Logger.Error( msg, ex );
                throw new FilerException( Error.FileNotFound, msg, ex );
            }

            Stream keyIn = null;
            IFileWriter writer = null;
            Stream inputStream = null;
            Stream compressedStr = null;

            // this try is only for the finally
            try
            {
                writer = factory.MakeFileWriter( configData );

                inputStream = PgpUtilities.GetDecoderStream( fileIn );

                var pgpF = new PgpObjectFactory( inputStream );

                var obj = pgpF.NextPgpObject();

                // handle case where first structure is a marker
                if ( obj is PgpMarker )
                {
                    obj = pgpF.NextPgpObject();
                }

                // first see if this is just a simple compressed file
                if ( obj is PgpCompressedData )
                {
                    Logger.Debug( "This file is only compressed" );

                    var man = factory.MakeBatchConverter( configData );

                    var lineLength = man.BatchRecordLength;

                    writer.CreateFile( aesFile, aesPassword,
                        CreateFileHeader( lineLength ) );

                    PgpOnePassSignature ops = null;
                    PgpObjectFactory comFact = null;

                    compressedStr = GetCompressedData( (PgpCompressedData) obj, out ops,
                        out comFact );

                    // see if a signature was found, we should never see this
                    if ( ops != null )
                    {
                        var msg = "Module does not support Compressed, Signed PGP files";
                        Logger.Error( msg );
                        throw new FilerException( Error.DecryptionFailed, msg );
                    }

                    WritePgpToAes( compressedStr, writer, lineLength, null );
                }
                // if it wasn't just compressed then it has to start with encryption objects
                else if ( obj is PgpEncryptedDataList )
                {
                    keyFile = Utils.GetFilePath(
                        configData[ "PGPMerchantPrivateKey" ],
                        "config", Configurator.HomeDirectory );

                    TestForNull( keyFile, Error.FileNotFound,
                        $"The private key file \"{configData["PGPMerchantPrivateKey"]}\" could not be found.", true );

                    try
                    {
                        keyIn = File.OpenRead( keyFile );
                    }
                    catch ( Exception ex )
                    {
                        var msg = keyFile + ": " + ex.Message;
                        Logger.Error( msg, ex );
                        throw new FilerException( Error.FileNotFound, msg, ex );
                    }

                    if ( !DecryptFile( aesFile, aesPassword,
                        (PgpEncryptedDataList) obj, keyIn, writer ) )
                    {
                        var msg = "Failed to decrypt file";
                        Logger.Error( msg );
                        throw new FilerException( Error.DecryptionFailed, msg );
                    }
                }
                else
                {
                    var msg = "Encountered unexpected PGP object of type: " + obj.GetType().Name;
                    Logger.Error( msg );
                    throw new FilerException( Error.DecryptionFailed, msg );
                }
            }
            finally
            {
                CloseStream( fileIn );
                CloseStream( keyIn );
                CloseStream( inputStream );
                CloseStream( compressedStr );

                try { writer.Close(); }
                catch { }
            }
        }


        /// <summary>
        /// Just decompress a file and optionally start the signature verification process
        /// </summary>
        /// <param name="comData"></param>
        /// <param name="ops"></param>
        /// <param name="comFact"></param>
        /// <returns></returns>
        private Stream GetCompressedData( PgpCompressedData comData, out PgpOnePassSignature ops,
            out PgpObjectFactory comFact )
        {
            Stream retVal = null;
            ops = null;
            comFact = null;

            try
            {
                comFact = new PgpObjectFactory( comData.GetDataStream() );

                var obj = comFact.NextPgpObject();

                if ( obj is PgpLiteralData )
                {
                    retVal = ((PgpLiteralData) obj).GetInputStream();
                }
                else if ( obj is PgpOnePassSignatureList )
                {
                    if ( verifySignature )
                    {
                        // Verify the signature
                        ops = InitSignatureVerify( (PgpOnePassSignatureList) obj );

                        if ( ops == null )
                        {
                            var msg = "Failed to verify the signature";
                            Logger.Error( msg );
                            throw new FilerException( Error.DecryptionFailed, msg );
                        }
                    }

                    obj = comFact.NextPgpObject();

                    if ( !(obj is PgpLiteralData) )
                    {
                        var msg = "Encountered unexpected PGP object of type: " + obj.GetType().Name;
                        Logger.Error( msg );
                        throw new FilerException( Error.DecryptionFailed, msg );
                    }

                    retVal = ((PgpLiteralData) obj).GetInputStream();
                }
                else
                {
                    var msg = "Encountered unexpected PGP object of type: " + obj.GetType().Name;
                    Logger.Error( msg );
                    throw new FilerException( Error.DecryptionFailed, msg );
                }
            }
            catch ( FilerException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.DecryptionFailed, ex.Message, ex );
            }

            return retVal;
        }

        /// <summary>
        /// Start the signature verification process
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private PgpOnePassSignature InitSignatureVerify( PgpOnePassSignatureList list )
        {
            PgpOnePassSignature ops = null;
            Stream keyIn = null;

            try
            {
                keyIn = GetPublicKey();

                var pgpRing = new PgpPublicKeyRingBundle(
                    PgpUtilities.GetDecoderStream( keyIn ) );

                PgpPublicKey key = null;
                for ( var i = 0; i < list.Count; i++ )
                {
                    ops = (PgpOnePassSignature) list[ i ];

                    key = pgpRing.GetPublicKey( ops.KeyId );

                    if ( key == null )
                    {
                        continue;
                    }

                    ops.InitVerify( key );
                    break;
                }

                if ( key == null || key.KeyId != ops.KeyId )
                {
                    ops = null;
                }
            }
            finally
            {
                CloseStream( keyIn );
            }

            return ops;
        }

        /// <summary>
        /// Copy data from a pgp encrypted file to an AES encrypted file,
        /// update signature info (if needed) while doing it.
        /// </summary>
        /// <param name="unc"></param>
        /// <param name="writer"></param>
        /// <param name="lineLength"></param>
        /// <param name="ops"></param>

        private void WritePgpToAes(
            Stream unc,
            IFileWriter writer,
            int lineLength,
            PgpOnePassSignature ops )
        {
            byte[] line = null;

            byte prevTerminator = 0;
            while ( (line = ReadLine( unc, lineLength ) ).Length == lineLength )
            {
                if ( ops != null )
                {
                    ops.Update( line );
                }

                // When doing CI testing, the file sent is the file
                // returned,
                // which has an extra newline. Remove it, if it exists.
                // It gets a little tricky when the last byte from the
                // previous line has the \r and the first in the new
                // line has the \n.
                var rec = Utils.ByteArrayToString( line );
                rec = rec.Replace( "\r\n", "\r" );
                if ( line[ 0 ] == '\n' && prevTerminator == '\r' )
                {
                    rec = rec.Remove( 0, 1 );
                }
                if ( line[ 0 ] == '\n' && prevTerminator == '\n' )
                {
                    rec = rec.Remove( 0, 1 );
                }
                prevTerminator = line[ line.Length - 1 ];

                // Only write if the line byte-array is not empty.
                if ( line[ 0 ] != 0 )
                {
                    writer.WriteRecord( rec.GetBytes() );
                }
            }

            var bytesRead = line.Length;

            // Write any leftover data.
            if ( bytesRead > 0 && bytesRead < lineLength )
            {
                var newBuf = new byte[ bytesRead ];
                for ( var i = 0; i < bytesRead; i++ )
                {
                    newBuf[ i ] = line[ i ];
                }

                if ( ops != null )
                {
                    ops.Update( newBuf );
                }

                writer.WriteRecord( newBuf );
            }


            // Added by Ramesh
            // Sending an additional record terminator for PGP over TCP
            // writer will ignore the additional one for regular files
            if ( writer.RecordTerminator != null )
            {
                writer.WriteRecord( writer.RecordTerminator );
            }

            if ( writer.FileHeader != null )
            {
                FileType = writer.FileHeader.FileType;
            }
            else
            {
                FileType = writer.FormatName;
            }
        }

        /// <summary>
        /// Reads a single line from the stream. The stream may not
        /// always read all the requested bytes from the stream
        /// when it's not empty, so keep reading until we have
        /// the right number of bytes (or we read 0 bytes).
        /// </summary>
        /// <param name="unc"></param>
        /// <param name="lineLength"></param>
        /// <returns></returns>
        private byte[] ReadLine( Stream unc, int lineLength )
        {
            var bytes = new List<byte>();
            var bytesRead = 0;
            do
            {
                var line = new byte[ lineLength ];
                bytesRead = unc.Read( line, 0, lineLength - bytes.Count );

                if ( bytesRead > 0 && bytesRead < lineLength )
                {
                    // In this case, we can't let us have a 121 byte
                    // array with most set to 0. So, we must fill an
                    // array of the right size.
                    var newLine = new byte[ bytesRead ];
                    for ( var i = 0; i < bytesRead; i++ )
                    {
                        newLine[ i ] = line[ i ];
                    }
                    line = newLine;
                }

                if ( bytesRead > 0 )
                {
                    bytes.AddRange( line );
                }
            }
            while ( bytes.Count < lineLength && bytesRead > 0 );

            if ( bytes.Count == 0 )
            {
                return new byte[ 0 ];
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// Convert an error file to an AES file.
        /// </summary>
        /// <param name="pgpFile">The PGP file to convert.</param>
        /// <returns></returns>
        public string ConvertErrorFrom( string pgpFile )
        {
            var keyFile = Utils.GetFilePath( configData[ "PGPMerchantPrivateKey" ], "config", Configurator.HomeDirectory );
            string retVal = null;

            TestForNull( keyFile, Error.FileNotFound,
                $"The file \"{configData["PGPMerchantPrivateKey"]}\" could not be found.", true );

            Stream fileIn = File.OpenRead( pgpFile );
            Stream keyIn = File.OpenRead( keyFile );
            Stream dIn = null;

            IFileWriter writer = new BufferFileWriter();

            try
            {
                var inputStream = PgpUtilities.GetDecoderStream( fileIn );
                var pgpF = new PgpObjectFactory( inputStream );
                var obj = pgpF.NextPgpObject();

                if ( obj is PgpMarker )
                {
                    obj = pgpF.NextPgpObject();
                }

                if ( obj is PgpEncryptedDataList )
                {
                    PgpOnePassSignature ops = null;

                    dIn = GetDecryptor( (PgpEncryptedDataList) obj, keyIn, 64 * 1024,
                        out pgpF, out ops );

                    if ( dIn == null )
                    {
                        var msg = "Failed to get the decryptor. " +
                            "Make sure your key and passphrase are correct.";
                        Logger.Error( msg );
                        throw new FilerException( Error.DecryptionFailed, msg );
                    }

                    var info = new FileInfo( pgpFile );

                    var record = GetSignedRecord( dIn, ops );
                    writer.WriteRecord( record );
                }
                else
                {
                    var msg = "Encountered unexpected PGP object of type: " + obj.GetType().Name;
                    Logger.Error( msg );
                    throw new FilerException( Error.DecryptionFailed, msg );
                }
            }
            catch ( FilerException )
            {
                throw;
            }
            finally
            {
                CloseStream( fileIn );
                CloseStream( keyIn );
                CloseStream( dIn );
            }

            retVal = writer.DecryptFile().Trim();

            try { writer.Close(); }
            catch { }

            // Return null if not an error file.
            if ( !retVal.Contains( "User ID:" ) && !retVal.Contains( "File ID:" ) )
            {
                Logger.Debug( "Converted file does not contain error file text." );
                return null;
            }

            return retVal;
        }

        public void ConvertClearToAES( string clearFile, string aesFile )
        {
            var aesPassword = configData[ "SubmissionFilePassword" ];
            Stream fileIn = null;

            try
            {
                fileIn = File.OpenRead( clearFile );
            }
            catch ( Exception ex )
            {
                CloseStream( fileIn );

                var msg = clearFile + ": " + ex.Message;
                Logger.Error( msg, ex );
                throw new FilerException( Error.FileNotFound, msg, ex );
            }

            Stream keyIn = null;
            IFileWriter writer = null;
            Stream inputStream = null;

            // this try is only for the finally
            try
            {
                writer = factory.MakeFileWriter( configData );

                Logger.Debug( "This file is only compressed" );

                var man = factory.MakeBatchConverter( configData );

                var lineLength = man.BatchRecordLength;

                writer.CreateFile( aesFile, aesPassword, CreateFileHeader( lineLength ) );

                WritePgpToAes( fileIn, writer, lineLength, null );
            }
            finally
            {
                CloseStream( fileIn );
                CloseStream( keyIn );
                CloseStream( inputStream );
                CloseStream( fileIn );

                try { writer.Close(); }
                catch { }
            }
        }

        #endregion

        #region Private Members

        private FileHeader CreateFileHeader( int lineLength )
        {
            var header = new FileHeader();
            header.BatchClosed = true;
            header.FieldDelimiter = null;
            header.FileType = null;
            header.FormatName = null;
            header.LineLength = lineLength;
            header.RecordDelimiter = null;
            return header;
        }

        private byte[] GetSignedRecord( Stream dIn, PgpOnePassSignature ops )
        {
            return GetSignedRecord( dIn, ops, 1000 );
        }

        private byte[] GetSignedRecord( Stream dIn, PgpOnePassSignature ops,
            int recordLength )
        {
            int ch;
            var count = 0;
            var record = new List<byte>();
            while ( (ch = dIn.ReadByte()) >= 0 )
            {
                if ( ops != null )
                {
                    ops.Update( (byte) ch );
                }
                if ( count < recordLength )
                {
                    record.Add( (byte) ch );
                    count++;
                }
                if ( count == recordLength )
                {
                    return record.ToArray();
                }
            }

            if ( record.Count == 0 )
                return null;

            return record.ToArray();
        }

        private OrderSeparator CreateSeparator( byte[] record )
        {
            var line = Utils.ByteArrayToString( record );
            var sep = new OrderSeparator();
            if ( line.StartsWith( "PID" ) || line.StartsWith( "B " ) ||
                line.StartsWith( "T " ) )
            {
                sep.Type = "Sp";
            }
            return sep;
        }

        /// <summary>
        /// Decrypt a file and optionally check the signature (if there is one)
        /// </summary>
        /// <param name="aesFile"></param>
        /// <param name="aesPassword"></param>
        /// <param name="obj"></param>
        /// <param name="keyIn"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        private bool DecryptFile( string aesFile, string aesPassword, PgpEncryptedDataList obj,
            Stream keyIn, IFileWriter writer )
        {
            var retVal = false;
            Stream dIn = null;

            try
            {
                PgpOnePassSignature ops = null;
                PgpObjectFactory comFact = null;
                var converter = factory.MakeBatchConverter( configData );

                var recordLength = converter.BatchRecordLength;

                dIn = GetDecryptor( obj, keyIn, recordLength,
                    out comFact, out ops );

                if ( dIn != null )
                {
                    var header = CreateFileHeader( recordLength );

                    writer.CreateFile( aesFile, configData[ "SubmissionFilePassword" ], header );

                    WritePgpToAes( dIn, writer, recordLength, ops );

                    if ( verifySignature && ops != null )
                    {
                        var newObj = comFact.NextPgpObject();

                        // finalize the signature check
                        if ( newObj is PgpSignatureList )
                        {
                            var p3 = (PgpSignatureList) newObj;

                            if ( !ops.Verify( p3[ 0 ] ) )
                            {
                                var msg = "Failed to verify the signature";
                                Logger.Error( msg );
                                throw new FilerException( Error.DecryptionFailed, msg );
                            }
                        }
                        else // if ( !( newObj is PgpEncryptedDataList ) )
                        {
                            var msg = "Encountered unexpected PGP object of type: " + obj.GetType().Name;
                            Logger.Error( msg );
                            throw new FilerException( Error.DecryptionFailed, msg );
                        }
                    }
                    retVal = true;
                }
            }
            catch ( FilerException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                var msg = "Failed to decrypt the file";
                Logger.Error( msg, ex );
                throw new FilerException( Error.DecryptionFailed, msg, ex );
            }
            finally
            {
                CloseStream( dIn );
            }

            return retVal;
        }

        /// <summary>
        /// Returns a PgpLiteralData stream
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="keyIn"></param>
        /// <param name="lineLength"></param>
        /// <param name="pgpF"></param>
        /// <param name="ops"></param>
        /// <returns></returns>
        private Stream GetDecryptor( PgpEncryptedDataList obj, Stream keyIn,
            int lineLength, out PgpObjectFactory pgpF, out PgpOnePassSignature ops )
        {
            ops = null;
            Stream retVal = null;
            pgpF = null;
            Stream decodeStream = null;
            try
            {
                //
                // find the private key
                //
                PgpPrivateKey sKey = null;

                PgpPublicKeyEncryptedData pbe = null;

                decodeStream = PgpUtilities.GetDecoderStream( keyIn );

                var pgpSec = new PgpSecretKeyRingBundle(
                     decodeStream );

                var passChars = new char[ 0 ];
                if ( configData[ "PGPMerchantPassPhrase" ] != null )
                {
                    passChars = configData[ "PGPMerchantPassPhrase" ].ToCharArray();
                }

                foreach ( PgpPublicKeyEncryptedData pked in obj.GetEncryptedDataObjects() )
                {
                    sKey = FindSecretKey( pgpSec, pked.KeyId, passChars );

                    if ( sKey != null )
                    {
                        pbe = pked;
                        break;
                    }
                }

                if ( sKey == null )
                {
                    var msg = "Private key in key file: " + configData[ "PGPMerchantPrivateKey" ] + " not found.";
                    Logger.Error( msg );
                    throw new FilerException( Error.DecryptionFailed, msg );
                }

                // now read what is in the key
                var clear = pbe.GetDataStream( sKey );
                var plainFact = new PgpObjectFactory( clear );
                pgpF = plainFact;
                var keyobj = plainFact.NextPgpObject();

                if ( keyobj is PgpOnePassSignatureList )
                {
                    if ( verifySignature )
                    {
                        ops = InitSignatureVerify(
                            (PgpOnePassSignatureList) keyobj );

                        if ( ops == null )
                        {
                            var msg = "Failed to verify the signature";
                            Logger.Error( msg );
                            throw new FilerException( Error.DecryptionFailed, msg );
                        }
                    }

                    keyobj = plainFact.NextPgpObject();

                    if ( keyobj is PgpLiteralData )
                    {
                        var p2 = (PgpLiteralData) keyobj;

                        retVal = p2.GetInputStream();
                    }
                }
                // if no signature, it should just be compressed
                else if ( keyobj is PgpCompressedData )
                {
                    retVal = GetCompressedData( (PgpCompressedData) keyobj, out ops,
                        out pgpF );
                }
                else if ( keyobj is PgpLiteralData )
                {
                    retVal = ((PgpLiteralData) keyobj).GetInputStream();
                }
                else
                {
                    var msg = "Unexpected PGP object of type: " + keyobj.GetType().Name;
                    Logger.Error( msg );
                    throw new FilerException( Error.DecryptionFailed, msg );
                }
            }
            catch ( Exception ex )
            {
                Logger.Warn( "Failed to decrypt signed file.", ex );
                CloseStream( retVal );
                retVal = null;
                throw;
            }
            finally
            {
                CloseStream( decodeStream );
            }


            return retVal;
        }

        /// <summary>
        /// Search a secret key ring collection for a secret key corresponding to
        /// keyId if it exists.
        /// </summary>
        /// <param name="pgpSec">A secret key ring collection.</param>
        /// <param name="keyId">KeyId we want.</param>
        /// <param name="pass">Passphrase to decrypt secret key with.</param>
        /// <returns></returns>
        private PgpPrivateKey FindSecretKey( PgpSecretKeyRingBundle pgpSec,
            long keyId, char[] pass )
        {
            try
            {
                var pgpSecKey = pgpSec.GetSecretKey( keyId );

                if ( pgpSecKey == null )
                {
                    return null;
                }

                return pgpSecKey.ExtractPrivateKey( pass );
            }
            catch ( Exception ex )
            {
                var msg =
                    $"{"Private key could not be found. Verify that the"}{" key file and passphrase is correct in your ProtocolManager."}";
                Logger.Error( msg, ex );
                throw new FilerException( Error.DecryptionFailed, msg, ex );
            }
        }

        private PgpPublicKey CreateKey( string filename )
        {
            var keyFile = Utils.GetFilePath( filename, "config", Configurator.HomeDirectory );
            TestForNull( keyFile, Error.FileNotFound,
                $"The file \"{filename}\" could not be found.", true );

            Stream inputStream = new FileStream( keyFile, FileMode.Open,
                FileAccess.Read, FileShare.Read );
            Stream decodeStream = null;
            try
            {
                decodeStream = PgpUtilities.GetDecoderStream( inputStream );

                var pgpPub = new PgpPublicKeyRingBundle( decodeStream );

                //
                // we just loop through the collection till we find a key suitable
                // for encryption, in the real
                // world you would probably want to be a bit smarter about this.
                //

                //
                // iterate through the key rings.
                //

                foreach ( PgpPublicKeyRing kRing in pgpPub.GetKeyRings() )
                {
                    foreach ( PgpPublicKey k in kRing.GetPublicKeys() )
                    {
                        if ( k.IsEncryptionKey )
                        {
                            return k;
                        }
                    }
                }
            }
            finally
            {
                CloseStream( inputStream );
                CloseStream( decodeStream );
            }

            throw new ArgumentException( "Can't find encryption key in key ring." );
        }

        /// <summary>Write out the passed in file as a literal data packet.</summary>
        private void WriteFileToLiteralData(
            Stream outputStream,
            char fileType,
            IFileReader reader )
        {

            var inc = configData.GetBool( "SuppressLinefeed", false ) ? 0 : 1;
            var size = (reader.FileHeader.LineLength + inc) * reader.TotalRecords;

            var outStr = new PgpLiteralDataGenerator().Open(
                outputStream, fileType, reader.File.Name, size,
                reader.File.LastWriteTime );

            try
            {
                string line = null;
                while ( reader.HasNextRecord )
                {
                    var record = reader.GetNextRecord();
                    if ( record == null )
                        continue;
                    line = Utils.ByteArrayToString( record );
                    outStr.Write( record, 0, record.Length );
                    // We must write an 0x0A out or the Stratus will
                    // fail to process the file. This is only for SFTP.
                    if ( !configData.GetBool( "SuppressLinefeed", false ) )
                    {
                        outStr.Write( new byte[] { 0x0A }, 0, 1 );
                    }
                }
            }
            finally
            {
                CloseStream( outStr );
            }
        }

        private void TestForNull( object testObject, Error error, string name,
            bool isFullMessage )
        {
            if ( testObject == null )
            {
                string msg = null;
                if ( isFullMessage )
                {
                    msg = name;
                }
                else
                {
                    msg = $"The {name} is null.";
                }
                Logger.Error( msg );
                throw new FilerException( error, msg );
            }
        }
        private void CloseStream( Stream stream )
        {
            try
            {
                if ( stream != null )
                {
                    stream.Close();
                }
            }
            catch { }
        }
        #endregion
    }
}
