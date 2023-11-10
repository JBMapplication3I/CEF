// <copyright file="HappyPathCheckoutTest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the happy path checkout test class</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System.Linq;
    using OpenQA.Selenium;
    using Xunit;

    public partial class SuiteTests : SuiteTestsBase
    {
        [Fact]
        public void HappyPathCheckout()
        {
            // Test name: Happy Path Checkout
            // Step # | name | target | value | note
            ImportProductsIndexProductsAndClearCaches();
            // 02 | open | websiteUrl | | Open the website
            Driver.Navigate().GoToUrl(WebsiteUrl);
            // 03 | click | id=btnCategoriesMenuLinkMega | | Click the menu button to open it (more reliable that the test trying to mouse over it)
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnCategoriesMenuLinkMega")), FirstPageLoadWaitInMS);
            Driver.MouseOverElement(By.Id("btnCategoriesMenuLinkMega"), FindElementWaitInMS);
            // Wait for the menu to open
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnMenuItemBrowseAllCategories")), PageLoadWaitInMS);
            // 04 | click | id=btnMenuItemBrowseAllCategories | | Click the Browse All Categories button so we get into the catalog
            Driver.FindElement(By.Id("btnMenuItemBrowseAllCategories"), FindElementWaitInMS).Click();
            var youAreHereLabel = Driver.FindElement(By.Id("lblYouAreHereProducts"), FindElementWaitInMS);
            var retries = 0;
            while (youAreHereLabel.Text != "You are here:" && ++retries < 3)
            {
                // Make sure the translations loaded
                Driver.Navigate().Refresh();
                Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("lblYouAreHereProducts")), PageLoadWaitInMS);
                youAreHereLabel = Driver.FindElement(By.Id("lblYouAreHereProducts"), FindElementWaitInMS);
            }
            Assert.Equal("You are here:", youAreHereLabel.Text);
            // 05 | click | css=button[title=\"Add to Cart\"] (take first one) | | Add an item to the cart
            Driver.FindElements(By.CssSelector("button[title=\"Add to Cart\"]"), FindElementWaitInMS).First().Click();
            // 06 | assertText | id=btnAddToCartModalGoToCart | View Cart
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnAddToCartModalGoToCart")), PageLoadWaitInMS);
            var addToCartModalGoToCartButton = Driver.FindElement(By.Id("btnAddToCartModalGoToCart"), FindElementWaitInMS);
            Assert.Equal("View Cart", addToCartModalGoToCartButton.Text);
            // 07 | click | id=btnAddToCartModalGoToCart | | CLick the button to go to the cart
            addToCartModalGoToCartButton.Click();
            // 08 | assertText | id=btnProceedToCheckout | ProceedVerify the checkout button is there with the right text
            var proceedToCheckoutButton = Driver.FindElement(By.Id("btnProceedToCheckout"), FindElementWaitInMS);
            retries = 0;
            while (proceedToCheckoutButton.Text != "Proceed to Checkout" && ++retries < 3)
            {
                // Make sure the translations loaded
                Driver.Navigate().Refresh();
                Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnProceedToCheckout")), PageLoadWaitInMS);
                proceedToCheckoutButton = Driver.FindElement(By.Id("btnProceedToCheckout"), FindElementWaitInMS);
            }
            Assert.Equal("Proceed to Checkout", proceedToCheckoutButton.Text);
            // 09 | click | id=btnProceedToCheckout | | Go to the checkout page using the button
            proceedToCheckoutButton.Click();
            // 09.5 | assertText | id=lblOrderSummary | Order Summary | Make sure the translations loaded
            var orderSummaryLabel = Driver.FindElement(By.Id("lblOrderSummary"), PageLoadWaitInMS);
            retries = 0;
            while (orderSummaryLabel.Text != "Order Summary" && ++retries < 3)
            {
                Driver.Navigate().Refresh();
                Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("lblOrderSummary")), PageLoadWaitInMS);
                orderSummaryLabel = Driver.FindElement(By.Id("lblOrderSummary"), FindElementWaitInMS);
            }
            // 10 | | | log in via checkout method pane
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("txtCheckoutLoginUsername")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("txtCheckoutLoginUsername"), FindElementWaitInMS).SendKeys(Username);
            Driver.FindElement(By.Id("pwCheckoutLoginPassword"), FindElementWaitInMS).SendKeys(Password);
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnCheckoutLoginSubmit")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnCheckoutLoginSubmit"), FindElementWaitInMS).Click();
            // 11 | click | id=ddlBillingSelection | | Wait for the login to finish and then get into the dropdown and select the Ascot address
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("ddlBillingSelection")), PageLoadWaitInMS);
            Driver.OpenAndPickOptionFromSelect(
                By.Id("ddlBillingSelection"),
                ////By.XPath("//option[.='ASCOT: 1321 Ascot Street']"), // Alt selectors: css=option:nth-child(2) __ xpath=/option[2]
                By.CssSelector("#ddlBillingSelection > option:nth-child(2)"),
                FindElementWaitInMS,
                PageLoadWaitInMS);
            // 12 | click | id=btnSubmit_purchaseStepBilling | Wait for the button to be ready (validator run finishes) and then click continue
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnSubmit_purchaseStepBilling")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnSubmit_purchaseStepBilling"), FindElementWaitInMS).Click();
            // 13 | click | select[ng-model='target.DestinationContactID'][index='0'] | | Wait for the shipping step to load and then select the destination for the cart item
            var targetSelectBoxCSSSelectorString = "select[ng-model='target.DestinationContactID'][index='0']";
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.CssSelector(targetSelectBoxCSSSelectorString)),
                PageLoadWaitInMS);
            Driver.OpenAndPickOptionFromSelect(
                By.CssSelector(targetSelectBoxCSSSelectorString),
                ////By.XPath("//option[.='ASCOT: 1321 Ascot Street']"), // Alt selectors: css=option:nth-child(2) __ xpath=/option[2]
                By.CssSelector(targetSelectBoxCSSSelectorString + " > option:nth-child(2)"),
                FindElementWaitInMS,
                PageLoadWaitInMS);
            // 14 | click | id=btnSubmitAndGetRateQuotesForShipments | | Submit the targets and get rates
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnSubmitAndGetRateQuotesForShipments")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnSubmitAndGetRateQuotesForShipments"), FindElementWaitInMS).Click();
            // 15 | runScript | window.scrollTo(0,337) | | Scroll down so we can see the rates
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("lbResolvedToTargetsMessage")),
                PageLoadWaitInMS);
            Driver.ScrollToElement(JS, By.Id("lbResolvedToTargetsMessage"), PageLoadWaitInMS);
            // 16 | click | css=input[type='radio'][ng-model='cRQMWCtrl.selectedRateQuoteID'][index='0'] | | Select the first shipping rate that is available
            ////var firstRateQuoteAvailableCSSSelectorString = "input[type='radio'][ng-model='cRQMWCtrl.selectedRateQuoteID'][index='0']";
            // The radio button always says it's hidden (probably due to styling), but the label works
            var firstRateQuoteAvailableCSSSelectorString = "label[ng-bind='rate.ShipCarrierMethodName'][index='0']";
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.CssSelector(firstRateQuoteAvailableCSSSelectorString)),
                PageLoadWaitInMS * 2);
            Driver.FindElement(By.CssSelector(firstRateQuoteAvailableCSSSelectorString), FindElementWaitInMS).Click();
            // 17 | click | id=btnSubmit_purchaseStepSplitShipping | | Wait for the validation to finish and then click the continue button
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.Id("btnSubmit_purchaseStepSplitShipping")),
                PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnSubmit_purchaseStepSplitShipping"), FindElementWaitInMS).Click();
            // 18 | click | id=txtCardHolderName | | Wait for the payment screen to load and then scroll to the right location
            Driver.WaitFor(
                Conditions.ElementIsVisibleAndEnabled(By.CssSelector("div[cef-purchase-step-view-title] > h5")),
                PageLoadWaitInMS);
            Driver.ScrollToElement(JS, By.CssSelector("div[cef-purchase-step-view-title] > h5"), PageLoadWaitInMS);
            // 19 | click | id=txtCardHolderName | | Click the Card Holder Name field
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("txtCardHolderName")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("txtCardHolderName"), FindElementWaitInMS).Click();
            // 20 | type | id=txtCardHolderName | Clarity | And then enter text
            Driver.FindElement(By.Id("txtCardHolderName"), FindElementWaitInMS).SendKeys("John Smith");
            // 21 | click | id=txtCardNumber | | Click the Credit Card Number field
            Driver.FindElement(By.Id("txtCardNumber"), FindElementWaitInMS).Click();
            // 22 | type | id=txtCardNumber | 4111111111111111 | Set the credit card number
            Driver.FindElement(By.Id("txtCardNumber"), FindElementWaitInMS).SendKeys("4111111111111111");
            // 23 | click | id=ddlExpirationMonth | | Set the expiration month
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("ddlExpirationMonth")), PageLoadWaitInMS);
            Driver.OpenAndPickOptionFromSelect(
                By.Id("ddlExpirationMonth"),
                By.XPath("//option[. = 'July']"),
                FindElementWaitInMS,
                PageLoadWaitInMS);
            // 24 | click | id=ddlExpirationYear | | Set the expiration year
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("ddlExpirationYear")), PageLoadWaitInMS);
            Driver.OpenAndPickOptionFromSelect(
                By.Id("ddlExpirationYear"),
                By.XPath("//option[. = '2025']"),
                FindElementWaitInMS,
                PageLoadWaitInMS);
            // 25 | click | id=txtCVV | | Click the CVV field
            Driver.FindElement(By.Id("txtCVV"), FindElementWaitInMS).Click();
            // 26 | type | id=txtCVV | 123 | Set the CVV code
            Driver.FindElement(By.Id("txtCVV"), FindElementWaitInMS).SendKeys("123");
            // 27 | assertText | id=btnSubmit_purchaseStepPayment | Confirm Order and Purchase | Verify the text
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnSubmit_purchaseStepPayment")), PageLoadWaitInMS);
            Assert.Equal("Confirm Order and Purchase", Driver.FindElement(By.Id("btnSubmit_purchaseStepPayment"), FindElementWaitInMS).Text);
            // 28 | click | id=btnSubmit_purchaseStepPayment | | Click the button
            Driver.FindElement(By.Id("btnSubmit_purchaseStepPayment"), FindElementWaitInMS).Click();
            // 29 | assertText | css=.mt-0:nth-child(1) | Thank you for your Purchase | Assert the text is there
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("lbThankYouForYourPurchase")), PageLoadWaitInMS * 3);
            Assert.Equal("Thank you for your Purchase", Driver.FindElement(By.Id("lbThankYouForYourPurchase"), FindElementWaitInMS).Text);
            // 30 | click | id=btnYourOrderHistory | | Click the Your Order History button to go to the dashboard
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("btnYourOrderHistory")), PageLoadWaitInMS);
            Driver.FindElement(By.Id("btnYourOrderHistory"), FindElementWaitInMS).Click();
            Driver.WaitFor(Conditions.ElementIsVisibleAndEnabled(By.Id("userDashboardSideMenuOrdersBtn")), PageLoadWaitInMS);
        }
    }
}
