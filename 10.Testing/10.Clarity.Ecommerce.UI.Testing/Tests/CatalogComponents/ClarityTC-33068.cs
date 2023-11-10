namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33068 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndexPage = new ClarityIndexPage(driver);
            clarityIndexPage.WaitUntilPageLoads();
            clarityIndexPage.ProductsMegaMenu_Open();
            clarityIndexPage.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33068DropdownOption);
            clarityIndexPage.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33068DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            clarityBookCatalog.AnyoneProductImage_click(1); //CLICK ON THE FIRST PRODUCT

            //STEP 3:
            var clarityBooksDetails = new ClarityProductDetailsPage(driver);
            clarityBooksDetails.WaitUntilPageLoads();
            driver.Navigate().Back();

            //STEP 4:
            clarityBookCatalog.WaitUntilPageLoads();
            variables.ProductTitle = clarityBookCatalog.GetTitleAnyoneProduct(3); //GET THE TITLE OF THE SECOND PRODUCT
            clarityBookCatalog.AnyoneProductImage_click(3); //CLICK IN SECOND PRODUCT

            //STEP 5:
            clarityBooksDetails.WaitUntilPageLoads();
            Assert.AreEqual(variables.ProductTitle, clarityBooksDetails.ReturnBookTitle());
        }
    }
}
