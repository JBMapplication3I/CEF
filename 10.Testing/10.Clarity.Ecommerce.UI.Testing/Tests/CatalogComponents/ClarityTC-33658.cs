namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33658 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StarTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33658DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33658DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            clarityBookCatalog.SelectSortBy_changeToPriceAscending();

            Assert.IsTrue(clarityBookCatalog.CheckIfBookSortByPricesAscending());
        }
    }
}
