namespace Clarity.Ecommerce.UI.Testing.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using Tests;

    public static class WebDriverExtensions
    {
        #region Actions
        // Methods in this region should return `driver.WaitUntilJsReady()`

        public static bool ClickById(
            this IWebDriver driver, Ids elementId, string? index = null, int count = 1, int times = 1)
        {
            var jsReady = false;
            if (times > 1)
            {
                for (var i = 0; i < times; i++)
                {
                    jsReady = driver.ClickById(elementId, index, count);
                }
            }
            else
            {
                var element = driver.GetWebElementById(elementId, index, count);
                Assert.IsNotNull(element);
                element.Click();
                jsReady = driver.WaitUntilJsReady();
            }
            return jsReady;
        }

        public static bool ClickByLinkText(
            this IWebDriver driver, string linkText, string? index = null, int count = 1)
        {
            var element = driver.GetWebElementByLinkText(linkText, index, count);
            Assert.IsNotNull(element);
            element.Click();
            return driver.WaitUntilJsReady();
        }

        public static bool InputTextById(
            this IWebDriver driver, Ids elementId, string text, string? index = null, int count = 1)
        {
            var element = driver.GetWebElementById(elementId, index, count);
            Assert.IsNotNull(element);
            element.Clear();
            element.SendKeys(text);
            return driver.WaitUntilJsReady();
        }

        public static bool InputTextById(
            this IWebDriver driver, Ids elementId, Ids text, string? index = null, int count = 1)
        {
            var element = driver.GetWebElementById(elementId, index, count);
            Assert.IsNotNull(element);
            element.Clear();
            element.SendKeys(TestsBase.Parameters[text]);
            return driver.WaitUntilJsReady();
        }

        public static bool MoveMouseToLocationById(
            this IWebDriver driver, Ids elementId, string? index = null, int count = 1)
        {
            var element = driver.GetWebElementById(elementId, index, count);
            new Actions(driver).MoveToElement(element).Build().Perform();
            return driver.WaitUntilJsReady();
        }
        #endregion

        #region Navigation
        // Methods in this region should return `driver.WaitUntilPageLoads()`

        public static bool GoToPageById(this IWebDriver driver, Ids elementId, string? index = null, int count = 1)
        {
            var link = driver.GetWebElementById(elementId, index, count);
            link.Click();
            return driver.WaitUntilPageLoads();
        }

        public static bool GoToPageByUrl(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            return driver.WaitUntilPageLoads();
        }

        public static bool GoToPageByUrl(this IWebDriver driver, Ids url)
        {
            driver.Navigate().GoToUrl(TestsBase.Parameters[url]);
            return driver.WaitUntilPageLoads();
        }

        public static bool GoToProductCategoryFromCategoriesMenu_Mega_Desktop(this IWebDriver driver, Ids index)
        {
            // Move over the element with the "mouse" to activate hover
            var categoriesMenuLinkMegaElement = driver.GetWebElementById(Ids.CategoriesMenuLinkMegaId);
            new Actions(driver).MoveToElement(categoriesMenuLinkMegaElement).Build().Perform();
            ////driver.ClickById(Ids.CategoriesMenuLinkMegaId);
            Thread.Sleep(1000); // Wait for the menu to open
            var categoriesMenuCategoryLink = driver.GetWebElementById(
                Ids.CategoriesMenuCategoryMegaLinkId, TestsBase.Parameters[index]);
            new Actions(driver).MoveToElement(categoriesMenuCategoryLink).Build().Perform();
            driver.ClickById(Ids.CategoriesMenuCategoryNoChildrenSeeAllItemLinkId, TestsBase.Parameters[index]);
            return driver.WaitUntilPageLoads();
        }

        public static bool GoToPreviousPage(this IWebDriver driver)
        {
            if (!(driver is IJavaScriptExecutor javaScriptExecutor)) { return false; }
            javaScriptExecutor.ExecuteScript("window.history.go(-1)");
            return driver.WaitUntilPageLoads();
        }
        #endregion

        #region Sequences
        public static void AddCartQuickSearchItem(this IWebDriver driver, Ids productCustomKey, Ids productName)
        {
            driver.InputTextById(Ids.CartQuickOrderSearchInputId, productName);
            Thread.Sleep(1000); // typeahead-wait-ms="500"
            driver.ClickByLinkText($"{TestsBase.Parameters[productName]} - {TestsBase.Parameters[productCustomKey]}");
            driver.ClickById(Ids.CartQuickSearchAddItemId);
        }

        public static void AddProductToCart(this IWebDriver driver, string productDetailsUrl, int quantity = 1)
        {
            driver.GoToPageByUrl(productDetailsUrl);
            driver.InputTextById(Ids.ProductDetailsQuantityInputId, quantity.ToString());
            driver.ClickById(Ids.ProductDetailsAddToCartButtonId);
            driver.ClickById(Ids.ProductDetailsViewCartHrefButtonId);
        }

        public static void AddOrderDiscountToCart(
            this IWebDriver driver,
            Ids code,
            Ids type,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string index = "0")
        {
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            ////driver.GetWebElementById(Ids.CartOrderDiscountTypeId, index, text: type);
            driver.GetWebElementById(Ids.CartOrderDiscountNameId, index, text: GetDiscountName(name));
            driver.GetWebElementById(Ids.CartOrderDiscountValueId, index, text: value);
            driver.GetWebElementById(Ids.CartOrderDiscountTotalId, index, text: discountsTotal);
            driver.GetWebElementById(Ids.CartOrderDiscountRemoveButtonId, index);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotal);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: discountsTotal);
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: total);
        }

        public static void AddShipmentDiscountToCart(
            this IWebDriver driver,
            Ids code,
            Ids type,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string index = "0")
        {
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            ////driver.GetWebElementById(Ids.CartShipmentDiscountTypeId, index, text: type);
            driver.GetWebElementById(Ids.CartShipmentDiscountNameId, index, text: GetDiscountName(name));
            driver.GetWebElementById(Ids.CartShipmentDiscountValueId, index, text: value);
            driver.GetWebElementById(Ids.CartShipmentDiscountTotalId, index, text: discountsTotal);
            driver.GetWebElementById(Ids.CartShipmentDiscountRemoveButtonId, index);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotal);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: discountsTotal);
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: total);
        }

        public static void AddShipmentDiscountToCart(
            this IWebDriver driver,
            Ids code,
            Ids type,
            string name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string index = "0")
        {
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            ////driver.GetWebElementById(Ids.CartShipmentDiscountTypeId, index, text: type);
            driver.GetWebElementById(Ids.CartShipmentDiscountNameId, index, text: name);
            driver.GetWebElementById(Ids.CartShipmentDiscountValueId, index, text: value);
            driver.GetWebElementById(Ids.CartShipmentDiscountTotalId, index, text: discountsTotal);
            driver.GetWebElementById(Ids.CartShipmentDiscountRemoveButtonId, index);
            driver.GetWebElementById(Ids.CartTotalsSubtotalId, text: subtotal);
            driver.GetWebElementById(Ids.CartTotalsDiscountsId, text: discountsTotal);
            driver.GetWebElementById(Ids.CartTotalsTotalId, text: total);
        }

        public static void AddProductDiscountToCart(
            this IWebDriver driver,
            Ids code,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string productIndex  = "0",
            string discountIndex = "0")
        {
            var cartProductDiscountDescriptionId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountDescriptionId], productIndex, discountIndex);
            var cartProductDiscountValueId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountValueId], productIndex, discountIndex);
            var cartProductDiscountRemoveButtonId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountRemoveButtonId], productIndex, discountIndex);
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            driver.GetWebElementById(cartProductDiscountDescriptionId, text: GetDiscountName(name));
            driver.GetWebElementById(cartProductDiscountValueId, text: value);
            driver.GetWebElementById(cartProductDiscountRemoveButtonId);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotal);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: discountsTotal);
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: total);
        }

        public static void RemoveOrderDiscountFromCart(
            this IWebDriver driver, string subtotalText, string totalText, string index = "0")
        {
            driver.ClickById(Ids.CartOrderDiscountRemoveButtonId, index);
            ////driver.GetWebElementById(Ids.CartOrderDiscountTypeId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountNameId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountValueId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountTotalId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountRemoveButtonId, index, count: 0);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotalText);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: "$0.00");
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: totalText);
        }

        public static void RemoveDiscountFromMiniCartItem(
            this IWebDriver driver, string subtotalText, string totalText, string productIndex = "0", string discountIndex = "0")
        {
            var cartProductDiscountDescriptionId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountDescriptionId], productIndex, discountIndex);
            var cartProductDiscountValueId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountValueId], productIndex, discountIndex);
            var cartProductDiscountRemoveButtonId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountRemoveButtonId], productIndex, discountIndex);
            driver.ClickById(cartProductDiscountRemoveButtonId);
            driver.GetWebElementById(cartProductDiscountDescriptionId, count: 0);
            driver.GetWebElementById(cartProductDiscountValueId, count: 0);
            driver.GetWebElementById(cartProductDiscountRemoveButtonId, count: 0);
            driver.GetWebElementById(Ids.CartTotalsWidgetSubtotalId, text: subtotalText);
            driver.GetWebElementById(Ids.CartTotalsWidgetDiscountsId);
            driver.GetWebElementById(Ids.CartTotalsWidgetTotalId, text: totalText);
        }

        public static void RemoveShipmentDiscountFromCart(
            this IWebDriver driver, string subtotalText, string totalText, string index = "0")
        {
            driver.ClickById(Ids.CartShipmentDiscountRemoveButtonId, index);
            driver.GetWebElementById(Ids.CartShipmentDiscountTypeId, index, count: 0);
            driver.GetWebElementById(Ids.CartShipmentDiscountNameId, index, count: 0);
            driver.GetWebElementById(Ids.CartShipmentDiscountValueId, index, count: 0);
            driver.GetWebElementById(Ids.CartShipmentDiscountTotalId, index, count: 0);
            driver.GetWebElementById(Ids.CartShipmentDiscountRemoveButtonId, index, count: 0);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotalText);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: "$0.00");
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: totalText);
        }

        public static void RemoveDiscountFromCartItem(
            this IWebDriver driver, string subtotalText, string totalText, string productIndex = "0", string discountIndex = "0")
        {
            var cartProductDiscountDescriptionId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountDescriptionId], productIndex, discountIndex);
            var cartProductDiscountValueId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountValueId], productIndex, discountIndex);
            var cartProductDiscountRemoveButtonId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountRemoveButtonId], productIndex, discountIndex);
            driver.ClickById(cartProductDiscountRemoveButtonId);
            driver.GetWebElementById(cartProductDiscountDescriptionId, count: 0);
            driver.GetWebElementById(cartProductDiscountValueId, count: 0);
            driver.GetWebElementById(cartProductDiscountRemoveButtonId, count: 0);
            driver.GetWebElementById(Ids.TotalsWidgetSubtotalId, text: subtotalText);
            driver.GetWebElementById(Ids.TotalsWidgetDiscountsId, text: "$0.00");
            driver.GetWebElementById(Ids.TotalsWidgetTotalId, text: totalText);
        }

        public static void SelectCountryAndRegion(
            this IWebDriver driver, Ids country, Ids countryDdlId, Ids region, Ids regionDdlId, string? index = null)
        {
            ////var countriesDdl = driver.GetSelectElementById(countryDdlId, index);
            ////countriesDdl.SelectByText(BaseTest.Parameters[country]);
            driver.InputTextById(countryDdlId, country, index);
            var regionsDdl = driver.GetSelectElementById(regionDdlId, index);
            Assert.IsNotNull(regionsDdl);
            regionsDdl.SelectByText(TestsBase.Parameters[region]);
        }

        ////public static void GetShipmentRatesForCart(
        ////    this IWebDriver driver, Ids country, Ids region, Ids postalCode)
        ////{
        ////    driver.SelectCountryAndRegion(
        ////        country, Ids.CartShipmentInfoCountryDdlId, region, Ids.CartShipmentInfoRegionDdlId);
        ////    driver.InputTextById(Ids.CartShipmentInfoZipCodeInputId, postalCode);
        ////    driver.ClickById(Ids.CartShipmentInfoGetRatesButtonId);
        ////}

        public static void SignIn(this IWebDriver driver, Ids userName, Ids password, bool clickSignInBtn = true)
        {
            if (clickSignInBtn)
            {
                driver.ClickById(Ids.MiniMenuLoginButtonId);
            }
            Thread.Sleep(500);
            driver.InputTextById(Ids.MiniMenuLoginUsername, userName);
            driver.InputTextById(Ids.MiniMenuLoginPassword, password);
            Thread.Sleep(500);
            driver.ClickById(Ids.MiniMenuSubmitLoginCredentials);
            Thread.Sleep(500);
        }

        public static void CheckoutSignIn(
            this IWebDriver driver, Ids userName, Ids password, bool clickSignInBtn = true)
        {
            if (clickSignInBtn)
            {
                driver.ClickById(Ids.MiniMenuLoginButtonId);
            }
            driver.WaitUntilJsReady();
            driver.InputTextById(Ids.CheckoutUserName, userName);
            driver.InputTextById(Ids.CheckoutPassword, password);
            driver.WaitUntilJsReady();
            driver.ClickById(Ids.CheckoutLoginButton);
            driver.WaitUntilJsReady();
        }

        // Checkout
        public static void AddCheckoutBillingAddress(
            this IWebDriver driver,
            string FirstName,
            string LastName,
            string Email,
            string Phone,
            Ids Company,
            Ids Country,
            Ids Street1,
            Ids City,
            Ids Region,
            Ids ZipCode,
            Ids Submit)
        {
            driver.InputTextById(Ids.CheckoutBillingFirstName, FirstName);
            driver.InputTextById(Ids.CheckoutBillingLastName, LastName);
            driver.InputTextById(Ids.CheckoutBillingEmail, Email);
            driver.InputTextById(Ids.CheckoutBillingPhone, Phone);
            driver.InputTextById(Ids.CheckoutBillingCompany, Company);
            driver.InputTextById(Ids.CheckoutBillingStreet1, Street1);
            driver.InputTextById(Ids.CheckoutBillingCity, City);
            driver.InputTextById(Ids.CheckoutBillingZipCode, ZipCode);
            driver.SelectCountryAndRegion(
                country: Country,
                countryDdlId: Ids.CheckoutBillingCountry,
                region: Region,
                regionDdlId: Ids.CheckoutBillingRegion);
            driver.ClickById(Submit);
        }

        public static void AddCheckoutShippingAddress(
            this IWebDriver driver,
            string FirstName,
            string LastName,
            string Email,
            string Phone,
            Ids Company,
            Ids Country,
            Ids Street1,
            Ids City,
            Ids Region,
            Ids ZipCode,
            Ids Submit)
        {
            driver.InputTextById(Ids.CheckoutShippingFirstName, FirstName);
            driver.InputTextById(Ids.CheckoutShippingLastName, LastName);
            driver.InputTextById(Ids.CheckoutShippingEmail, Email);
            driver.InputTextById(Ids.CheckoutShippingPhone, Phone);
            driver.InputTextById(Ids.CheckoutShippingCompany, Company);
            driver.InputTextById(Ids.CheckoutShippingStreet1, Street1);
            driver.InputTextById(Ids.CheckoutShippingCity, City);
            driver.InputTextById(Ids.CheckoutShippingZipCode, ZipCode);
            driver.SelectCountryAndRegion(
                country: Country,
                countryDdlId: Ids.CheckoutShippingCountry,
                region: Region,
                regionDdlId: Ids.CheckoutShippingRegion);
            driver.ClickById(Submit);
        }

        // Discounts for MiniCart
        public static void ValidateCartTotalsWidget(
            this IWebDriver driver, string subtotal, string total, string? discount = null)
        {
            driver.GetWebElementById(Ids.CartTotalsWidgetSubtotalId, text: subtotal);
            driver.GetWebElementById(Ids.CartTotalsWidgetTotalId, text: total);
            if (discount != null)
            {
                driver.GetWebElementById(Ids.CartTotalsWidgetDiscountsId, text: discount);
            }
        }

        public static void AddOrderDiscountToMiniCart(
            this IWebDriver driver,
            Ids code,
            Ids type,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string index = "0")
        {
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            driver.GetWebElementById(Ids.CartOrderDiscountNameId, index, text: GetDiscountName(name));
            driver.GetWebElementById(Ids.CartOrderDiscountValueId, index, text: value);
            driver.GetWebElementById(Ids.CartOrderDiscountTotalId, index, text: discountsTotal);
            driver.GetWebElementById(Ids.CartOrderDiscountRemoveButtonId, index);
            driver.ValidateCartTotalsWidget(subtotal: subtotal, total: total, discount: discountsTotal);
        }

        public static void AddProductDiscountToMiniCart(
            this IWebDriver driver,
            Ids code,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string productIndex  = "0",
            string discountIndex = "0")
        {
            var cartProductDiscountDescriptionId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountDescriptionId], productIndex, discountIndex);
            var cartProductDiscountValueId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountValueId], productIndex, discountIndex);
            var cartProductDiscountRemoveButtonId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountRemoveButtonId], productIndex, discountIndex);
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            driver.GetWebElementById(cartProductDiscountValueId, text: value);
            driver.ValidateCartTotalsWidget(subtotal: subtotal, total: total, discount: discountsTotal);
        }

        public static void AddProductDiscountToMiniCart(
            this IWebDriver driver,
            string code,
            string name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string productIndex  = "0",
            string discountIndex = "0")
        {
            var cartProductDiscountDescriptionId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountDescriptionId], productIndex, discountIndex);
            var cartProductDiscountValueId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountValueId], productIndex, discountIndex);
            var cartProductDiscountRemoveButtonId = string.Format(
                TestsBase.Parameters[Ids.CartProductDiscountRemoveButtonId], productIndex, discountIndex);
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            driver.GetWebElementById(cartProductDiscountValueId, text: value);
            driver.ValidateCartTotalsWidget(subtotal: subtotal, total: total, discount: discountsTotal);
        }

        public static void RemoveOrderDiscountFromMiniCart(
            this IWebDriver driver, string subtotalText, string totalText, string index = "0")
        {
            driver.ClickById(Ids.CartOrderDiscountRemoveButtonId, index);
            ////driver.GetWebElementById(Ids.CartOrderDiscountTypeId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountNameId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountValueId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountTotalId, index, count: 0);
            driver.GetWebElementById(Ids.CartOrderDiscountRemoveButtonId, index, count: 0);
            driver.ValidateCartTotalsWidget(subtotal: subtotalText, total: totalText);
        }

        public static void AddShipmentDiscountToMiniCart(
            this IWebDriver driver,
            Ids code,
            Ids type,
            Ids name,
            string value,
            string discountsTotal,
            string subtotal,
            string total,
            string index = "0")
        {
            driver.InputTextById(Ids.CartDiscountInputId, code);
            driver.ClickById(Ids.CartDiscountApplyButtonId);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountNameId, index, text: GetDiscountName(name));
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountValueId, index, text: value);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountTotalId, index, text: discountsTotal);
            driver.ValidateCartTotalsWidget(subtotal: subtotal, total: total, discount: discountsTotal);
        }

        public static void RemoveShipmentDiscountFromMiniCart(
            this IWebDriver driver, string subtotalText, string totalText, string index = "0")
        {
            driver.ClickById(Ids.MiniCartShipmentDiscountRemoveButtonId, index);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountNameId, index, count: 0);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountValueId, index, count: 0);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountTotalId, index, count: 0);
            driver.GetWebElementById(Ids.MiniCartShipmentDiscountRemoveButtonId, index, count: 0);
            driver.ValidateCartTotalsWidget(subtotal: subtotalText, total: totalText);
        }
        #endregion

        public static IWebElement? GetWebElementById(
            this ISearchContext driver,
            Ids elementId,
            string? index = null,
            int count = 1,
            string? text = null,
            bool displayed = true)
        {
            var id = index != null
                ? string.Format(TestsBase.Parameters[elementId], index)
                : TestsBase.Parameters[elementId];
            return GetWebElementInner(driver.FindElements(By.Id(id)), count, id, displayed, text);
        }

        public static IWebElement? GetWebElementByLinkText(
            this ISearchContext driver,
            string linkText,
            string? index = null,
            int count = 1)
        {
            return GetWebElementInner(driver.FindElements(By.LinkText(linkText)), count, linkText, true);
        }

        public static SelectElement? GetSelectElementById(
            this ISearchContext driver,
            Ids elementId,
            string? index = null,
            int count = 1)
        {
            var id = index != null
                ? string.Format(TestsBase.Parameters[elementId], index)
                : TestsBase.Parameters[elementId];
            var elements = driver.FindElements(By.Id(id));
            Assert.AreEqual(
                count,
                elements.Count,
                $"\r\nElement '{id}' occurred {elements.Count} time(s) when it should have been {count} time(s)");
            if (elements.Count <= 0)
            {
                return null;
            }
            var element = elements[count - 1];
            Assert.IsTrue(element.Displayed);
            return new SelectElement(element);
        }

        public static void VerifyCartProduct(
            this ISearchContext driver, string index, Ids name, Ids customKey, Ids price, Ids subtotalToMultiply, int quantity)
        {
            driver.GetWebElementById(Ids.CartProductImageThumbnailId, index);
            driver.GetWebElementById(Ids.CartProductSkuId, index, text: TestsBase.Parameters[customKey]);
            driver.GetWebElementById(Ids.CartProductNameId, index, text: TestsBase.Parameters[name]);
            driver.GetWebElementById(Ids.CartProductPriceId, index, text: decimal.Parse(TestsBase.Parameters[price]).ToString(TestsBase.MoneyFormat));
            driver.GetWebElementById(Ids.CartProductSubtotalId, index, text: decimal.Parse(TestsBase.Parameters[subtotalToMultiply]).ToString(TestsBase.MoneyFormat));
            driver.GetWebElementById(Ids.CartProductQuantityId, index, text: quantity.ToString("n0"));
        }

        public static void VerifyMiniCartProduct(
            this ISearchContext driver, string index, string name, decimal price, int quantity)
        {
            driver.GetWebElementById(Ids.MiniCartProductImageThumbnailId, index);
            driver.GetWebElementById(Ids.MiniCartProductNameId, index, text: name);
            driver.GetWebElementById(Ids.MiniCartProductPriceId, index, text: price.ToString(TestsBase.MoneyFormat));
            driver.GetWebElementById(Ids.MiniCartProductQuantityId, index, text: quantity.ToString("n0"));
        }

        private static bool ClickById(
            this IWebDriver driver, string elementId, string? index = null, int count = 1, int times = 1)
        {
            var jsReady = false;
            if (times > 1)
            {
                for (var i = 0; i < times; i++)
                {
                    jsReady = driver.ClickById(elementId, index, count);
                }
            }
            else
            {
                var element = driver.GetWebElementById(elementId, index, count);
                Assert.IsNotNull(element);
                element.Click();
                jsReady = driver.WaitUntilJsReady();
            }
            return jsReady;
        }

        private static IWebElement? GetWebElementById(
            this ISearchContext driver,
            string elementId,
            string? index = null,
            int count = 1,
            string? text = null,
            bool displayed = true)
        {
            if (index != null)
            {
                elementId = string.Format(elementId, index);
            }
            return GetWebElementInner(driver.FindElements(By.Id(elementId)), count, elementId, displayed, text);
        }

        private static IWebElement? GetWebElementInner(
            IReadOnlyList<IWebElement> elements,
            int count,
            string elementId,
            bool displayed,
            string? text = null)
        {
            Assert.AreEqual(
                count,
                elements.Count,
                $"\r\nElement '{elementId}' occurred {elements.Count} time(s) when it should have been {count} time(s)");
            if (elements.Count <= 0)
            {
                return null;
            }
            var element = elements[count - 1];
            Assert.AreEqual(
                displayed,
                element.Displayed,
                $"\r\nElement '{elementId}' should be {(displayed ? "" : "not ")}displayed but it wasn't matching this");
            if (!string.IsNullOrEmpty(text))
            {
                var value = element.TagName.Equals("input") ? element.GetAttribute("value") : element.Text;
                Assert.AreEqual(
                    text!.ToLower(),
                    value.ToLower(),
                    $"\r\nElement '{elementId}' should have a text value of\r\n\r\n{text}\r\n\r\nbut has\r\n\r\n{value}");
            }
            return element;
        }

        public static bool WaitUntilJsReady(this IWebDriver driver)
        {
            if (!(driver is IJavaScriptExecutor javaScriptExecutor))
            {
                return false;
            }
            const string JavaScript = "return window.angular !== undefined"
                + "&& angular.element(document).injector() !== undefined"
                + "&& angular.element(document).injector().get('$http').pendingRequests.length === 0";
            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            // Must pass at least four times times over the course of a second
            for (var i = 0; i < 4; i++)
            {
                webDriverWait.Until(x => (bool)javaScriptExecutor.ExecuteScript(JavaScript));
                Thread.Sleep(250);
            }
            return true;
        }

        /// <summary>Find the "CLEAR BROWSING BUTTON" on the Chrome settings page.</summary>
        /// <param name="driver">The driver.</param>
        /// <returns>The clear browsing button.</returns>
        private static IWebElement GetClearBrowsingButton(this IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("* /deep/ #clearBrowsingDataConfirm"));
        }

        /// <summary>Clear the cookies and cache for the ChromeDriver instance.</summary>
        /// <param name="driver">The driver.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool ClearBrowserCache(this IWebDriver driver)
        {
            // Navigate to the settings page
            driver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            driver.WaitUntilJsReady();
            // wait for the button to appear
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            var e = wait.Until(GetClearBrowsingButton);
            // click the button to clear the cache
            e.Click();
            // wait for the button to be gone before returning
            // wait.UntilNot(GetClearBrowsingButton); // No UntilNot function
            return driver.WaitUntilJsReady();
        }

        private static bool WaitUntilPageLoads(this IWebDriver driver)
        {
            if (!(driver is IJavaScriptExecutor javaScriptExecutor))
            {
                return false;
            }
            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            webDriverWait.Until(x => javaScriptExecutor.ExecuteScript("return document.readyState").Equals("complete"));
            return driver.WaitUntilJsReady();
        }

        private static string GetDiscountName(Ids discountName)
        {
            var name = TestsBase.Parameters[discountName];
            return name.Length > 13 ? $"{name.Substring(0, 13)}..." : name;
        }
    }
}
