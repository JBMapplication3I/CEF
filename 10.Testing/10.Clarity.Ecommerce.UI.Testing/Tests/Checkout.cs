namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Checkout : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void Tc33507()
        {
            // Checkout Features--Mini-Cart Widget: Verify widget features and functionality
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33507

            Assert.IsTrue(Driver.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Go to the cart and using the quick add, add 2 different items to the cart
            Assert.IsTrue(Driver.GoToPageById(Ids.MicroCartButtonId));
            Driver.AddCartQuickSearchItem(Ids.Product01CustomKey, Ids.Product01Name);
            Driver.AddCartQuickSearchItem(Ids.Product02CustomKey, Ids.Product02Name);
            // Products will be added to the cart, cart total will be updated
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);
            Driver.VerifyCartProduct(
                "1", Ids.Product02Name, Ids.Product02CustomKey, Ids.Product02Price, Ids.Product02Price, 1);

            // Step 2
            // In the cart, click on continue to checkout
            Assert.IsTrue(Driver.GoToPageById(Ids.CartProceedToCheckoutButtonId));
            // Checkout page will be displayed, mini cart will be displayed in the right side of the page
            Driver.GetWebElementById(Ids.CheckoutMiniCartId);

            // Step 3
            // Check Widget contains the items added, should be located to the right of the page.
            // Products thumbnails, name, price, subtotals and total will be displayed.
            var product1SalePrice = decimal.Parse(this[Ids.Product01SalePrice]);
            var product2Price = decimal.Parse(this[Ids.Product02Price]);
            Driver.VerifyMiniCartProduct("0", this[Ids.Product01Name], product1SalePrice, 1);
            Driver.VerifyMiniCartProduct("1", this[Ids.Product02Name], product2Price, 1);
            Assert.AreEqual((product1SalePrice + product2Price).ToString(MoneyFormat), Driver.GetWebElementById(Ids.CartTotalsWidgetSubtotalId).Text);
            Assert.AreEqual((product1SalePrice + product2Price).ToString(MoneyFormat), Driver.GetWebElementById(Ids.CartTotalsWidgetTotalId).Text);

            // Step 4
            // Click the Edit button, change amount of one of the items
            // Cart will be updated to reflect new amounts and totals.

            Driver.Close();
        }

        [TestMethod]
        public void Tc33510()
        {
            // Checkout Features--Sign-in: check it's possible to sign in during checkout.
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33510

            Assert.IsTrue(Driver.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Log out if already signed in.
            // User should be logged out

            // Step 2
            // Add an item to the cart and continue to checkout
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCartPageUrl()));
            Driver.AddCartQuickSearchItem(Ids.Product01CustomKey, Ids.Product01Name);
            Assert.IsTrue(Driver.ClickById(Ids.CartProceedToCheckoutButtonId));
            Assert.IsTrue(Driver.WaitUntilJsReady());
            // Product will be added if available in stock and user will be redirected to the checkout page when he selects "continue to checkout"
            Assert.AreEqual(GenerateFullCheckoutPageUrl(), Driver.Url);

            // Step 3
            // Enter invalid username and password in the form field
            Assert.IsTrue(Driver.InputTextById(Ids.CartCheckoutMethodLoginUsernameId, "invalidUsername"));
            Assert.IsTrue(Driver.InputTextById(Ids.CartCheckoutMethodLoginPasswordId, "invalidPassword"));
            // When the user enters invalid credentials, an error message indicating the the credentials are invalid and to try again with the correct ones.
            Assert.IsTrue(Driver.ClickById(Ids.CartCheckoutMethodLoginButtonId));
            Driver.GetWebElementById(Ids.CartCheckoutMethodLoginErrorMessageId);
            Assert.AreEqual("Sign In", Driver.GetWebElementById(Ids.MiniMenuLoginButtonId).Text);

            // Step 4
            // Enter valid username and password
            Assert.IsTrue(Driver.InputTextById(Ids.CartCheckoutMethodLoginUsernameId, Ids.UserName));
            Assert.IsTrue(Driver.InputTextById(Ids.CartCheckoutMethodLoginPasswordId, Ids.Password));

            // Step 5
            // Click on Sign-in and continue
            Assert.IsTrue(Driver.ClickById(Ids.CartCheckoutMethodLoginButtonId));
            // User will be logged in, the dashboard dropdown in the top right will display the username.
            var miniMenuUserNameButton = Driver.GetWebElementById(Ids.MiniMenuUserNameButtonId);
            Assert.AreEqual($"Hi {this[Ids.DisplayName]} ", miniMenuUserNameButton.Text);

            Driver.Close();
        }
    }
}
