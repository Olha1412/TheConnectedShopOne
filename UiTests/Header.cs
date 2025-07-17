using Microsoft.VisualBasic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;
using System.Threading;

namespace theConnectedShop.UiTests
{
    [TestFixture]
    public class Header
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.Navigate().GoToUrl("https://theconnectedshop.com/");
            _wait.Until(driver => driver.Url.StartsWith("https://theconnectedshop.com"));
            _wait.Until(drv => drv.Title.Length > 0);
        }

        [Test]
        public void HomePage_ShouldDisplayBothLogos()
        {
            var primaryLogo = _driver.FindElement(By.CssSelector("img.Header__LogoImage--primary"));
            var transparentLogo = _wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("img.Header__LogoImage--transparent")));

            Assert.Multiple(() =>
            {
                Assert.That(primaryLogo, Is.Not.Null, "Primary логотип не найден.");
                Assert.That(transparentLogo.Displayed, Is.True, "Прозорий логотип не видно.");
            });
        }

        // Heading Link Link--primary Text--subdued u-h8
        [Test]
        public void ClickSecondLink_Account()
        {
            var links = _wait.Until(driver =>
            {
                var found = driver.FindElements(By.CssSelector("a.Heading.Link.Link--primary.Text--subdued.u-h8"));
                return found.Count >= 2 ? found : null;
            });
        
            links[0].Click(); 
        
            _wait.Until(driver => driver.Url.Contains("/account"));
            Assert.That(_driver.Url, Does.Contain("/account"), "Не виконано перехід на /account");
        }

        [Test]
        public void ClickSearch()
        {

            var search = _wait.Until(driver =>
            {
                var found = _driver.FindElement(By.CssSelector("[data-action='toggle-search']"));
                return (found != null && found.Displayed && found.Enabled) ? found : null;
            });

            Assert.That(search.Displayed, Is.True, "Search element is not displayed.");
            Assert.That(search.Enabled, Is.True, "Search element is not enabled.");

            search.Click();
        }

        // By.CssSelector("[data-action='toggle-search']")

        [Test]
        public void SearchForExistingThing()
        {

            var searchToggle = _driver.FindElement(By.CssSelector("[data-action='toggle-search']"));

            searchToggle.Click();

            var searchInput = _driver.FindElement(By.CssSelector("input.Search__Input.Heading"));

            string searchWord = "smart lock";
            searchInput.Clear();
            searchInput.SendKeys(searchWord);
            searchInput.SendKeys(Keys.Enter);

            var result = _wait.Until(driver =>
            {
                var items = driver.FindElements(By.CssSelector(".ProductItem__PriceList"));
                return items.Count > 0;
            });

                var resultTitles = _driver.FindElements(By.CssSelector(".ProductItem__Title, .ProductItem"));

                bool found = resultTitles.Any(item =>
        item.Text.Contains("smart lock", StringComparison.OrdinalIgnoreCase));

    Assert.That(found, Is.True, "No results found containing 'smart lock'.");
           

            

        }


            [TearDown]

            public void Teardown()
            {
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Quit();
                _driver.Dispose();
            }
    }
}