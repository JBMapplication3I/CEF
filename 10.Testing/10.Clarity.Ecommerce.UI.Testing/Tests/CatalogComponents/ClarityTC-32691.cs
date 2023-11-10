namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc32691 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc32691DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc32691DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            clarityBookCatalog.filterCategoriesDropdown_click();
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.filterCategoriesDropdownExpandButton_click();
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.filterCategoriesDropdownExpandButton_click();
        }
    }
}
