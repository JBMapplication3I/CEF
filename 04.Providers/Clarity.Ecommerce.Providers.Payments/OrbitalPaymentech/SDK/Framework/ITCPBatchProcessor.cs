using System;
using System.IO;
using JPMC.MSDK;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Performs all batch processing.
	/// </summary>
	public interface ITCPBatchProcessor
	{
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
		/// Sends the submission to the Stratus. The submission will get converted 
		/// into the proper payload and then transmitted to the Stratus using the 
		/// specified protocol.
		/// </summary>
		/// <param name="submission">The descriptor for the submission to send.</param>
		/// <param name="pgpFileName">The absolute path to the file that the PGP data will be written to.</param>
		void SendSubmission(
			ISubmissionDescriptor submission,
			string pgpFileName);
	
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="password"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		IResponseDescriptor ReceiveResponse(
			IRequest request,
			string password,
			string file );
	}
}
