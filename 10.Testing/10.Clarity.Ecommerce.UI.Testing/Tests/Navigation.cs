namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using System.Drawing;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Navigation : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void Verify_MegaMenuNavigation()
        {
            // Needs extra height to ensure chosen menu item isn't below the fold
            Driver.Manage().Window.Size = new Size(1920, 2160);
            Assert.IsTrue(Driver.GoToPageByUrl(Ids.BaseUrl));
            Assert.IsTrue(Driver.GoToProductCategoryFromCategoriesMenu_Mega_Desktop(Ids.Category1Index));
            Assert.AreEqual(
                this[Ids.BaseUrl]
                + string.Format(
                    this[Ids.CategoryPageRelativeUrlFormat],
                    this[Ids.Category1Name],
                    this[Ids.Category1CustomKey]),
                Driver.Url);
        }
    }
}
