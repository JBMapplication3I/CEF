// <copyright file="DriverActions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the driver actions class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System.Diagnostics;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public static class DriverActions
    {
        [DebuggerNonUserCode, DebuggerStepThrough]
        public static IWebElement MouseOverElement(this IWebDriver driver, By by, int timeoutInMS)
        {
            return driver.MouseOverElement(driver.FindElement(by, timeoutInMS));
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static IWebElement MouseOverElement(this IWebDriver driver, IWebElement element)
        {
            new Actions(driver).MoveToElement(element, 5, 5).Perform();
            return element;
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void OpenAndPickOptionFromSelect(this IWebDriver driver, By select, By option, int timeoutInMS, int timeoutForOpenSelectInMS)
        {
            var selectElement = driver.FindElement(select, timeoutInMS);
            selectElement.Click(); // Open the dropdown
            driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(option), timeoutForOpenSelectInMS);
            var optionElement = driver.FindElement(option, timeoutInMS);
            optionElement.Click(); // Pick the option
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void ScrollToElement(this IWebDriver driver, IJavaScriptExecutor js, By by, int timeoutInMS)
        {
            js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(by, timeoutInMS));
        }
    }
}
