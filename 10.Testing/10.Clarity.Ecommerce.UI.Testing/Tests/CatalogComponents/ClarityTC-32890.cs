namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc32890 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc32890DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc32890DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            clarityBookCatalog.filterPricesRangeDropdown_click();
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.filterPricesRangeOption_click(2);
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.topButtonsPageNumbers_moveToPage(2);
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.topButtonsPageNumbers_moveToPage(1);
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.filterPricesRangeRemove_click(1);
        }
    }
}
