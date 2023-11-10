// <copyright file="CatalogViewOptionsTest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the catalog view options test class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using OpenQA.Selenium;
    using Xunit;

    public partial class SuiteTests
    {
        /// <summary>Catalog view options.</summary>
        [Fact]
        public void CatalogViewOptions()
        {
            Driver.Navigate().GoToUrl(WebsiteUrl);
            Driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            // Step 1
            Driver.MouseOverElement(Driver.FindElement(By.CssSelector(Ids.Get("Step1CssSelector")), PageLoadWaitInMS));
            Driver.MouseOverElement(Driver.FindElement(By.CssSelector(Ids.Get("Step2CssSelector"))));
            Driver.FindElement(By.XPath(Ids.Get("Step3XPathSelector"))).Click();
            Assert.Equal(Ids.Get("CatalogBreadcrumbNameResult"), Driver.FindElement(By.CssSelector("#breadCatalogRootLink > .ng-scope")).Text);
            Assert.Equal("View", Driver.FindElement(By.CssSelector(".ng-isolate-scope > .form-group > .control-label > .ng-scope")).Text);
            Assert.Equal("Table", Driver.FindElement(By.Id("btnFormatsViewtable"), FindElementWaitInMS).Text);
            Driver.MouseOverElement(Driver.FindElement(By.CssSelector("#btnFormatsViewtable > .text-capitalize")));
            Driver.FindElement(By.XPath("//button[@id=\'btnFormatsViewtable\']/span")).Click();
            Assert.Equal("Add to Cart", Driver.FindElement(By.CssSelector("[side-id=btnGridAddToCartProduct_1] > span")).Text);
            Assert.Equal("Price", Driver.FindElement(By.CssSelector("tr > .ng-scope:nth-child(4)")).Text);
            Assert.Equal("Stock", Driver.FindElement(By.CssSelector("thead .w-auto:nth-child(3)")).Text);
            Assert.Equal("SKU", Driver.FindElement(By.CssSelector("tr > .ng-scope:nth-child(2)")).Text);
            Assert.Equal("Name", Driver.FindElement(By.CssSelector("tr > .ng-scope:nth-child(1)")).Text);
            Assert.Equal("List", Driver.FindElement(By.CssSelector("#btnFormatsViewlist > .text-capitalize")).Text);
            Driver.MouseOverElement(Driver.FindElement(By.CssSelector(".fa-th-list")));
            Driver.FindElement(By.Id("btnFormatsViewlist")).Click();
            Driver.MouseOverElement(Driver.FindElement(By.CssSelector("#btnFormatsViewtable .ng-binding")));
            Assert.Equal("SKU", Driver.FindElement(By.CssSelector("#productListBlock145 .product-sku > .ng-scope > .ng-scope")).Text);
            Assert.Equal("Add to Cart", Driver.FindElement(By.Id("btnGridAddToCartProduct145")).Text);
            Assert.Equal("Grid", Driver.FindElement(By.Id("btnFormatsViewgrid")).Text);
            Driver.MouseOverElement(Driver.FindElement(By.Id("btnFormatsViewgrid"), FindElementWaitInMS));
            Driver.FindElement(By.Id("btnFormatsViewgrid")).Click();
        }
    }
}
