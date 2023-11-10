namespace ClarityTC.pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    internal class ClarityIndexPage : Pages
    {
        private const string ProductsDropdownXpath = "//div[@id='headerBot']/descendant::a[text()='Products']";
        private const string ProductsMegaMenu_LeftSide_Item_SelectXPath = "//ul[contains(@class,'nav nav-tabs nav-stacked')]//descendant::a[contains(*,'____')]";
        private const string ProductsMegaMenu_RightSide_SeeAllButton_SelectXPath = "//div[@id='headerBot']/descendant::a[contains(@uisrp-params,\"{ 'category': '____\")]";
        private const string UserNameInputID = "txtUserName";
        private const string UserPasswordInputID = "pwPassword";
        private const string LoginButtonXpath = "//button[contains(@ng-click,'loginModalCtrl.open')]";
        private const string SignInButtonXpath = "//button[contains(@ng-click,'loginCtrl.loginUser')]";

        private IWebElement? productsDropdown;
#pragma warning disable 169
        private IWebElement? productsDropdownOptionBook;
        private IWebElement? productsDropdownOptionBookSeeAll;
        private IWebElement? productsDropdownOptionElectronicsSeeAll;
        private IWebElement? productsDropdownOptionComputer;
        private IWebElement? productsDropdownOptionComputerSeeAll;
        private IWebElement? productDropdownOptionSelect;
        private IWebElement? productDropdownOptionSelectSeeAll;
#pragma warning restore 169
        private IWebElement? loginButton;
        private IWebElement? userNameInput;
        private IWebElement? userPasswordInput;
        private IWebElement? signInButton;

        private readonly Actions actions;

        public ClarityIndexPage(IWebDriver driver) : base(driver)
        {
            actions = new Actions(driver);
        }

        public void ProductsMegaMenu_LeftSide_Item_Click(string category)
        {
            var xPath = ProductsMegaMenu_LeftSide_Item_SelectXPath.Replace("____", category);
            var webElement = Driver.FindElement(By.XPath(xPath));
            System.Threading.Thread.Sleep(1000);
            actions.MoveToElement(webElement).Build().Perform();
            System.Threading.Thread.Sleep(1000);
        }

        public void ProductsMegaMenu_RightSide_SeeAllButton_Click(string subCategory)
        {
            var xPath = ProductsMegaMenu_RightSide_SeeAllButton_SelectXPath.Replace("____", subCategory);
            var webElement = Driver.FindElement(By.XPath(xPath));
            webElement.Click();
        }

        public void ProductsMegaMenu_Open()
        {
            productsDropdown = Driver.FindElement(By.XPath(ProductsDropdownXpath));
            productsDropdown.Click();
        }

        public void loginButton_click()
        {
            loginButton = Driver.FindElement(By.XPath(LoginButtonXpath));
            loginButton.Click();
        }

        public void userPasswordInput_setText(string password)
        {
            userPasswordInput = Driver.FindElement(By.Id(UserPasswordInputID));
            userPasswordInput.SendKeys(password);
        }

        public void userNameInput_setText(string userName)
        {
            userNameInput = Driver.FindElement(By.Id(UserNameInputID));
            userNameInput.SendKeys(userName);
        }

        public void signInButton_click()
        {
            signInButton = Driver.FindElement(By.XPath(SignInButtonXpath));
            signInButton.Click();
        }

        public void FullSignIn(string userName, string password)
        {
            loginButton_click();
            WaitUntilJsReady();
            userNameInput_setText(userName);
            userPasswordInput_setText(password);
            signInButton_click();
            WaitUntilPageLoads();
        }
    }
}
