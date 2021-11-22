using CodilityTest.Configs;
using CodilityTest.Data;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodilityTest.PageObject
{
    public class HomePage
    {
        public IWebDriver _driver = TestSetup._driver;
        #region PageObject
        public IWebElement lstCategories { get; set; }
        public string lstCategoriesXpath = "//a[contains(@class,'categories-menu')]";

        public IList<IWebElement> lstCategoriesMenu { get; set; }
        public string lstCategoriesMenuXpath = "//ul[@id='menu-categories-menu']//a";

        public IWebElement icnCart { get; set; }
        public string icnCartXpath = @"//div[contains(@class,'header-right')]//a[@title='{0}']";

        public IWebElement txtPageHeaderTitle { get; set; }
        public string txtPageTitleClassName = "page-title";

        public IWebElement txtCartTitle { get; set; }
        public string txtCartTitleClassName = "single-title";

        public IList<IWebElement> lstProductName { get; set; }
        public string lstProductNameXpath = "//li[contains(@class,'product')]//h2";

        public IList<IWebElement> btnProductNameButton { get; set; }
        public string btnProductNameButtonXpath = "//li[contains(@class,'product')]//a[contains(@class,'button')]";

        public IList<IWebElement> tblCartItems { get; set; }
        public string tblCartItemsXpath = @"//tr[contains(@class,'cart_item')]/td[@class='product-{0}']";

        public IWebElement imgLogo { get; set; }
        public string imgLogoClassName = "custom-logo-link";

        public IWebElement txtSearch { get; set; }
        public string txtSearchClassName = "header-search-input";

        public IWebElement lstMenuItems { get; set; }
        public string lstMenuItemsID = "menu-main-menu";
        #endregion

        /// <summary>
        /// Constructor to assign web elements
        /// </summary>
        public HomePage()
        {
            lstCategories = _driver.FindElement(By.XPath(lstCategoriesXpath));
            imgLogo = _driver.FindElement(By.ClassName(imgLogoClassName));
            txtSearch = _driver.FindElement(By.ClassName(txtSearchClassName));
            lstMenuItems = _driver.FindElement(By.Id(lstMenuItemsID));
        }

        /// <summary>
        /// Method to validate home page
        /// </summary>
        public void Validate()
        {
            Assert.IsTrue(lstCategories.Displayed, "[Error]:lstCategories not displayed");
            Assert.IsTrue(imgLogo.Displayed, "[Error]:imgLogo not displayed");
            Assert.IsTrue(txtSearch.Displayed, "[Error]:txtSearch not displayed");
            Assert.IsTrue(lstMenuItems.Displayed, "[Error]:lstMenuItems not displayed");
            TestContext.Out.WriteLine("[PASS]:Home Page validation successfull");
        }

        /// <summary>
        /// Method to mouse over and choose Category item
        /// </summary>
        /// <param name="menuItemName">Category name</param>
        public void ChooseCategory(string menuItemName)
        {
            try
            {
                Actions action = new Actions(_driver);
                action.MoveToElement(lstCategories);
                action.Click().Build().Perform();
                var subMenu = _driver.FindElements(By.XPath(lstCategoriesMenuXpath));
                menuItemName = TestData._menuItem.Where(X => X.Equals(menuItemName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                foreach (var item in subMenu)
                {
                    if (item.Text.Equals(menuItemName))
                    {
                        action.MoveToElement(item);
                        action.Click().Build().Perform();
                        break;
                    }
                }
                TestContext.Out.WriteLine("[PASS]:Element '" + menuItemName + "' is clicked");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Element'" + menuItemName + "' not clicked");
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Method to Validate Browser title
        /// </summary>
        /// <param name="title"></param>
        public void ValidatePageTitle(string title)
        {
            string expected = TestData._pageTitle.Where(X => X.Equals(title, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            try
            {
                Assert.AreEqual(_driver.Title, expected);
                TestContext.Out.WriteLine("[PASS]:Page Title are equal");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Page Title\nExpected[Clothing] but Actual [" + _driver.Title + "]");
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Method to validate selected header title
        /// </summary>
        /// <param name="name">Name of the header item</param>
        public void ValidateSelectedHeaderTitle(string name)
        {
            string expected = TestData._menuItem.Where(X => X.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            expected = string.IsNullOrEmpty(expected) ? TestData._headerRightIcons.Where(X => X.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() : expected;
            try
            {
                try
                {
                    txtPageHeaderTitle = _driver.FindElement(By.ClassName(txtPageTitleClassName));
                }
                catch (Exception)
                {
                    txtPageHeaderTitle = _driver.FindElement(By.ClassName(txtCartTitleClassName));
                }
                Assert.AreEqual(txtPageHeaderTitle.Text, expected);
                TestContext.Out.WriteLine("[PASS]:Selected Category Title [" + name + "] are equal");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Validation\nExpected[" + name + "] but Actual [" + txtPageHeaderTitle.Text + "]");
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Method to Click on Add to cart button based on Product name
        /// </summary>
        /// <param name="productName">Name of the product</param>
        public void ClickAddToCart(string productName)
        {
            try
            {
                lstProductName = _driver.FindElements(By.XPath(lstProductNameXpath));
                foreach (var item in lstProductName)
                {
                    if (item.Text.Equals(productName, StringComparison.OrdinalIgnoreCase))
                    {
                        item.FindElement(By.XPath("//a[@aria-label='Add “" + productName + "” to your cart']")).Click();
                        TestContext.Out.WriteLine("[PASS]:Button [AddToCart ] is clicked for [" + productName + "]");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Button [AddToCart ] is not clicked for [" + productName + "]");
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Method to click on Right side header icon
        /// </summary>
        /// <param name="iconName">Icon name</param>
        public void ClickHeaderIcon(string iconName)
        {
            var _icon = TestData._headerRightIcons.Where(X => X.Equals(iconName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            try
            {
                icnCart = _driver.FindElement(By.XPath(string.Format(icnCartXpath, _icon)));
                icnCart.Click();
                TestContext.Out.WriteLine("[PASS]:Header Icon [" + iconName + "] is clicked");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Icon [" + iconName + "] is not clicked");
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Method to valide the Cart items based on Column name passed in paramater
        /// </summary>
        /// <param name="columnName">Name of the column</param>
        public void ValidateProductsInCart(IList<string> columnName)
        {
            string _className = TestData._cartTableClassName.Where(X => X.Equals("name", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            try
            {
                tblCartItems = _driver.FindElements(By.XPath(string.Format(tblCartItemsXpath, _className)));
                var actual = tblCartItems.Select(X => X.Text);
                var expected = columnName.Cast<string>();
                Assert.IsTrue(expected.SequenceEqual(actual));
                TestContext.Out.WriteLine("[PASS]:Products are present in cart list");
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("[FAIL]:Products are not present in cart list");
                Assert.Fail(ex.Message);
            }
        }
    }
}
