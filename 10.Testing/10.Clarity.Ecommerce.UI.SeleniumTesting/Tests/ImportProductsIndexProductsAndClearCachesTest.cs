// <copyright file="ImportProductsIndexProductsAndClearCachesTest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import products index products and clear caches test class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System;
    using OpenQA.Selenium;
    using Xunit;

    public partial class SuiteTests : SuiteTestsBase
    {
        private void ImportProductsIndexProductsAndClearCaches()
        {
            // Test name: import products, index products and clear caches
            // Step # | name | target | value
            // 1 | setWindowSize | 1920x1080 |
            Driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            // 2 | open | /Admin/Clarity-Ecommerce-Admin |
            var normalTimeout1 = Driver.Manage().Timeouts().AsynchronousJavaScript;
            var normalTimeout2 = Driver.Manage().Timeouts().ImplicitWait;
            var normalTimeout3 = Driver.Manage().Timeouts().PageLoad;
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(5);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(5);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);
            Driver.Navigate().GoToUrl(WebsiteUrl + "/Admin/Clarity-Ecommerce-Admin");
            // 3 | click | id=txtLoginUsername |
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("txtLoginUsername")), FirstPageLoadWaitInMS);
            Driver.FindElement(By.Id("txtLoginUsername")).Click();
            // 4 | type | id=txtLoginUsername | clarity
            Driver.FindElement(By.Id("txtLoginUsername")).SendKeys("clarity");
            // 5 | click | id=pwLoginPassword |
            Driver.FindElement(By.Id("pwLoginPassword")).Click();
            // 6 | type | id=pwLoginPassword | QAZXSWwsxzaq!@#$4321
            Driver.FindElement(By.Id("pwLoginPassword")).SendKeys("QAZXSWwsxzaq!@#$4321");
            // 7 | click | id=btnSignIn |
            Driver.FindElement(By.Id("btnSignIn")).Click();
            // 8 | click | id=btninventory.products.list |
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("btninventoryproductslist")),
                LoginWaitInMS);
            Driver.FindElement(By.Id("btninventoryproductslist")).Click();
            // 9 | click | css=.btn-sm:nth-child(2) > span |
            Driver.FindElement(By.CssSelector(".btn-sm:nth-child(2) > span")).Click();
            // 11 | type | name=Images | C:\CEF-Configs\CEF-2020.1-Testing\Ron-product-import-v9.xlsx
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.CssSelector(".k-upload")),
                PageLoadWaitInMS);
            Driver.FindElement(By.Name("Images")).SendKeys("C:\\CEF-Configs\\2021.1\\Testing\\CEF\\Ron-product-import-v9.xlsx");
            // 12 | click | id=btnImport |
            Driver.FindElement(By.Id("btnImport")).Click();
            // 13 | click | id=btnConfirmModalOk |
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("btnConfirmModalOk")),
                PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnConfirmModalOk")).Click();
            // 14 | assertText | css=.inline-block | Importing, please wait...
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("lbImporting")),
                PageLoadWaitInMS);
            Assert.Equal("Importing, please wait...", Driver.FindElement(By.Id("lbImporting")).Text);
            // 15 | assertText | id=lbNoErrors | There were no errors uploading the products.
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("lbNoErrors")),
                FirstPageLoadWaitInMS * 8);
            Assert.Equal("There were no errors uploading the products.", Driver.FindElement(By.Id("lbNoErrors")).Text);
            // 16 | click | css=.btn > .ng-scope |
            Driver.FindElement(By.Id("btnViewProducts")).Click();
            // 17 | click | id=ddlBoolFilter11_Active |
            Driver.FindElement(By.Id("ddlBoolFilter11_Active")).Click();
            // 18 | select | id=ddlBoolFilter11_Active | label=Yes
            Driver.OpenAndPickOptionFromSelect(
                By.Id("ddlBoolFilter11_Active"),
                ////By.XPath("//option[.='ASCOT: 1321 Ascot Street']"), // Alt selectors: css=option:nth-child(2) __ xpath=/option[2]
                By.CssSelector("#ddlBoolFilter11_Active > option:nth-child(2)"),
                FindElementWaitInMS,
                PageLoadWaitInMS);
            // 19 | click | id=ddlBoolFilter11_Active |
            Driver.FindElement(By.Id("ddlBoolFilter11_Active")).Click();
            // 20 | click | css=.btn-info > .ng-scope |
            Driver.FindElement(By.Id("btnGridFilter")).Click();
            //// // 21 | assertText | linkText=5 | 5
            //// Assert.Equal("5", Driver.FindElement(By.LinkText("5")).Text);
            // 22 | click | css=.btn:nth-child(6) > span |
            Driver.FindElement(By.CssSelector(".btn:nth-child(6) > span")).Click();
            // 23 | assertText | id=messageModalLabel | Your product index has been successfully updated
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("messageModalLabel")),
                FirstPageLoadWaitInMS * 3);
            Assert.Equal("Your product index has been successfully updated", Driver.FindElement(By.Id("messageModalLabel")).Text);
            // 24 | click | id=btnMessageModalOk |
            Driver.FindElement(By.Id("btnMessageModalOk")).Click();
            // 25 | click | css=.yamm-fw > a |
            Driver.FindElement(By.CssSelector(".yamm-fw > a")).Click();
            // 26 | click | id=menuItemSiteMaintenance |
            Driver.FindElement(By.Id("menuItemSiteMaintenance")).Click();
            // 27 | click | id=btnProducts |
            Driver.FindElement(By.Id("btnProducts")).Click();
            // 28 | click | id=btnUiTranslations |
            Driver.FindElement(By.Id("btnUiTranslations")).Click();
            // 29 | click | id=btnCategories |
            Driver.FindElement(By.Id("btnCategories")).Click();
            // 30 | click | id=btnGeneralAttributes |
            Driver.FindElement(By.Id("btnGeneralAttributes")).Click();
            // 31 | click | css=.uib-tab:nth-child(2) |
            Driver.FindElement(By.CssSelector(".uib-tab:nth-child(2)")).Click();
            // 32 | click | id=btnJSConfigs |
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("btnJSConfigs")),
                PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnJSConfigs")).Click();
            // 33 | click | id=btnHardSoftStops |
            Driver.FindElement(By.Id("btnHardSoftStops")).Click();
            // 34 | click | id=btnMessageModalOk |
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("btnMessageModalOk")),
                PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnMessageModalOk")).Click();
            // 35 | click | id=btnPrice |
            Driver.FindElement(By.Id("btnPrice")).Click();
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Timeouts().AsynchronousJavaScript = normalTimeout1;
            Driver.Manage().Timeouts().ImplicitWait = normalTimeout2;
            Driver.Manage().Timeouts().PageLoad = normalTimeout3;
            //// // 36 | click | css=a.header-link[target='_self'] |
            //// Driver.FindElement(By.CssSelector("a.header-link[target=\'_self\']")).Click();
            //// // 37 | waitForElementVisible | css=a[uib-dropdown-toggle] > .control-label | 30000
            //// {
            ////     var wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(30));
            ////     wait.Until(driver => Driver.FindElement(By.CssSelector("a[uib-dropdown-toggle] > .control-label")).Displayed);
            //// }
            //// var welcomeLabel = Driver.FindElement(By.CssSelector("a[uib-dropdown-toggle] > .control-label"), FindElementWaitInMS);
            //// var retries = 0;
            //// while (welcomeLabel.Text != "Hi Clarity Admin" && ++retries < 3)
            //// {
            ////     Driver.Navigate().Refresh();
            ////     Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("a[uib-dropdown-toggle] > .control-label")), FirstPageLoadWaitInMS);
            ////     welcomeLabel = Driver.FindElement(By.CssSelector("a[uib-dropdown-toggle] > .control-label"), FindElementWaitInMS);
            //// }
            //// // 38 | assertText | css=a[uib-dropdown-toggle] > .control-label | Hi Clarity Admin
            //// Assert.Equal("Hi Clarity Admin", Driver.FindElement(By.CssSelector("a[uib-dropdown-toggle] > .control-label")).Text);
            //// // 39 | click | id=btnMiniMenuUserName |
            //// Driver.FindElement(By.Id("btnMiniMenuUserName")).Click();
            //// // 40 | click | css=.col:nth-child(1) |
            //// Driver.FindElement(By.Id("btnMiniMenuLogout")).Click();
            //// // 41 | assertText | css=#btnMiniMenuLogin > .ng-scope | Sign In
            //// Assert.Equal("Sign In", Driver.FindElement(By.CssSelector("#btnMiniMenuLogin > .ng-scope")).Text);
        }
    }
}
