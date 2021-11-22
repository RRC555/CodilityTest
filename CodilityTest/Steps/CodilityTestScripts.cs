using CodilityTest.Configs;
using CodilityTest.PageObject;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace CodilityTest.Steps
{
    class CodilityTestScripts
    {
        public IWebDriver _driver = null;
        [SetUp]
        public void Setup()
        {
            _driver = TestSetup.Launch();
        }
        [TearDown]
        public void cleanup()
        {
            _driver.Quit();
        }

        [Test]
        public void SC001_AddDifferentProductsToWishlist()
        {
            IList<string> productName = new List<string>() { "Black trousers", "Single Shirt", "Bikini","Hard top"};
            var homePage = new HomePage();
            homePage.Validate();
            homePage.ChooseCategory("CLOTHING");
            homePage.ValidatePageTitle("CLOTHING – TESTSCRIPTDEMO");
            homePage.ValidateSelectedHeaderTitle("CLOTHING");
            foreach (var item in productName)
            {
                homePage.ClickAddToCart(item);
            }
            homePage.ClickHeaderIcon("CART");
            homePage.ValidateSelectedHeaderTitle("CART");
            homePage.ValidateProductsInCart(productName);
        }

        [Test]
        public void SC002_AddLowestPriceProductToWishlist()
        {
            var homePage = new HomePage();
            homePage.Validate();
            homePage.ChooseCategory("WATCHES");
            homePage.ValidatePageTitle("WATCHES – TESTSCRIPTDEMO");
            homePage.ValidateSelectedHeaderTitle("WATCHES");
            homePage.ClickAddToCart("Modern");
            homePage.ClickHeaderIcon("CART");
            homePage.ValidateSelectedHeaderTitle("CART");
            homePage.ValidateProductsInCart(new List<string>() { "Modern" });
        }
    }
}
