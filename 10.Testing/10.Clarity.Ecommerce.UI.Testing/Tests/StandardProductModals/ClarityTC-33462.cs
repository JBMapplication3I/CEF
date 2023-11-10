namespace StandardProductModals
{
    using Clarity.Ecommerce.UI.Testing;
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Tc33462 : UITestsBase
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
            clarityIndex.ProductsMegaMenu_LeftSide_Item_Click(variables.Tc33462DropdownOption);
            clarityIndex.ProductsMegaMenu_RightSide_SeeAllButton_Click(variables.Tc33462DropdownOptionSeeAll);

            //STEP 3:
            var clarityCatalog = new ClarityCatalogPage(driver);
            clarityCatalog.WaitUntilPageLoads();
            variables.ProductTitle = clarityCatalog.GetTitleAnyoneProduct(3); //Book - beautiful testing
            clarityCatalog.AnyoneProductTitle_click(3); //Book - beautiful testing

            //STEP 4:
            var clarityProductDetails = new ClarityProductDetailsPage(driver);
            clarityProductDetails.WaitUntilPageLoads();
            clarityProductDetails.AddToShoppingList_click();

            //STEP 5:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.SelectAShoppingList_selectOption(2); //SELECT OPTION 2 IN DISPLAYED LIST
            clarityProductDetails.WaitUntilJsReady();
            variables.OptionSelected = clarityProductDetails.SelectAShoppingList_getText();

            //STEP 6:
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.AddToShoppingListButton_click();
            clarityProductDetails.WaitUntilJsReady();
            clarityProductDetails.AddToShoppingListModal_clickOutside(); //CLOSE MODAL

            //STEP 7:
            clarityProductDetails.myAccountButton_click();
            clarityProductDetails.myDashboard_click();
            var clarityDashboard = new ClarityDashboardPage(driver);
            clarityDashboard.WaitUntilPageLoads();
            clarityDashboard.ShoppingListButton_click();
            clarityDashboard.WaitUntilJsReady();
            clarityDashboard.shoppingListSelectWhichever_click(variables.OptionSelected);

            //STEP 8:
            clarityDashboard.WaitUntilJsReady();
            Assert.IsTrue(clarityDashboard.shoppingListSelectedFindProduct_checkVisibility(variables.ProductTitle));
        }
    }
}
