namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33069 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.ProductsMegaMenu_Open();

            //STEP 2:
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33069DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33069DropdownOptionSeeAll);

            //STEP 3:
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();
            clarityCatalog.AddToCompareButtons_click(1);
            variables.Tc33069ProductNameToCompare1 = clarityCatalog.GetTitleAnyoneProduct(1);

            //STEP 4:
            clarityCatalog.WaitUntilJsReady();
            clarityCatalog.AddToCompareButtons_click(2);
            variables.Tc33069ProductNameToCompare2 = clarityCatalog.GetTitleAnyoneProduct(1);

            clarityCatalog.WaitUntilJsReady();
            Assert.IsTrue(clarityCatalog.CompareTableProductsFinder_checkVisibility(variables.Tc33069ProductNameToCompare1)
                && clarityCatalog.CompareTableProductsFinder_checkVisibility(variables.Tc33069ProductNameToCompare2));
        }
    }
}
