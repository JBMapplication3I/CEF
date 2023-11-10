namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Cart : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void Tc33464_VerifyAddingProductsAndChangingTheQuantitiesUpdateTheSubtotals()
        {
            // Cart Components--Bulk Cart Actions and Totals: Check updating the totals of items
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33464

            // Step 1
            // Go to the Products drop down and select the [Books] category
            // Selected category will be opened

            // Step 2
            // Select "a brief history of time"
            // Product detail page for the selected product will be updated

            // Step 3
            // Click the add to cart button
            // product should be added to cart in the selected amount, cart total will be updated and the modal will
            // be displayed

            // Step 4
            // Once the modal opens, click the View Cart button
            // On click, user should be redirected to the cart page
            Driver!.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.AreEqual(GenerateFullCartPageUrl(), Driver.Url);
            Assert.AreEqual("1", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));

            var cartTotalsSubtotal = Driver.GetWebElementById(Ids.CartTotalsSubtotalId);
            Assert.AreEqual(decimal.Parse(this[Ids.Product01SalePrice]).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            // Step 5
            // Once in the cart, use the + button to increase the amount to 4
            Assert.IsTrue(Driver.ClickById(Ids.CartIncreaseProductQuantityButtonId, "0", times: 3));
            // Quantity will be updated, Subtotal amount will be updated to reflect item amount by the amount of items
            Assert.AreEqual("4", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));
            Assert.AreEqual((decimal.Parse(this[Ids.Product01SalePrice]) * 4m).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            // Step 6
            // Use the '-' button to decrease the amount to 3
            Assert.IsTrue(Driver.ClickById(Ids.CartDecreaseProductQuantityButtonId, "0"));
            // Quantity will be updated, Subtotal amount will be updated to reflect item amount by the amount of items.
            Assert.AreEqual("3", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));
            Assert.AreEqual((decimal.Parse(this[Ids.Product01SalePrice]) * 3m).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            Driver.Close();
        }

        [TestMethod]
        public void Tc33489_VerifyRemovingItemsFromCartWorks()
        {
            // Bulk Clear Cart Confirmation Modal: Check the modal appears and controls work
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33489

            // Step 1
            // Go to the Products drop down, select the books category
            // Items in the books category will be displayed

            // Step 2
            // Add "a brief history of time" to the cart
            // Modal will be opened indicating the product and amount that was added

            // Step 3
            // Click the "view cart" button
            // User will be redirected to the cart, the item that was added will be displayed in the same quantity as
            // it was added.
            Driver!.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.AreEqual(GenerateFullCartPageUrl(), Driver.Url);
            Assert.AreEqual("1", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));

            var cartTotalsSubtotal = Driver.GetWebElementById(Ids.CartTotalsSubtotalId);
            Assert.AreEqual(decimal.Parse(this[Ids.Product01SalePrice]).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            // Step 4
            // Once in the cart, Click the red basket icon
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductButtonId, "0"));
            // Modal asking for confirmation of the removal of the product will be displayed
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade in

            // Step 5
            // Click on cancel
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductModalCancelButtonId, "0"));
            // Modal will close, the item will remain in the cart
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade out
            Assert.AreEqual("1", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));
            Assert.AreEqual(decimal.Parse(this[Ids.Product01SalePrice]).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            // Step 6
            // Click the red basket icon
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductButtonId, "0"));
            // Modal asking for confirmation of the removal of the product will be displayed
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade in

            // Step 7
            // Click the "remove item from cart" button
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductModalConfirmationButtonId, "0"));
            // Modal will close, and product will be removed from the cart, shopping cart subtotals and discounts (if
            // applied) will be updated to reflect the item that was removed.
            var cartEmptyLabel = Driver.GetWebElementById(Ids.CartEmptyLabelId);
            Assert.AreEqual(this[Ids.EmptyCartText], cartEmptyLabel.Text);
            Assert.AreEqual("$0.00", cartTotalsSubtotal.Text);

            Driver.Close();
        }

        [TestMethod]
        public void Tc33490_VerifyItemsCanBeAddedViaTheQuickAddControl()
        {
            // Cart Components--Quick Order Search: check items can be added to the cart when in stock
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33490
            Assert.IsTrue(Driver!.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Click the cart icon in the top
            Assert.IsTrue(Driver!.GoToPageById(Ids.MicroCartButtonId));
            // Cart page will open, if no items were previously added it will appear without products.
            Assert.AreEqual(GenerateFullCartPageUrl(), Driver.Url);
            var cartEmptyLabel = Driver.GetWebElementById(Ids.CartEmptyLabelId);
            Assert.AreEqual(this[Ids.EmptyCartText], cartEmptyLabel.Text);

            // Step 2
            // Next to the "Quickly add items to your shopping cart" text enter "time"
            Assert.IsTrue(Driver.InputTextById(Ids.CartQuickOrderSearchInputId, Ids.Product01Name));
            System.Threading.Thread.Sleep(1000); // typeahead-wait-ms="500"
            // Quick add search box will display items based on the text entered

            // Step 3
            // Select "a brief history of time"
            Driver.ClickByLinkText($"{this[Ids.Product01Name]} - {this[Ids.Product01CustomKey]}");
            // SKU of the selected product will fill the search bar
            var cartQuickOrderSearch = Driver.GetWebElementById(Ids.CartQuickOrderSearchInputId);
            var cartQuickSearchItemQuantity = Driver.GetWebElementById(Ids.CartQuickSearchItemQuantityId);
            Assert.AreEqual(this[Ids.Product01Name], cartQuickOrderSearch.GetAttribute("value"));
            Assert.AreEqual("1", cartQuickSearchItemQuantity.GetAttribute("value"));

            // Step 4
            // Change the amount from 1 to 2 by pressing the + button
            Assert.IsTrue(Driver.ClickById(Ids.CartIncreaseQuickSearchItemQuantityButtonId));
            // Amount  will change
            Assert.AreEqual("2", cartQuickSearchItemQuantity.GetAttribute("value"));

            // Step 5
            // Click the add button
            Assert.IsTrue(Driver.ClickById(Ids.CartQuickSearchAddItemId));
            System.Threading.Thread.Sleep(250);
            Driver.WaitUntilJsReady();
            // If there is enough stock for the existing product, It will be added, if not It will be added with the
            // available amount.
            Assert.AreEqual("2", Driver.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));

            // Step 6
            // Enter "testing" in the text box
            Assert.IsTrue(Driver.InputTextById(Ids.CartQuickOrderSearchInputId, Ids.Product02Name));
            System.Threading.Thread.Sleep(1000); // typeahead-wait-ms="500"
            // Quick add search box will display items based on the text entered

            // Step 7
            // Select  "Beautiful Testing"
            Driver.ClickByLinkText($"{this[Ids.Product02Name]} - {this[Ids.Product02CustomKey]}");
            // SKU of the selected product will fill the search bar
            cartQuickOrderSearch = Driver.GetWebElementById(Ids.CartQuickOrderSearchInputId);
            cartQuickSearchItemQuantity = Driver.GetWebElementById(Ids.CartQuickSearchItemQuantityId);

            Assert.AreEqual(this[Ids.Product02Name], cartQuickOrderSearch.GetAttribute("value"));
            Assert.AreEqual("1", cartQuickSearchItemQuantity.GetAttribute("value"));

            // Step 8
            // Click the add button
            Assert.IsTrue(Driver.ClickById(Ids.CartQuickSearchAddItemId));
            // If there is enough stock for the existing product, It will be added, if not It will be added with the
            // available amount.
            Assert.AreEqual("1", Driver.GetWebElementById(Ids.CartProductQuantityId, "1")?.GetAttribute("value"));

            Driver.Close();
        }

        [TestMethod, Ignore("Skip-Obsolete: This input is no longer on this page")]
        public void Tc33501_VerifyDiscountCodesCanBeAddedFromCartPage()
        {
            // Cart Components--Coupon Code Specifications: check valid coupons can be applied
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33501

            // Step 1
            // Go to the Products drop down, Select the books category
            // On click, the items in the selected category will be displayed.

            // Step 2
            // Select "A brief history of time"'
            // Product Detail page for the selected item will be loaded

            // Step 3
            // Add it to the cart
            // Product will be added to the cart, new cart total will be displayed to reflect the item added
            Driver!.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.AreEqual("1", Driver!.GetWebElementById(Ids.CartProductQuantityId, "0")?.GetAttribute("value"));

            var cartTotalsSubtotal = Driver!.GetWebElementById(Ids.CartTotalsSubtotalId);
            Assert.AreEqual(decimal.Parse(this[Ids.Product01SalePrice]).ToString(MoneyFormat), cartTotalsSubtotal.Text);

            // Step 4
            // In the "shopping cart total" section, enter an invalid coupon code and click apply
            Assert.IsTrue(Driver!.InputTextById(Ids.CartDiscountInputId, Ids.Invalid));
            Assert.IsTrue(Driver!.ClickById(Ids.CartDiscountApplyButtonId));
            // An "invalid discount code" message will be displayed.

            // Step 5
            // Enter a valid code

            // Code will be applied, discounted amount will be applied to the subtotal, shipping or grand total amount
            // depending on the nature of the code.
            Driver!.Close();
        }

        [TestMethod]
        public void Tc33502_VerifyTheDataPointsOnCartItems()
        {
            // Cart Components--Shopping Cart Information: check the different parts of the cart information are
            // correctly displayed
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33502
            Assert.IsTrue(Driver!.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Click the Cart icon
            Assert.IsTrue(Driver!.GoToPageById(Ids.MicroCartButtonId));
            // Cart will be displayed,
            Assert.AreEqual(GenerateFullCartPageUrl(), Driver.Url);

            // Step 2
            // In the quick add section, select  "a brief history of time" and add it to the cart
            Driver.AddCartQuickSearchItem(Ids.Product01CustomKey, Ids.Product01Name);
            // product will be added to cart, a thumbnail, sku, name, price, quantity and subtotal will be displayed,
            // next to the subtotal a "basket" icon will be displayed to remove the item .
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);

            // Step 3
            // Click the part number link
            Driver.GetWebElementById(Ids.CartProductSkuId, "0").Click();
            // On click, it will redirect to the product detail page  for the selected item
            Assert.AreEqual(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl), Driver.Url);

            // Step 4
            // Go back
            Assert.IsTrue(Driver.GoToPreviousPage());
            // Cart in previous state will be displayed
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);

            // Step 5
            // Click the name link
            Driver.GetWebElementById(Ids.CartProductNameId, "0").Click();
            // On click, it will redirect to the product detail page  for the selected item
            Assert.AreEqual(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl), Driver.Url);

            // Step 6
            // Go back
            Assert.IsTrue(Driver.GoToPreviousPage());
            // Cart in previous state will be displayed
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);

            // Step 7
            // Click the product image if visible
            Driver.GetWebElementById(Ids.CartProductImageThumbnailId, "0").Click();
            // On click, it will redirect to the product detail page  for the selected item
            Assert.AreEqual(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl), Driver.Url);

            // Step 8
            // Go back
            Assert.IsTrue(Driver.GoToPreviousPage());
            // Cart in previous state will be displayed
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);

            Driver.Close();
        }

        [TestMethod]
        public void Tc33506_VerifyCanGetToCartPageFromMicroCartAndAddAndRemoveItems()
        {
            // Cart Components--Remove from Cart Confirmation Modal: check the button and modal work
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33506

            Assert.IsTrue(Driver.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Click the cart icon
            Assert.IsTrue(Driver.GoToPageById(Ids.MicroCartButtonId));
            // Cart will load, if no previous items were added it will show empty
            Assert.AreEqual(GenerateFullCartPageUrl(), Driver.Url);

            // Step 2
            // In the quick search box, enter "beautiful testing" and add
            Driver.AddCartQuickSearchItem(Ids.Product01CustomKey, Ids.Product01Name);
            // Product will be added to the cart
            Driver.VerifyCartProduct(
                "0", Ids.Product01Name, Ids.Product01CustomKey, Ids.Product01BasePrice, Ids.Product01SalePrice, 1);

            // Step 3
            // Click the red basket icon
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductButtonId, "0"));
            // Confirmation modal will be displayed, will display a text asking if you want to remove (item) from the
            // cart, Cancel button to the left and remove button to the right
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade in

            // Step 4
            // Click on "cancel"
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductModalCancelButtonId, "0"));
            // Cancels the request and closes the modal
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade out
            Assert.AreEqual("1", Driver.GetWebElementById(Ids.CartProductQuantityId, "0").GetAttribute("value"));

            // Step 5
            // Click the red basket icon
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductButtonId, "0"));
            // Confirmation modal will be displayed again
            System.Threading.Thread.Sleep(1000); // Wait for modal to fade in

            // Step 6
            // Click on  "remove item from cart"
            Assert.IsTrue(Driver.ClickById(Ids.CartRemoveProductModalConfirmationButtonId, "0"));
            // Modal will be closed, only the selected product will be removed from the cart and the cart will be
            // updated.
            var cartEmptyLabel = Driver.GetWebElementById(Ids.CartEmptyLabelId);
            Assert.AreEqual(this[Ids.EmptyCartText], cartEmptyLabel.Text);

            Driver.Close();
        }
    }
}
