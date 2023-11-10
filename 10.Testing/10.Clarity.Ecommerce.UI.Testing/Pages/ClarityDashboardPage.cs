namespace ClarityTC.pages
{
    using OpenQA.Selenium;

    internal class ClarityDashboardPage : Pages
    {
        private const string ShoppingListButtonXpath = "//button[@ui-sref='userDashboard.shoppingLists.list']";
        private string shoppingListSelectWhicheverXpath = "//button[@ui-sref=\"userDashboard.shoppingLists.detail({ ID: '____', Name: '____' })\"]";
        private string shoppingListSelectFindProductXpath = "//label[contains(text(),'____')]";

        private IWebElement? shoppingListButton;
        private IWebElement? shoppingListSelectWhichever;
#pragma warning disable 169
        private IWebElement? shoppingListSelectFindProduct;
#pragma warning restore 169

        public ClarityDashboardPage(IWebDriver driver) : base(driver)
        {
            //Nothing for now
        }

        public void ShoppingListButton_click()
        {
            shoppingListButton = Driver.FindElement(By.XPath(ShoppingListButtonXpath));
            shoppingListButton.Click();
        }

        public void shoppingListSelectWhichever_click(string shoppingListSelected)
        {
            shoppingListSelectWhicheverXpath = shoppingListSelectWhicheverXpath.Replace("____", shoppingListSelected);
            shoppingListSelectWhichever = Driver.FindElement(By.XPath(shoppingListSelectWhicheverXpath));
            shoppingListSelectWhichever.Click();
        }

        public bool shoppingListSelectedFindProduct_checkVisibility(string productName)
        {
            shoppingListSelectFindProductXpath = shoppingListSelectFindProductXpath.Replace("____", productName);
            return Driver.FindElement(By.XPath(shoppingListSelectFindProductXpath)).Displayed;
        }
    }
}
