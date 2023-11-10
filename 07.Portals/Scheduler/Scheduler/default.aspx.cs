// <copyright file="default.aspx.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default.aspx class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;
    using System.Configuration;

    /// <content>A default.</content>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>Gets the CEF schedule root.</summary>
        /// <value>The cef schedule root.</value>
        // ReSharper disable once InconsistentNaming, StyleCop.SA1401
        protected string CefScheduleRoot { get; private set; } = null!;

        /// <summary>Event handler. Called by Page for load events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // HangfireBootstrapper.Instance.Start(Startup.App, "Default.Page_Load");
            CefScheduleRoot = ConfigurationManager.AppSettings["Clarity.Files.Scheduler.Root"]
                ?? "/DesktopModules/ClarityEcommerce/Scheduler/Dashboard";
        }
    }
}
