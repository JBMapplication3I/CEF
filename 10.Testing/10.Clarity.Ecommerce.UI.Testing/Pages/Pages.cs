namespace ClarityTC.pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    internal class Pages
    {
        protected IWebDriver Driver;
        //protected var driverOpciones;

        public Pages(IWebDriver driver)
        {
            Driver = driver;
        }

        // Get the web driver
        public IWebDriver GetWebDriver()
        {
            return Driver;
        }

        // Wait until a page is loaded completely
        public void WaitUntilPageLoads()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(1000)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            WaitUntilJsReady();
        }

        //Wait untill an element becomes visible.
        public IWebElement WaitToFindElementByXpath(IWebDriver driver, string by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(by)));
            }
            return driver.FindElement(By.XPath(by));
        }

        //ESPERAR HASTA QUE ELEMENTO SE CLICKEABLE
        public IWebElement WaitToBeClickEnableByXpath(IWebDriver driver, string by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(by))); //ElementToBeClickable
            }
            return driver.FindElement(By.XPath(by));
        }

        public List<IWebElement> WaitToAllBeVisbleByXpath(IWebDriver driver, string by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(by))).ToList();
            }
            return driver.FindElements(By.XPath(by)).ToList();
        }

        public bool RunSomeJavascript(string javaScript)
        {
            return (bool)((IJavaScriptExecutor)Driver).ExecuteScript(javaScript);
        }

        public void WaitUntilJsReady()
        {
            var jQScript = "return (window.angular !== undefined) && "
                + "(angular.element(document).injector() !== undefined) && "
                + "(angular.element(document).injector().get('$http').pendingRequests.length === 0)";
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10000)).Until(
                d => RunSomeJavascript(jQScript).Equals(true));
        }

        public void WaitAnimationReady()
        {
            var jQScript = "'return $('div').is(':animated');'";
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10000)).Until(
                d => RunSomeJavascript(jQScript).Equals(true));
        }
    }
}
