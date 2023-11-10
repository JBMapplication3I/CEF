namespace JPMC.MSDK.Framework
{
    using System;
    using System.Globalization;
    using System.IO;
    using Common;
    using Configurator;

    /// <exclude />
    /// <summary>
    /// Summary description for FrameworkBase.
    /// </summary>
    public abstract class FrameworkBase
    {
        /// <exclude />
        /// <summary>
        ///
        /// </summary>
        protected IDispatcherFactory factory;
        /// <exclude />
        /// <summary>
        ///
        /// </summary>
        protected IFileManager fileMgr;

        /// <exclude />
        /// <summary>
        ///
        /// </summary>
        public FrameworkBase()
        {
        }

        /// <exclude />
        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected log4net.ILog Logger => factory.Logger;

        /// <exclude />
        /// <summary>
        /// Gets the Config Manager.
        /// </summary>
        protected IConfigurator Configurator => factory.Config;

        /// <exclude />
        /// <summary>
        /// Gets the File manager
        /// </summary>
        protected IFileManager FileMgr
        {
            get
            {
                if (fileMgr == null)
                {
                    fileMgr = factory.MakeFileManager();
                }
                return fileMgr;
            }
        }

        /// <exclude />
        /// <summary>
        /// Gets the path to a file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        protected string GetFileName(string name, string filename, string directory)
        {
            if (filename != null && Utils.IsAbsolutePath(filename))
            {
                return !FileMgr.Exists(filename) ? null : filename;
            }

            var fname = directory;
            if (!Utils.IsAbsolutePath(fname))
            {
                fname = FileMgr.FindFilePath(name, fname);
            }
            else
            {
                fname = $"{fname}\\{name}";
                if (!FileMgr.Exists(fname))
                {
                    fname = null;
                }
            }
            return fname;
        }

        /// <summary>
        /// Generates a unique filename based on the supplied filename.
        /// </summary>
        /// <remarks>
        /// To make the filename unique, it appends a number in parentheses to
        /// the filename (before extension). Example: "mybatch(1).response".
        /// </remarks>
        /// <param name="filename">The filename to base the unique one on.</param>
        /// <returns></returns>
        protected string GetUniqueFileName(string filename)
        {
            var finalName = filename;
            var fname = new FileName(filename);
            var baseName = Path.GetFileNameWithoutExtension(filename);

            for (var index = 1; FileMgr.Exists(finalName); index++)
            {
                fname.Name = $"{baseName}({index})";
                finalName = fname.ToString();
            }

            return finalName;
        }

        /// <summary>
        /// Renames the specified response file.
        /// </summary>
        /// <remarks>
        /// This method is called by both the TCPBatchProcessor and the
        /// PGPFileConverter.
        /// </remarks>
        /// <param name="responseType"></param>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        protected string RenameResponseFile(string responseType, string filename, string password, ConfigurationData configData)
        {
            if (!FileMgr.Exists(filename))
            {
                return null;
            }

            // Rename the file and create the ResponseDescriptor.
            var fname = new FileName(filename);
            string finalName = null;
            if (responseType == ResponseType.Response)
            {
                var response = factory.MakeResponseDescriptor(filename, password, configData, null);
                fname.Name = response.Header["FileName"];
                response.Close();
            }
            else if (responseType == ResponseType.DelimitedFileReport)
            {
                var dateTime = DateTime.Now.ToString("MM-dd-yy hh.mm.ss", DateTimeFormatInfo.InvariantInfo);
                var tempfilename = string.Concat(responseType + "_", dateTime).Replace(" ", "_");
                IResponseDescriptorImpl response = null;
                try
                {
                    // Get the file naem from the DFR header
                    response = factory.MakeResponseDescriptor(filename, password, configData, null);
                    var resp = response.GetNextParsedRecord();
                    var pid = resp["PID"];
                    var entType = resp["EntityType"];
                    var entNum = resp["EntityNumber"];
                    var frq = resp["Frequency"];
                    fname.Name = responseType + "_" + pid + "_" + entType + "_" + entNum + "_" + frq + "_" + dateTime.Replace(" ", "_");
                    response.Close();
                }
                catch (Exception e)
                {
                    // somthing wrong, used the old naming style
                    if (response != null)
                    {
                        response.Close();
                    }
                    Logger.Warn("Error while renaming the delimited report");
                    Logger.Warn(e.ToString());
                    fname.Name = tempfilename;
                }
            }
            else
            {
                var dateTime = DateTime.Now.ToString("MM-dd-yy hh.mm.ss", DateTimeFormatInfo.InvariantInfo);
                fname.Name = string.Concat(responseType + "_", dateTime).Replace(" ", "_");
            }

            finalName = !fname.ToString().EndsWith(".response") ? fname + ".response" : fname.ToString();
            finalName = GetUniqueFileName(finalName);
            try
            {
                FileMgr.Move(filename, finalName);
            }
            catch (Exception ex)
            {
                Logger.Warn("While moving file: " + filename + " to: " + finalName +
                    "Got Exception: " + ex.GetType().Name
                    + " with message: " + ex.Message, ex);
            }

            if (!FileMgr.Exists(finalName))
            {
                Logger.WarnFormat("Failed to rename file \"{0}\" to \"{1}\".", filename, finalName);
                finalName = filename;
            }

            return finalName;
        }

        /// <summary>
        /// Tests a string to see if it's either null or empty.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected bool IsEmpty(string text)
        {
            return text == null || text.Length == 0;
        }

        /// <summary>
        /// This method gets the current date and returns it to the caller as
        /// a string.
        /// </summary>
        /// <remarks>
        /// This method is called by CreateBatch and CloseBatch
        /// </remarks>
        /// <returns>The date as a string in yyMMdd format.</returns>
        protected static string GetCurrentDateString()
        {
            var dt = DateTime.Now;
            return dt.ToString("yyMMdd");
        }
    }
}
