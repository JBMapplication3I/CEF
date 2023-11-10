// <copyright file="Global.asax.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the global.asax class</summary>
namespace Clarity.Ecommerce.Service.Reporting
{
    using System;
    using System.Diagnostics;
    using DevExpress.XtraReports.Web.Extensions;
    using JetBrains.Annotations;

    [PublicAPI]
    public class Global : System.Web.HttpApplication
    {
        public static ReportStorageWebExtension ReportStorage { get; private set; }

        private void Application_Start(object sender, EventArgs e)
        {
            DevExpress.XtraReports.Web.Native.ClientControls.Services.DefaultLoggingService.SetInstance(new MyLoggingService());
            DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;
            // Report Storage
            ReportStorage = new ClarityReportStorageWebExtension();
            ReportStorageWebExtension.RegisterExtensionGlobal(ReportStorage);
        }

        private void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Debug.WriteLine(e);
        }

        private void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        private void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Note: The Session_End event is raised only when the sessionState mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer
            // or SQLServer, the event is not raised.
        }
    }

    public class MyLoggingService : DevExpress.XtraReports.Web.Native.ClientControls.Services.ILoggingService
    {
        private ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        public void Error(string message)
        {
            Logger.LogError("DevExpress.XtraReports.Error", message, contextProfileName: null);
        }

        public void Info(string message)
        {
            Logger.LogInformation("DevExpress.XtraReports.Info", message, contextProfileName: null);
        }
    }
}
