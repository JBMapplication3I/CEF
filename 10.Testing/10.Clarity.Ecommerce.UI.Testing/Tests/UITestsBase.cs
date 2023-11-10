namespace Clarity.Ecommerce.UI.Testing
{
    using ClarityTC.pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    [TestClass, TestCategory("UI Tests")]
    public abstract class UITestsBase
    {
        protected IWebDriver driver = null!;
        protected Variables variables = null!;

        [TestInitialize]
        public void SetUp()
        {
            variables = new();
            driver = new ChromeDriver();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Position = new(0, 0);
            driver.Manage().Window.Size = new(1920, 1080);
            driver.Url = variables.ClarityIndexUrl;
        }

        [TestCleanup]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}
