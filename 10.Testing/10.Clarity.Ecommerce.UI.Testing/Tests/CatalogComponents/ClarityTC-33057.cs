namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33057 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();

            //STEP 2:
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33057DropdownOptions);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33057DropdownOptionsSeeAll);

            //STEP 3:
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();
            variables.ProductTitle = clarityCatalog.GetTitleAnyoneProduct(1);
            clarityCatalog.AnyoneProductTitle_click(1);

            var clarityProductDetails = new ClarityProductDetailsPage(driver);
            clarityProductDetails.WaitUntilPageLoads();
            Assert.AreEqual(clarityProductDetails.ReturnBookTitle(), variables.ProductTitle);
        }
    }
}
