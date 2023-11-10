// <copyright file="Conditions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conditions class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System;
    using System.Diagnostics;
    using OpenQA.Selenium;

    public static class Conditions
    {
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static Func<IWebDriver, bool> ElementIsVisibleAndEnabled(By by)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(by);
                    return element.Displayed && element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
                catch (NoSuchElementException)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static Func<IWebDriver, bool> ElementIsVisibleAndEnabled(IWebElement element)
        {
            return driver =>
            {
                try
                {
                    return element.Displayed && element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
                catch (NoSuchElementException)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }
    }
}
