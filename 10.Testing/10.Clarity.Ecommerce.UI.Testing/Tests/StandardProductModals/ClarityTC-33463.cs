namespace StandardProductModals
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33463 : UITestsBase
    {
        [TestMethod, Ignore("Skip: Must be converted")]
        public void StartTest()
        {
            //STEP 1:
            var clarityIndex = new ClarityIndexPage(driver);
            clarityIndex.WaitUntilPageLoads();
            clarityIndex.FullSignIn(variables.UserName, variables.Password);
            clarityIndex.ProductsMegaMenu_Open();

            //STEP 2:
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33463DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33463DropdownOptionSeeAll);

            //STEP 3:
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();
            clarityCatalog.AnyoneProductTitle_click(1); //Select a brief history of time

            //STEP 4:
            var clarityProductDetails = new ClarityProductDetailsPage(driver);
            clarityProductDetails.WaitUntilPageLoads();
            clarityProductDetails.AddToShoppingList_click();

            //STEP 5:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.ShoppingListCreateListButton_click();

            //STEP 6:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.ShoppingListCreateNewInput_sendText(variables.Tc33463NameNewList);

            //STEP 7:
            clarityProductDetails.ShoppingListSaveListButton_click();
        }
    }
}
