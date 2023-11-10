// <copyright file="Global.asax.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the global.asax class</summary>
namespace Clarity.Ecommerce.Scheduler
{
    using System;

    /// <summary>A global.</summary>
    /// <seealso cref="System.Web.HttpApplication"/>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>Event handler. Called by Application for start events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // HangfireBootstrapper.Instance.Start(Startup.App, "Global.Application_Start");
        }

        /// <summary>Event handler. Called by Session for start events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            // HangfireBootstrapper.Instance.Start(Startup.App, "Global.Session_Start");
        }

        /// <summary>Event handler. Called by Session for end events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>Event handler. Called by Application for end events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }

        /// <summary>Event handler. Called by Application for begin request events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        /// <summary>Event handler. Called by Application for authenticate request events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>Event handler. Called by Application for error events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Event information.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
        }
    }
}
