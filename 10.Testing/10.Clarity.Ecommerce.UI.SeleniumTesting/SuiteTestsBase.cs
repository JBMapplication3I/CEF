// <copyright file="SuiteTestsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the suite tests base class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Xunit;

    [Trait("Category", "UI Testing with Selenium")]
    public abstract class SuiteTestsBase : IDisposable
    {
        protected static readonly string WebsiteUrl = ConfigurationManager.AppSettings["Clarity.Routes.Site.RootUrl"];
        protected static readonly string Username = ConfigurationManager.AppSettings["Clarity.Testing.Username"];
        protected static readonly string Password = ConfigurationManager.AppSettings["Clarity.Testing.Password"];
        protected static readonly int FindElementWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.FindElementWaitInMS"]);
        protected static readonly int FirstPageLoadWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.FirstPageLoadWaitInMS"]);
        protected static readonly int LoginWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.LoginWaitInMS"]);
        protected static readonly int PageLoadWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.PageLoadWaitInMS"]);
        protected static readonly int ModalOpenWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.ModalOpenWaitInMS"]);
        protected static readonly int MenuOpenWaitInMS = int.Parse(ConfigurationManager.AppSettings["Clarity.Testing.Waits.MenuOpenWaitInMS"]);

        [DebuggerNonUserCode, DebuggerStepThrough]
        protected SuiteTestsBase()
        {
            Driver = new ChromeDriver();
            JS = (IJavaScriptExecutor)Driver;
            Vars = new Dictionary<string, object>();
        }

        public IWebDriver Driver { get; }

        public IDictionary<string, object> Vars { get; }

        public IJavaScriptExecutor JS { get; }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
