namespace CatalogComponents
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33067 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.loginButton_click();
            clarityIndex.WaitUntilJsReady();
            clarityIndex.userNameInput_setText(variables.UserName);
            clarityIndex.userPasswordInput_setText(variables.Password);
            clarityIndex.WaitUntilJsReady();
            clarityIndex.signInButton_click();
            clarityIndex.WaitUntilJsReady();
            clarityIndex.ProductsMegaMenu_Open();
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33067DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33067DropdownOptionSeeAll);

            //STEP 2:
            var clarityBookCatalog = new ClarityCatalogPage(driver);
            clarityBookCatalog.WaitUntilPageLoads();
            clarityBookCatalog.AddToWishListButtons_Click(1);
            clarityBookCatalog.WaitUntilJsReady();
            clarityBookCatalog.removeWishListButton_click(1);
            clarityBookCatalog.WaitUntilJsReady();
        }
    }
}
