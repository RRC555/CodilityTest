using System.Collections.Generic;

namespace CodilityTest.Data
{
    /// <summary>
    /// Class to provide the TestData
    /// </summary>
    public class TestData
    {
        public static IList<string> _menuItem = new List<string>(){ "Clothing", "Women's Clothing", "Men's Clothing", "Watches" };
        public static IList<string> _pageTitle = new List<string>(){ "Clothing – TESTSCRIPTDEMO", "Women’s Clothing – TESTSCRIPTDEMO", "Men’s Clothing – TESTSCRIPTDEMO", "Watches – TESTSCRIPTDEMO" };
        public static IList<string> _headerRightIcons = new List<string>(){ "Compare", "Wishlist", "My Account", "Cart" };
        public static IList<string> _cartTableClassName = new List<string>(){ "remove", "thumbnail", "name", "price","quantity","subtotal" };
    }
}
