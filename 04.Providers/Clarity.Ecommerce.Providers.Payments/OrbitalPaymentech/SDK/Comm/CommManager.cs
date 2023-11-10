#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// Title: Communication Manager
// Description: Handles all communication modules
// Copyright (c)2018, Paymentech, LLC. All rights reserved
// Company: J. P. Morgan
// @author Frank McCanna
// @version 3.0
namespace JPMC.MSDK.Comm
{
    using System.Collections.Concurrent;
    using System.Threading;
    using JPMC.MSDK.Common;
    using JPMC.MSDK.Configurator;
    using log4net;

    /// <summary>
    /// Single entry point for all communication modules
    /// </summary>
    public class CommManager : ICommManager
    {
        // The common eCommerce log for the MSDK
        private static ILog logger;

        public static ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new LoggingWrapper().EngineLogger;
                }
                return logger;
            }
            set => logger = value;
        }

        /**
         * This map is for special comm modules that have had their default
         * configuration values changed.
         */
        private SafeDictionary<string, ConcurrentQueue<ICommModule>> commObjectLists =
            new SafeDictionary<string, ConcurrentQueue<ICommModule>>();

        private object SynchObj = new object();

        private const int MAX_LIST_SIZE = 100;

        /**
         * null Constructor makes sure members are initialized
         * @throws CommException
         */
        public CommManager()
        {
            CommBase.Logger = Logger;
            Logger.Debug("********** Constructed a new CommManager Object **********");
        }

        /**
         * This gets the specified comm object and also creates one if
         * it doesn't exist and "createIfMissing" is true
         * @param which
         * @param cdata
         * @param createIfMissing
         * @return
         * @throws CommException
         */
        public ICommModule GetCommObject(ConfigurationData cdata,
            bool createIfMissing)
        {
            var which = cdata.ConfigName;

            ICommModule module = null;

            if (which == null)
            {
                return null;
            }

            ConcurrentQueue<ICommModule> which_list = null;

            // Got to lock so we don't end up trying to add 2
            lock (SynchObj)
            {
                which_list = commObjectLists[which];

                if (which_list == null)
                {
                    // make a new list for this config name
                    which_list = new ConcurrentQueue<ICommModule>();

                    commObjectLists.Add(which, which_list);
                }
            }

            foreach (var member in which_list)
            {
                // the Equals method is optimized to only compare altered fields
                if (member.Config.Equals(cdata))
                {
                    module = member;
                    break;
                }
            }

            // if we couldn't find one in the list
            if (module == null && createIfMissing)
            {
                module = CreateModule(cdata);

                if (module != null)
                {
                    which_list.Enqueue(module);

                    // check to see if the list hit the maximum size
                    // if so, delete the first element of the list
                    // keep doing this until we get to the max size
                    // we can get several above max if TryDequeue returns false
                    // don't worry about threads that are currently using the
                    // removed ones, remember that removing the modules from the
                    // queue does not close them, it just prevents them from being
                    // given to a new thread. The modules will still function until
                    // all references to them disappear and then they will be gc'd
                    ICommModule victim = null;
                    while (which_list.Count > MAX_LIST_SIZE &&
                        which_list.TryDequeue(out victim) == true)
                    {
                        victim = null;
                        Logger.Warn("Maximum size for communications module list reached");
                    }
                }
            }

            return module;
        }

        /**
         * complete one transaction
         * @param args CommArgs
         * @return CommArgs
         * @throws CommException
         */
        public CommArgs CompleteTransaction(CommArgs args)
        {
            CommArgs retVal = null;
            var cdata = args.Config;
            ICommModule commObject = null;
            commObject = GetCommObject(cdata, true);
            if (commObject == null)
            {
                var str = "Unknown module type: ";
                str += cdata.Protocol.ToString();
                throw new CommException(Error.ModuleUnavailable, str);
            }
            retVal = commObject.CompleteTransaction(args);
            return retVal;
        }

        /**
         * Close all comm objects.
         */
        public void Close()
        {
            if (commObjectLists == null)
            {
                return;
            }
            foreach (var entry in commObjectLists)
            {
                var moduleList = entry.Value;
                foreach (var each in moduleList)
                {
                    if (each is TCPConnect ||
                        each is PNSConnect)
                    {
                        each.Close();
                    }
                }
            }
        }

        /// <summary>
        /// close a specific "non-upload" comm module
        /// </summary>
        /// <param name="cdata"></param>
        public void Close(ConfigurationData cdata)
        {
            ICommModule commObject = null;

            try
            {
                commObject = GetCommObject(cdata, false);
            }
            catch (CommException e)
            {
                // ignore errors because close doesn't need to succeed if not in map
                Logger.Debug(e.StackTrace);
            }

            if (commObject != null)
            {
                commObject.Close();
            }
        }

        /// <summary>
        ///  close specific comm modules that have a special close method that takes a batch ID
        /// </summary>
        /// <param name="batchUID"></param>
        /// <param name="cdata"></param>
        public void Close(string batchUID, ConfigurationData cdata)
        {
            ICommModule commObject = null;
            var whichModule = cdata.ConfigName;
            var batchID = batchUID + Thread.CurrentThread.ManagedThreadId;

            try
            {
                commObject = GetCommObject(cdata, false);
            }
            catch (CommException)
            {
                // ignore but log
                Logger.Warn("could not find comm object with config name: " + whichModule +
                        " while trying to close it");
            }

            if (commObject != null)
            {
                try
                {
                    // if the caller specified a batch ID then they want the
                    // special closes implemented in the following
                    if (batchUID != null)
                    {
                        if (commObject is HTTPSUpload)
                        {
                            var conn = (HTTPSUpload)commObject;
                            conn.Close(batchID);
                        }
                        else if (commObject is PNSUpload)
                        {
                            var conn = (PNSUpload)commObject;
                            conn.Close(batchID);
                        }
                    }
                    else
                    {
                        commObject.Close();
                    }

                }
                catch (CommException ex)
                {
                    // ignore but log
                    Logger.Info("Failure during close of comm module" + ex);
                }
                // don't delete the module references because another thread
                // might be using them
            }
            else
            {
                Logger.Warn("could not find comm object with config name: " + whichModule +
                    " while trying to close it");
            }
        }

        /**
         * Called automatically by the garbage collector
         * @throws Throwable
         */
        ~CommManager()
        {
            Close();
        }

        /**
         * Create a comm module of the type specified
         * @param which
         * @param cdata
         * @return ICommModule
         * @throws CommException
         */
        private ICommModule CreateModule(ConfigurationData cdata)
        {
            var which = cdata.Protocol;
            ICommModule retVal = null;

            try
            {
                // create the one specified by the caller
                switch (which)
                {
                    case CommModule.TCPConnect:
                        retVal = new TCPConnect(cdata);
                        break;
                    case CommModule.TCPBatch:
                        retVal = new TCPBatch(cdata);
                        break;
                    case CommModule.SFTPBatch:
                        retVal = new SFTPBatch(cdata);
                        break;
                    case CommModule.PNSConnect:
                        retVal = new PNSConnect(cdata);
                        break;
                    case CommModule.PNSUpload:
                        retVal = new PNSUpload(cdata);
                        break;
                    case CommModule.HTTPSConnect:
                        retVal = new HTTPSConnect(cdata);
                        break;
                    case CommModule.HTTPSUpload:
                        retVal = new HTTPSUpload(cdata);
                        break;
                    case CommModule.Unknown:
                    default:
                    {
                        throw new CommException(Error.ArgumentMismatch,
                            "Module: " + which + "not supported");
                    }
                }
            }
            catch (ConfiguratorException e)
            {
                throw new CommException(Error.InitializationFailure,
                        e.Message, e);
            }

            Logger.Debug("CommManager created a comm module of type "
                + which + " with the following configuration values:\n"
                + cdata.DumpFieldValues(true));

            return retVal;
        }
    }
}
