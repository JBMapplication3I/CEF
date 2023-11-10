namespace Clarity.Ecommerce.UI.Testing.Extensions
{
    using OpenQA.Selenium;

    public static class WebElementExtensions
    {
        public static bool HasClass(this IWebElement element, string className)
        {
            return element.GetAttribute("class").Contains(className);
        }
    }
}
