namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using System;
    using System.Threading;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;

    [TestClass, TestCategory("UI Tests")]
    public class ProductDetailComponent : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void Tc33128_VerifyRatingStarsFillWhenHovered()
        {
            // Product Details: Rating Scale Stars Should Fill Up on Hover
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33128

            // Step 1
            // Navigate to the Product Dropdown

            // Step 2
            // Select the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Go to the reviews section
            Driver.SignIn(Ids.UserName, Ids.Password);
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsTabReviews, "0"));

            // Step 5
            // Hover on the stars
            for (var i = 1; i <= 5; i++)
            {
                Assert.IsTrue(Driver.MoveMouseToLocationById(Ids.ProductDetailsRatingStars, i.ToString()));
                Assert.IsFalse(Driver.GetWebElementById(Ids.ProductDetailsRatingStars, i.ToString()).HasClass("fa-star-o"));
            }
        }

        [TestMethod]
        public void Tc33129_VerifyEnteringReviewsWorks()
        {
            // Product Details: Verify Entering a Review Works
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33129

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Go to the reviews section
            Driver.SignIn(Ids.UserName, Ids.Password);
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsTabReviews, "0"));
            Thread.Sleep(500);

            // Step 5
            // Click on the desired amount of stars to select rating
            // Enter a title
            // Enter a comment about a product
            // Click the save button
            const string Success = "Success! Your Review has been saved.";
            for (var i = 1; i <= 5; i++)
            {
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsRatingStars, i.ToString()));
                Assert.IsTrue(Driver.InputTextById(Ids.ProductDetailsRatingTitle, $"{i} star title", i.ToString()));
                Assert.IsTrue(Driver.InputTextById(Ids.ProductDetailsRatingComment, $"This is a {i} star review", i.ToString()));
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsSubmitRatingButton));
                Assert.AreEqual(Success, Driver.GetWebElementById(Ids.ProductDetailsReviewSuccessString).Text);
            }
        }

        [TestMethod]
        public void Tc33138_VerifyAddToCartOpensModal()
        {
            // Product Details: Verify the Cart Button Works and Opens Modal
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33138
            // Combined With:
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33247

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Select the desired quantity and click "add to cart"
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsIncreaseProductQuantity));
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsIncreaseProductQuantity));
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsDecreaseProductQuantity));
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToCartButtonId));

            // Step 5
            // Make sure correct amount of product was added and click View Cart button
            Assert.AreEqual(
                (decimal.Parse(this[Ids.Product06Price]) * 2).ToString(MoneyFormat),
                Driver.GetWebElementById(Ids.ProductDetailsAddToCartModalCartTotalId).Text);
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsViewCartHrefButtonId));
        }

        [TestMethod]
        public void Tc33141_VerifyAddToWishListProcess()
        {
            // Product Details: Verify that Items can be Added and Removed from the Wish List
            // Combined with Tc33140
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33141

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Click add to wish list button
            // Not logged in so it should pop the login modal instead of doing the action
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToWishListButtonId));

            // Step 5
            // Sign in on modal and not login button
            Driver.SignIn(Ids.UserName, Ids.Password, false);

            // Step 6
            // Click add to wish list and navigate to wish list in dashboard
            // Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToWishListButtonId)); The resolution from sign in does this
            Driver.GetWebElementById(Ids.ProductDetailsAddToWishListButtonId, displayed: false);
            Driver.GetWebElementById(Ids.ProductDetailsRemoveFromWishListButtonId);
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuUserNameButtonId));
            Thread.Sleep(500);
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuMyDashboardBtn));

            // Step 7
            // Check if product showed up on wish list
            Assert.IsTrue(Driver.ClickById(Ids.UserDashboardSideMenuWishListBtn));
            Assert.IsNotNull(Driver.GetWebElementById(Ids.UserDashboardWishListItem, "0"));

            // Step 8
            // Return to "Allen-Edmonds® Leather Shoe Care Kit" product page
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 9
            // Click Remove from Wish List and navigate to wish list in dashboard
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsRemoveFromWishListButtonId));
            Driver.GetWebElementById(Ids.ProductDetailsRemoveFromWishListButtonId, displayed: false);
            Driver.GetWebElementById(Ids.ProductDetailsAddToWishListButtonId);
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuUserNameButtonId));
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuMyDashboardBtn));

            // Step 10
            // Check if wish list is empty
            Assert.IsTrue(Driver.ClickById(Ids.UserDashboardSideMenuWishListBtn));
            Assert.IsNull(Driver.GetWebElementById(Ids.UserDashboardWishListItem, "0", 0));
        }

        [TestMethod]
        public void Tc33246_VerifyAddToShoppingListProcess()
        {
            // Product Details: Check an item can be added to a shopping list using the add button - signed in
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33246

            // Product Details: Verify the Cart Button Works and Opens Modal
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33138

            var shoppingListName = Guid.NewGuid();

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Click the add to shopping list button, sign in on redirect
            ////Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToShoppingListBtn));
            Driver.SignIn(Ids.UserName, Ids.Password);

            // Step 5
            // Click the add to shopping list button
            // possibly remove this if redirect happens
            ////Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToShoppingListBtn));

            // Step 6
            // Select the proper shopping list and add the product to the shopping list
            Thread.Sleep(1000);
            var shoppingLists = Driver.FindElements(By.Id(this[Ids.ProductDetailsShoppingListSelect]));
            Assert.IsNotNull(shoppingLists);
            if (shoppingLists.Count > 0 && shoppingLists[0].Text.Contains("\r\n"))
            {
                // Add to existing list
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsShoppingListSelect));
                shoppingLists[0].SendKeys(Keys.Down);
                shoppingLists[0].SendKeys(Keys.Return);
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToShoppingListModalBtn));
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsCloseSelectShoppingListModal));
            }
            else
            {
                // Create new shopping list
                Driver.InputTextById(Ids.ProductDetailsWishListCreateTxt, shoppingListName.ToString());
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsSubmitCreateListBtn));
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsShoppingListSelect));
                // ReSharper disable once PossibleNullReferenceException
                shoppingLists[0].SendKeys(Keys.Down);
                shoppingLists[0].SendKeys(Keys.Return);
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToShoppingListModalBtn));
                Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsCloseSelectShoppingListModal));
            }

            // Step 7
            // Go to shopping lists
            Thread.Sleep(500);
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuUserNameButtonId));
            Thread.Sleep(500);
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuShoppingListsBtn));

            // Step 8
            // Click the shopping list details that the item was added to
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsShoppingListDetailsBtn, "0"));

            // Step 9
            // Make sure item is in shopping list
            Assert.AreEqual(
                this[Ids.Product06Name],
                Driver.GetWebElementById(Ids.ProductDetailsShoppingListProductName, "0").Text);

            // Step 10
            // Delete shopping list to clean up
            //TODO Add test logic to test deleting the shopping list here also
        }

        [TestMethod]
        public void Tc33252_VerifyBasicProductInfoIsShown()
        {
            // Product Details: Verify product name, price, SKU, UofM, and Quick Description Text
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33252

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Verify Product Name, SKU, Price, UofM, and Quick Description
            var productName = Driver.GetWebElementById(Ids.ProductDetailsProductName);
            Assert.AreEqual(this[Ids.Product06Name], productName.Text);

            var productCustomKey = Driver.GetWebElementById(Ids.ProductDetailsProductCustomKey);
            Assert.AreEqual(this[Ids.Product06CustomKey], productCustomKey.Text);

            var productPrice = Driver.GetWebElementById(Ids.ProductDetailsProductPrice);
            Assert.AreEqual(decimal.Parse(this[Ids.Product06Price]).ToString(MoneyFormat), productPrice.Text);

            var productUofM = Driver.GetWebElementById(Ids.ProductDetailsProductUofM);
            Assert.AreEqual(this[Ids.Product06UofM], productUofM.Text);

            var productQuickDesc = Driver.GetWebElementById(Ids.ProductDetailsProductQuickDescription);
            Assert.AreEqual(this[Ids.Product06QuickDescription], productQuickDesc.Text);
        }

        [TestMethod, Ignore("Skip-Unreliable: ShareThis doesn't always load and it's beyond our control")]
        public void Tc33253_VerifyShareThisWorks()
        {
            // Product Details: Verify the social network icons redirect to the respective network to share
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33254

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "American Sniper" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product14SeoUrl)));

            // Step 4
            // Ensure buttons exist. Note: Since the buttons are formed via API, the individual btns cannot be IDed
            // If buttons div is not null, the API call worked and buttons exist
            var buttons = Driver.GetWebElementById(Ids.ProductDetailsShareThisOne);
            Assert.IsNotNull(buttons);
        }

        [TestMethod]
        public void Tc33254_VerifyStockInfoIsShown()
        {
            // Product Details: Verify the product stock information is displayed
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33254

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "American Sniper" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product02SeoUrl)));

            // Step 4
            // Check product stock information below the price
            var productStock = Driver.GetWebElementById(Ids.ProductDetailsStock);
            Assert.IsFalse(string.IsNullOrEmpty(productStock.Text));
        }

        [TestMethod]
        public void Tc33255_OutOfStockProductsCantBeAddedToTheCart()
        {
            // Product Details: Verify that When items are out of stock they cannot be added to the cart
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33255

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Energizer 8 pack AA batteries" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product15SeoUrl)));

            // Step 4
            // Add product to cart
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsAddToCartButtonId));

            // Step 5
            // Ensure product was not added to cart
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCartPageUrl()));
            var cartEmptyCheck = Driver.GetWebElementById(Ids.CartEmptyLabelId);
            Assert.IsNotNull(cartEmptyCheck);
        }

        [TestMethod]
        public void Tc33424_ProductTitleLinkAndImageRedirectToProductDetails()
        {
            // Product Details: Check the product title link and the product image redirect to the product detail page
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33424

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Click the first product's thumbnail and verify redirect happened
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsCartProductImageThumbnailId, "0"));
            Assert.AreEqual(GenerateFullProductDetailsPageUrl(Ids.Product07SeoUrl), Driver.Url);

            // Step 5
            // Navigate back to "Allen-Edmonds® Leather Shoe Care Kit"
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 6
            // Click the first product's product name and verify redirect happened
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsCartProductNameId, "0"));
            Assert.AreEqual(GenerateFullProductDetailsPageUrl(Ids.Product07SeoUrl), Driver.Url);
        }

        [TestMethod, Ignore("Skip-Unreliable: Requires the product to always have at least 5 related products")]
        public void Tc33443_ClickingTheLeftRightArrowsChangesWhatIsDisplayed()
        {
            // Product Details: Check the product title link and the product image redirect to the product detail page
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/33443

            // Step 1
            // Navigate to the Catalog

            // Step 2
            // Click the books filter

            // Step 3
            // Select the "Allen-Edmonds® Leather Shoe Care Kit" product
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl)));

            // Step 4
            // Check first related product is not null
            var firstProduct = Driver.GetWebElementById(Ids.ProductDetailsCartProductNameId, "0");
            Assert.IsNotNull(firstProduct);
            var firstProductText = firstProduct.Text;

            // Step 5
            // Click right arrow for related products
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsRelatedProductsScrollRight));

            // Step 6
            // Check next set's first related product for null and not same as first product
            var secondProduct = Driver.GetWebElementById(Ids.ProductDetailsCartProductNameId, "0");
            Assert.IsNotNull(secondProduct);
            Assert.AreNotEqual(firstProductText, secondProduct.Text);

            // Step 7
            // Click left arrow for related products
            Assert.IsTrue(Driver.ClickById(Ids.ProductDetailsRelatedProductsScrollLeft));

            // Step 6
            // Ensure we are back on first set of products
            Assert.AreEqual(
                firstProductText, Driver.GetWebElementById(Ids.ProductDetailsCartProductNameId, "0").Text);
        }
    }
}
