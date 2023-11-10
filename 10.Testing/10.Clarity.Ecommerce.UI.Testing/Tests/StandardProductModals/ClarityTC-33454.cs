namespace StandardProductModals
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33454 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33454DropdownOption);

            //STEP 2:
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33454DropdownOptionSeeAll);
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();

            //STEP 3:
            clarityCatalog.AnyoneProductTitle_click(1);

            //STEP 4:
            var clarityProductDetails = new ClarityProductDetailsPage(driver);
            clarityProductDetails.WaitUntilPageLoads();
            clarityProductDetails.addACartButton_click();

            //STEP 5:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.ContinuesShoppingButton_click();

            //STEP 6:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.increaseAmountButton_click();
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.increaseAmountButton_click();
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.addACartButton_click();

            //STEP 7:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.AddToCartWindow_clickOutside();

            //STEP 8:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.increaseAmountButton_click();
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.increaseAmountButton_click();
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.addACartButton_click();

            //STEP 9:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.ContinuesShoppingButton_click();
            clarityProductDetails.WaitUntilJsReady();

            Assert.IsFalse(clarityProductDetails.AddToCartWindow_CheckIfDisplayed());
        }
    }
}
