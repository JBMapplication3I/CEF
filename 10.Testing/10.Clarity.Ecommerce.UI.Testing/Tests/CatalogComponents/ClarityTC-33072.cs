namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33072 : UITestsBase
    {
        [TestMethod, Ignore("Must Convert")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();

            //STEP 2:
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33072DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33072DropdownOptionSeeAll);

            //STEP 3:
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();
            clarityCatalog.changeToListViewButton_click();

            //STEP 4:
            clarityCatalog.WaitUntilJsReady();
            clarityCatalog.changeToGridViewButton_click();
        }
    }
}
