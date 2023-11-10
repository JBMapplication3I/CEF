using System;

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Summary description for BatchFileType.
	/// </summary>
	public enum BatchFileType 
	{
        /// <summary>
        /// Not set. This should never be used.
        /// </summary>
		Unset = 0,
        /// <summary>
        /// The file is for a batch submission to be sent.
        /// </summary>
		Submission,
        /// <summary>
        /// The file is a response file returned from the server.
        /// </summary>
		Response
	}

}
