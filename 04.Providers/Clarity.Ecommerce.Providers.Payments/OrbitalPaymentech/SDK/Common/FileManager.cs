#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Common
{
    using System;
    using System.IO;
    using Configurator;
    using Framework;

    /// <summary>The type of filename.</summary>
    public enum FileType
    {
        /// <summary>
        /// The entire path, including drive, directory path, and filename.
        /// </summary>
        Absolute,
        /// <summary>
        /// The "outgoing" batch directory.
        /// </summary>
        Outgoing,
        /// <summary>
        /// The "incoming" batch directory.
        /// </summary>
        Incoming,
        /// <summary>
        /// A temporary file.
        /// </summary>
        Temporary
    }

    /// <summary>Provides a variety of file handling operations in a convenient interface. Much of FileManager's
    /// functionality simply wraps that of the FileInfo and File classes in order to enable mocking for unit tests.
    /// However, there are several methods that provide more complex file handling features, such as FindFilePath
    /// and FindPathContaining.</summary>
    /// <seealso cref="IFileManager"/>
    public class FileManager : IFileManager
    {
        private readonly object locker = new object();

        private readonly IDispatcherFactory factory;

        private const string installKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{07B44932-EE37-486D-819F-5B10D0D17B25}";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FileManager() : this(new DispatcherFactory())
        {
        }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="factory"></param>
        public FileManager(IDispatcherFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Checks if the specified file exists.
        /// </summary>
        /// <param name="path">The file to test for existence.</param>
        /// <returns>True if the file exists, false if it does not.</returns>
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Moves or renames a file.
        /// </summary>
        /// <param name="from">The source filename.</param>
        /// <param name="to">The destination filename.</param>
        public void Move(string from, string to)
        {
            File.Move(from, to);
        }

        public void Copy(string from, string to)
        {
            File.Copy(from, to);
        }

        /// <inheritdoc/>
        public string FindFilePath(string fileName, string directory)
        {
            return Utils.GetFilePath(
                fileName,
                directory,
                Configurator.Initialized ? Configurator.GetInstance().HomeDirectory : null);
        }

        /// <inheritdoc/>
        public string FindPathContaining(string fileName, string directory)
        {
            var path = FindFilePath(fileName, directory);
            if (path == null)
            {
                return null;
            }
            var fullPath = Path.Combine(directory, fileName);
            if (path == fullPath)
            {
                return directory;
            }
            if (path.EndsWith(fullPath))
            {
                return path.Substring(0, path.LastIndexOf(fullPath, StringComparison.Ordinal) - 1);
            }
            if (path.EndsWith(fileName))
            {
                return path.Substring(0, path.LastIndexOf(fileName, StringComparison.Ordinal) - 1);
            }
            return path;
        }

        /// <inheritdoc/>
        public bool Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                return ex.Message == "File does not exist";
            }
            return true;
        }

        /// <inheritdoc/>
        public bool Delete(FileInfo file)
        {
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {
                return ex.Message == "File does not exist";
            }
            return true;
        }

        /// <inheritdoc/>
        public FileInfo Create(string path, FileType type)
        {
            var fullPath = path;
            switch (type)
            {
                case FileType.Incoming:
                    fullPath = Path.Combine(Path.Combine(factory.HomeDirectory, "Incoming"), path);
                    break;
                case FileType.Outgoing:
                    fullPath = Path.Combine(Path.Combine(factory.HomeDirectory, "Outgoing"), path);
                    break;
            }
            var info = new FileInfo(fullPath);
            info.Create().Close();
            return info;
        }

        /// <summary>
        /// Gets the length of the specified file.
        /// </summary>
        public long Length(string path)
        {
            var info = new FileInfo(path);
            return info.Length;
        }

        /// <inheritdoc/>
        public string CreateTemp(string prefix, FileType type, ConfigurationData configData)
        {
            var retVal = CreateTempName(prefix, type, configData);

            if (retVal != null)
            {
                File.CreateText(retVal).Close();
            }
            return retVal;
        }

        /// <summary>
        /// Get a unique file name but don't create the file
        /// </summary>
        public string CreateTempName(string prefix, FileType type, ConfigurationData configData)
        {
            string retVal;
            if (factory.HomeDirectory.Length == 0)
            {
                factory.HomeDirectory = FindPathContaining("MSDKConfig.xml", "config");
            }
            if (factory.HomeDirectory.Length == 0)
            {
                throw new DispatcherException
                    (Error.InvalidHomeDirectory, "MSDK home could not be found.");
            }
            if (prefix == null)
            {
                prefix = "";
            }
            string before = null;
            switch (type)
            {
                case FileType.Absolute:
                    before = prefix;
                    break;
                case FileType.Outgoing:
                    before = BuildFilePath(configData["OutgoingBatchDirectory"], prefix);
                    break;
                case FileType.Incoming:
                    before = BuildFilePath(configData["IncomingBatchDirectory"], prefix);
                    break;
                case FileType.Temporary:
                    before = Path.GetTempPath();
                    break;
            }
            lock (locker)
            {
                for (var count = 1;; count++)
                {
                    retVal = $"{before}{count}";
                    if (!File.Exists(retVal))
                    {
                        break;
                    }
                }
            }
            return retVal;
        }

        private string BuildFilePath(string dir, string name)
        {
            return Path.Combine(Utils.IsAbsolutePath(dir) ? dir : Path.Combine(factory.HomeDirectory, dir), name);
        }
        /// <summary>
        /// Checks if the given directory path exists on the system.
        /// </summary>
        /// <param name="path">The directory path to check.</param>
        /// <returns>True if the directory exists, false if it does not.</returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
