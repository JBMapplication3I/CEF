#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.IO;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Common
{
    /// <summary>
    /// This interface wraps various file related methods.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Checks if the specified file exists.
        /// </summary>
        /// <param name="path">The file to test for existence.</param>
        /// <returns>True if the file exists, false if it does not.</returns>
        bool Exists( string path );

        /// <summary>
        /// Gets the length of the specified file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        long Length( string path );
        /// <summary>
        /// Searches for the supplied filename in the specified directory.
        /// It will ignore the directory parameter if it is null. It searches
        /// for the file in the following manner:
        ///
        /// <list type="number">
        /// <item>
        ///		<description>In .\directory\fileName</description>
        /// </item>
        /// <item>
        ///		<description>The MSDK_HOME environment variable</description>
        /// </item>
        /// </list>
        /// <item>
        ///		<description>The InstallLocation field in the SDK's uninstall Registry key.</description>
        /// </item>
        /// <item>
        ///		<description>The same directory that the calling assembly is in.</description>
        /// </item>
        /// <item>
        ///		<description>One directory back from the calling assembly's directory.</description>
        /// </item>
        /// </summary>
        /// <param name="fileName">The name of the file to find.</param>
        /// <param name="directory">The subdirectory that the file is in. Null, if no subdirectory is to be searched.</param>
        /// <returns>The absolute path to the file if found, null if not found.</returns>
        string FindFilePath( string fileName, string directory );
        /// <summary>
        /// Finds the file specified in the specified directory, but will
        /// only return the absolute path to the directory that contains
        /// the directory/file combination specified.
        /// </summary>
        /// <remarks>
        /// This method is used to get the directory path that contains the
        /// supplied directory (or the supplied file if the directory parameter
        /// is null). It will not return the path to the file itself.
        /// </remarks>
        /// <example>
        /// You can use this method to find where the SDK was installed to:
        ///
        /// <code>
        /// string homeDir = manager.FindPathContaining("MSDKConfig.xml", "config");
        /// </code>
        ///
        /// In the above example, FindPathContaining will search for a directory that
        /// has config/MSDKConfig.xml in it, but will only return the path to that directory.
        /// So, for the path:
        ///
        ///	<code>C:\MSDK\config\MSDKConfig.xml</code>
        ///
        ///	the method will return:
        ///
        ///	<code>C:\MSDK</code>
        /// </example>
        ///
        /// <param name="fileName">The name of the file to find.</param>
        /// <param name="directory">The subdirectory that the file is in, or null if no subdirectory is to be searched.</param>
        /// <returns>The absolute path to the directory containing the above directory/file combination, or null if not found.</returns>
        string FindPathContaining( string fileName, string directory );
        /// <summary>
        /// Delete the specified file.
        /// </summary>
        /// <remarks>
        /// Delete will return true if the file does not exist.
        /// Since the goal is to ensure that the file does not exist, this
        /// is treated as success.
        /// </remarks>
        /// <param name="path">The file to delete.</param>
        /// <returns>True if the file was deleted successfully, false if it was not.</returns>
        bool Delete( string path );
        /// <summary>
        /// Delete the specified file.
        /// </summary>
        /// <remarks>
        /// Delete will return true if the file does not exist.
        /// Since the goal is to ensure that the file does not exist, this
        /// is treated as success.
        /// </remarks>
        /// <param name="file">The file to delete.</param>
        /// <returns>True if the file was deleted successfully, false if it was not.</returns>
        bool Delete( FileInfo file );
        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="path">The file to create.</param>
        /// <param name="type">The type of file to create.</param>
        /// <returns></returns>
        FileInfo Create( string path, FileType type );
        /// <summary>
        /// Creates a temporary file and returns a FileInfo object that
        /// references it.
        /// </summary>
        /// <remarks>
        /// It creates the file name differently depending on the FileType
        /// used. Here are the different types and their functionality:
        ///
        /// Incoming: Creates a filename whose name begins with the
        /// supplied prefix followed by a unique integer and has the
        /// extension ".response". The filename's path will be a
        /// subdirectory called "incoming" off the SDK Home path.
        ///
        /// Outgoing: Creates a filename whose name begins with the
        /// supplied prefix followed by a unique integer and has no
        /// file extension. The filename's path will be a subdirectory
        /// called "outgoing" off the SDK Home path. An extension
        /// can be given by supplying it as part of the prefix, such
        /// as
        ///
        /// <code>CreateTemp("myfile.temp", FileType.Outgoing);</code>
        ///
        /// Absolute: Creates a filename whose name begins with the
        /// supplied prefix followed by a unique integer. The filename's
        /// path must be supplied as part of the prefix. Therefore,
        /// CreateTemp will not attempt to determine the absolute path
        /// because it assumes it is included in the prefix. For example,
        ///
        /// <code>
        /// CreateTemp(@"C:\MSDK\myfile.temp", FileType.Outgoing);
        /// </code>
        ///
        /// Temporary: Creates a temporary file in the current user's
        /// temp directory. CreateTemp will use C#'s algorithm for
        /// generating the filename and will ignore the prefix parameter.
        /// </remarks>
        /// <param name="prefix">The prefix that the filename will have.</param>
        /// <param name="type">Specifies how and where to create the file name.</param>
        /// <param name="configData">To specify incoming/outgoing directories.</param>
        /// <returns>A FileInfo object that references the new filename.</returns>
        string CreateTemp( string prefix, FileType type, ConfigurationData configData );

        /// <summary>
        /// Just create the unique temporary file name, do not actually create the file
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="type"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        string CreateTempName( string prefix, FileType type, ConfigurationData configData );
        /// <summary>
        /// Returns true if the specified directory exists, false if it does not.
        /// </summary>
        /// <param name="path">The directory to find.</param>
        /// <returns>True if the specified directory exists, false if it does not.</returns>
        bool DirectoryExists( string path );
        /// <summary>
        /// Moves or renames a file.
        /// </summary>
        /// <param name="from">The source filename.</param>
        /// <param name="to">The destination filename.</param>
        void Move( string from, string to );

        void Copy( string from, string to );
    }
}
