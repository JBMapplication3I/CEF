namespace ClarityTC.pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    internal class ClarityCatalogPage : Pages
    {
        private const string SelectSortByXpath = "//*[contains(@ng-if, \"searchCatalogResultsControlBarCtrl.hrPosition == 'bottom'\")]/..//select[@id='resultsSortPicker']";
        private const string BooksAllPricesXpath = "//span[contains(@class,'very-big')]";
        private const string TopButtonsPageNumbersXpath = "//*[contains(@ng-if, \"hrPosition == 'bottom'\")]/..//*[contains(@ng-repeat,\"searchCatalogResultsPageCtrl\")]/button";
        private const string TopOptionsShowItemsXpath = "//*[contains(@ng-if, \"hrPosition == 'bottom\")]/..//select[contains(@ng-model,'searchCatalogResultsPerPageCtrl')]";
        private const string FilterPricesRangeDropdownXpath = "//div[@class='panel-heading']/descendant::*[contains(text(),'Price Ranges')]";
        private const string FilterPricesRangeAllOptionsXpath = "//a[contains(@*,'searchCatalogFilterByPriceRangesCtrl')]";
        private const string FilterPricesRangeRemoveXpath = "//div[contains(@ng-repeat,'searchCatalogFiltersAppliedCtrl')]/child::a";
        private const string FilterBookRemoveXpath = "//a[@class='text-capitalize ng-binding']";
        private const string FilterCategoriesDropdownXpath = "//div[@class='panel-heading']/descendant::*[contains(text(),'Categories')]";
        private const string FilterCategoriesDropdownExpandButtonXpath = "//a[@ng-init='cat1.showChildren = true;']";
        private const string CatalogLevel1PageXpath = "//*[contains(@*,'level1')]";
        private const string AllProductTitlesLinkXpath = "//p[@ng-bind='productCardNameWidgetCtrl.product.Name']/..";
        private const string AllProductTitlesXpath = "//p[@ng-bind='productCardNameWidgetCtrl.product.Name']";
        private const string AllProductImageXpath = "//a[@class='product-image']";
        private const string ChangeToListViewButtonXpath = "//*[contains(@ng-if, \"hrPosition == 'bottom\")]/..//descendant::*[contains(text(),'list')]/..";
        private const string ChangeToGridViewButtonXpath = "//*[contains(@ng-if, \"hrPosition == 'bottom\")]/..//descendant::*[contains(text(),'grid')]/..";
        private const string AddToWishListButtonsXpath = "//button[contains(@ng-click,'productCardControlsWidgetCtrl.addToWishList')]";
        private const string RemoveWishListButtonXpath = "//button[contains(@ng-click,'productCardControlsWidgetCtrl.removeFromWishList')]";
        private const string AddToCompareButtonsXpath = "//button[contains(@ng-click,'productCardControlsWidgetCtrl.addToCompare')]";
        private const string CompareTableProductsFinderXpath = "//div[contains(@ng-if,'searchCatalogCompareCartCtrl.$state.includes')]/descendant::*[contains(text(),'____')]";

        private readonly IWebElement selectSortBy;
        private IWebElement? filterPricesRangeDropdown;
        private IWebElement? filterBookRemove;
        private IWebElement? topOptionsShowItems;
        private IWebElement? filterCategoriesDropdown;
        private IWebElement? filterCategoriesDropdownExpandButton;
        private IWebElement? changeToListViewButton;
        private IWebElement? changeToGridViewButton;
#pragma warning disable 169
        private IWebElement? compareTableProductsFinder;
#pragma warning restore 169

        private List<IWebElement>? catalogLevel1Page;
        private List<IWebElement>? filterPricesRangeRemove;
        private List<IWebElement>? filterPricesRangeAllOptions;
        private List<IWebElement>? allProductLinks;
        private List<IWebElement>? allProductTitles;
        private List<IWebElement>? allProductImage;
        private List<IWebElement>? topButtonsPageNumbers;
        private List<IWebElement>? addToWishListButtons;
        private List<IWebElement>? removeWishListButton;
        private List<IWebElement>? addToCompareButtons;

        public ClarityCatalogPage(IWebDriver driver) : base(driver)
        {
            allProductLinks = driver.FindElements(By.XPath(AllProductTitlesLinkXpath)).ToList();
            allProductTitles = driver.FindElements(By.XPath(AllProductTitlesXpath)).ToList();
            allProductImage = driver.FindElements(By.XPath(AllProductImageXpath)).ToList();
            selectSortBy = driver.FindElement(By.XPath(SelectSortByXpath));
        }

        public bool CompareTableProductsFinder_checkVisibility(string productName)
        {
            var temp = CompareTableProductsFinderXpath.Replace("____", productName);
            try
            {
                return Driver.FindElement(By.XPath(temp)).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddToCompareButtons_click(int product)
        {
            addToCompareButtons = Driver.FindElements(By.XPath(AddToCompareButtonsXpath)).ToList();
            addToCompareButtons[product - 1].Click();
        }

        public void SelectSortBy_changeToPriceAscending()
        {
            var selectElement = new SelectElement(selectSortBy);
            selectElement.SelectByText("Price Ascending");
        }

        public void SelectSortBy_changeToNameAscending()
        {
            var selectElement = new SelectElement(selectSortBy);
            selectElement.SelectByText("Name Ascending");
        }

        public List<IWebElement> SelectAllBooksPrices()
        {
            WaitUntilPageLoads();
            var getAllBooksPrices = Driver.FindElements(By.XPath(BooksAllPricesXpath)).ToList();
            return getAllBooksPrices;
        }

        public void filterPricesRangeDropdown_click()
        {
            filterPricesRangeDropdown = Driver.FindElement(By.XPath(FilterPricesRangeDropdownXpath));
            filterPricesRangeDropdown.Click();
        }

        public void filterPricesRangeOption_click(int optionsSelected)
        {
            filterPricesRangeAllOptions = Driver.FindElements(By.XPath(FilterPricesRangeAllOptionsXpath)).ToList();
            filterPricesRangeAllOptions[optionsSelected].Click();
        }

        public void filterPricesRangeRemove_click(int options)
        {
            filterPricesRangeRemove = WaitToAllBeVisbleByXpath(Driver, FilterPricesRangeRemoveXpath, 10);
            filterPricesRangeRemove[options - 1].Click();
        }

        public void filterBookRemove_click()
        {
            filterBookRemove = WaitToBeClickEnableByXpath(Driver, FilterBookRemoveXpath, 100);
            filterBookRemove.Click();
        }

        public bool CheckIfBookSortByPricesAscending()
        {
            var allBookPrices = SelectAllBooksPrices(); //GET ALL BOOKS PRICES
            var firstBooksPricesString = allBookPrices.First().GetAttribute("innerHTML").Split('$');
            var firstBooksPrices = Convert.ToDouble(firstBooksPricesString[1]);

            foreach (var webElement in allBookPrices.ToList())
            {
                var secondBooksPricesString = webElement.GetAttribute("innerHTML").Split('$'); //SPLIT THE STRING TO REMOVE $
                var secondBooksPrices = Convert.ToDouble(secondBooksPricesString[1]); //CHANGE TYPE TO DOUBLE
                if (firstBooksPrices <= secondBooksPrices)
                {
                    firstBooksPrices = secondBooksPrices;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void AnyoneProductTitle_click(int product)
        {
            allProductLinks = Driver.FindElements(By.XPath(AllProductTitlesLinkXpath)).ToList();
            allProductLinks[product - 1].Click();
        }
        public void AnyoneProductImage_click(int product)
        {
            allProductImage = Driver.FindElements(By.XPath(AllProductImageXpath)).ToList();
            allProductImage[product - 1].Click();
        }

        public string GetTitleAnyoneProduct(int product)
        {
            allProductTitles = Driver.FindElements(By.XPath(AllProductTitlesXpath)).ToList();
            return allProductTitles[product - 1].GetAttribute("innerHTML");
        }

        public bool CheckIfCatalogInLevel1()
        {
            catalogLevel1Page = WaitToAllBeVisbleByXpath(Driver, CatalogLevel1PageXpath, 10);
            return catalogLevel1Page.Any();
        }

        public void filterCategoriesDropdown_click()
        {
            filterCategoriesDropdown = Driver.FindElement(By.XPath(FilterCategoriesDropdownXpath));
            filterCategoriesDropdown.Click();
        }

        public void filterCategoriesDropdownExpandButton_click()
        {
            filterCategoriesDropdownExpandButton = Driver.FindElement(By.XPath(FilterCategoriesDropdownExpandButtonXpath));
            filterCategoriesDropdownExpandButton.Click();
        }

        public void topButtonsPageNumbers_moveToPage(int pageNumber)
        {
            topButtonsPageNumbers = WaitToAllBeVisbleByXpath(Driver, TopButtonsPageNumbersXpath, 10);
            topButtonsPageNumbers[pageNumber - 1].Click();
        }

        public void topOptionsShowItems_change(string option)
        {
            topOptionsShowItems = Driver.FindElement(By.XPath(TopOptionsShowItemsXpath));
            var selectElement = new SelectElement(topOptionsShowItems);
            selectElement.SelectByText(option);
        }

        public bool CheckQuantityOfItemOnScreen(int quantity)
        {
            allProductTitles = Driver.FindElements(By.XPath(AllProductTitlesXpath)).ToList();
            return allProductTitles.Count <= quantity;
        }

        public void changeToListViewButton_click()
        {
            changeToListViewButton = Driver.FindElement(By.XPath(ChangeToListViewButtonXpath));
            changeToListViewButton.Click();
        }

        public void changeToGridViewButton_click()
        {
            changeToGridViewButton = Driver.FindElement(By.XPath(ChangeToGridViewButtonXpath));
            changeToGridViewButton.Click();
        }

        public void AddToWishListButtons_Click(int product)
        {
            addToWishListButtons = Driver.FindElements(By.XPath(AddToWishListButtonsXpath)).ToList();
            addToWishListButtons[product - 1].Click();
        }

        public void removeWishListButton_click(int product)
        {
            removeWishListButton = Driver.FindElements(By.XPath(RemoveWishListButtonXpath)).ToList();
            removeWishListButton[product - 1].Click();
        }

        public bool CheckIfProductTitleAsSortNameAscending()
        {
            allProductTitles = Driver.FindElements(By.XPath(AllProductTitlesXpath)).ToList();
            var listToCheck = new List<string>();
            var listToCheck2 = new List<string>();
            foreach (var str in allProductTitles)
            {
                listToCheck.Add(str.GetAttribute("innerHTML"));
                listToCheck2.Add(str.GetAttribute("innerHTML"));
            }
            listToCheck2.Sort();
            for (var x = 0; x < listToCheck.Count; x++)
            {
                if (listToCheck[x] != listToCheck2[x])
                {
                    return false;
                }
            }
            return true;
        }
    }
}