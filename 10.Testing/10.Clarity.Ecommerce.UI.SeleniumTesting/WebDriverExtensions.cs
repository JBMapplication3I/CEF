// <copyright file="WebDriverExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the web driver extensions class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public static class WebDriverExtensions
    {
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInMS)
        {
            return timeoutInMS <= 0
                ? driver.FindElement(by)
                : new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMS))
                    .Until(drv => drv.FindElement(by));
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static IReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInMS)
        {
            return timeoutInMS <= 0
                ? driver.FindElements(by)
                : new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMS))
                    .Until(drv => drv.FindElements(by));
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static bool WaitFor(this IWebDriver driver, Func<IWebDriver, bool> condition, int timeoutInMS)
        {
            return new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMS)).Until(condition);
        }

        public static void WaitForClickable(this IWebDriver driver, string id, int timeoutInMS)
        {
            new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMS)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(id)));
        }

        public static void WaitForVisible(this IWebDriver driver, string id, int timeoutInMS)
        {
            new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMS)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)));
        }
    }
}
