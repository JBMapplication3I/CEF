namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33058 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndexPage = new ClarityIndexPage(driver);
            clarityIndexPage.WaitUntilPageLoads();
            clarityIndexPage.ProductsMegaMenu_Open();
            clarityIndexPage.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33058DropdownOption);
            clarityIndexPage.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33058DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            variables.ProductTitle = clarityBookCatalog.GetTitleAnyoneProduct(1); //GET NAME OF THE FIRST PRODUCT
            clarityBookCatalog.AnyoneProductTitle_click(1); //SELECT IN FIRST PRODUCT ON THE LIST

            //STEP 3:
            var clarityBooksDetails = new ClarityProductDetailsPage(driver);
            clarityBooksDetails.WaitUntilPageLoads();

            Assert.AreEqual(variables.ProductTitle, clarityBooksDetails.ReturnBookTitle());
        }
    }
}
