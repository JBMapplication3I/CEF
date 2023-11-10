using System;
using System.IO;
using JPMC.MSDK;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Performs all batch processing.
	/// </summary>
	public interface ISFTPBatchProcessor
	{
		/// <summary>
		/// Creates a Response object that contains the fields of the 
		/// NetConnect error response file referenced by the given
		/// filename.
		/// </summary>
		/// <remarks>
		/// The file referenced by the FileInfo object is a PGP encrypted
		/// error response file that contains fields that describe the 
		/// nature of the error.
		/// 
		/// A typical file looks like this:
		/// 
		/// The error response file looks something like this:
		/// 
		/// Batch Sequence Number: [16601]
		/// User ID: [SVQ4HDW5]
		/// File ID: [945903.071112_120348.txt.pgp]
		/// File Direction: [Merchant to Stratus]
		/// ----------------------------------------
		/// 6826: Incorrect compression type for this user
		/// 
		/// This method takes each line that contains the delimiter (":")
		/// and splits into a name/value pair. It then removes the brackets from the
		/// value and puts the pair into a map. Lines that do not contain the 
		/// delimiter are skipped.
		/// 
		/// The map is then passed to the converter, which gives me XML that is  
		/// then plugged into a new Response object to return to the client.
		/// </remarks>
		/// <param name="manager">The SecurityManager object that contains the keys necessary to decrypt the file.</param>
		/// <param name="pgpFile">The absolute filename of the error file. </param>
		/// <param name="sftpFileName">The name of the file on the SFTP server.</param>
		/// <returns>A Response object giving easy access to the error file's fields.</returns>    
		IResponse CreateNetConnectError(
            ConfigurationData configData, 
			string pgpFile,
			string sftpFileName);

		/// <summary>
		/// Converts an existing PGP response file into an AES response file 
		/// and returns a ResponseDescriptor to allow access to the AES file.
		/// </summary>
		/// <param name="responseFile">The path to the PGP file that must be converted.</param>
		/// <param name="aesFile">The path to the AES file that the response file will be converted to.</param>
		/// <param name="manager">The SecurityManager object that contains the keys necessary to convert the file.</param>
		/// <returns>An IResponseDescriptor that provides access to the new AES encrypted file.</returns>
		IResponseDescriptor CreateResponseDescriptor(
			string responseFile, 
			string aesFile,
            ConfigurationData configData );
        
		/// <summary>
		/// Deletes the specified file from the Stratus.
		/// </summary>
		/// <remarks>
		///  The file specified does not
		/// represent a file on the client's computer, but instead refers to a file
		/// on the server. This method will properly delete the file
		/// from all servers (including backup servers). 
		/// 
		/// The method returns true if the file was successfully deleted from all 
		/// servers. If it failed to delete it from even just one server, the method
		/// will return false. You can call this method as many times as it takes to
		/// delete all of the files. However, you may want to wait or call support 
		/// if it fails to delete them all the first time.
		/// </remarks>
		/// <param name="filename">The name of the file to delete from the server.</param>
		/// <param name="manager">An object containing the security data required for the operation.</param>
		/// <returns>True if the file was successfully deleted, false if not.</returns>
		bool DeleteServerFile(
			string filename,
            ConfigurationData configData );

		/// <summary>
		/// Sends the submission to the Stratus. The submission will get converted 
		/// into the proper payload and then transmitted to the Stratus using the 
		/// specified protocol.
		/// </summary>
		/// <param name="submission">The descriptor for the submission to send.</param>
		/// <param name="pgpFileName">The absolute path to the file that the PGP data will be written to.</param>
		void SendSubmission( ISubmissionDescriptor submission, string pgpFileName );

        /// <summary>
        /// Sends the submission to the Stratus. The submission is already in PGP format, 
        /// so it will just be transmitted to the Stratus using the specified protocol.
        /// </summary>
        /// <param name="configData">ConfigurationData to use for sending the file.</param>
        /// <param name="pgpFileName">The absolute path to the file that the PGP data will be written to.</param>
        void SendSubmission( ConfigurationData configData, string pgpFileName );
        
        /// <summary>
		/// Calls ICommManager's ReceiveFile method to download the PGP response 
		/// file, and then converts it into an AES response file.
		/// </summary>
		/// <remarks>
		/// The CommManager will not use the FileWriter that ReceiveResponse 
		/// creates to write to the file. Instead, it just uses it to get
		/// the filename, SecurityManager, and ResponseType. So, this method
		/// must not call IFileWriter.CreateFile and does not need to call
		/// Close. If the file is created, then the CommManager would not 
		/// be able to write to the file because the writer will already
		/// have it locked.
		/// </remarks>
		/// <param name="responseType">The type of response to being downloaded.</param>
		/// <param name="manager">
		/// The SecurityManager that defines the SSH and PGP security 
		/// parameters.
		/// </param>
		/// <param name="pgpFile">The path to the PGP file that the response 
		/// file is downloaded to.
		/// </param>
		/// <param name="aesFile">The path to the AES file that the PGP file 
		/// will be converted into. This will be the actual response file that 
		/// is wrapped by the ResponseDescriptor.
		/// </param>
		/// <returns>
		/// A ResponseDescriptor that wraps the new AES-encrypted response 
		/// file.
		/// .</returns>
		IResponseDescriptor ReceiveResponse(
			string responseType,
            ConfigurationData configData, 
			string pgpFile, 
			string aesFile);

		/// <summary>
		/// Not supported for SFTP.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="password"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		IResponseDescriptor ReceiveResponse(
			IRequest request, 
			string password, 
			string file);

		/// <summary>
		/// Downloads a PGP-encrypted response file from the server and 
		/// returns the file path to it. 
		/// </summary>
		/// <remarks>
		/// You cannot iterate through the response file downloaded using this 
		/// method. You must first convert it to a proper AES-encrypted file 
		/// and get an IResponseDescriptor object for it. 
		/// 
		/// This method is optional and is only used if you want to split the
		/// two time-consuming tasks of downloading and converting to AES into
		/// two separate tasks. 
		/// </remarks>
		/// <param name="responseType">The type of response you wish to download.</param>
		/// <param name="manager">The SecurityManager object that contains the keys 
		/// necessary to download the PGP file.</param>
		/// <param name="pgpFile">The PGP file that will be downloaded from the server.</param>
		/// <param name="sftpFilename">The name of the file as it exists on the server.</param>
		/// <returns></returns>
		string ReceivePGPResponse(
			string responseType,
			ConfigurationData configData, 
			string pgpFile, out string sftpFilename);

		/// <summary>
		/// Converts an existing AES-encrypted submission file into a PGP-
		/// encrypted submission file. 
		/// </summary>
		/// <remarks>
		/// This is to allow the client to perform the potentially time 
		/// consuming task of generating the PGP file ahead of time before
		/// sending the submission. 
		/// </remarks>
		/// <param name="submission">The AES submission file to convert.</param>
		/// <param name="pgpFileName">The absolute path to the PGP file that is to be created.</param>
		/// <returns>The absolute path to the generated PGP file.</returns>
		string CreatePGPSubmissionFile(
			ISubmissionDescriptor submission,  
			string pgpFileName);
	}
}
