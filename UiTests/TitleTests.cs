using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
 
namespace theConnectedShop
{
    [TestFixture]
    public class TitleTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            
        }
        public void BeforeEach()
        {
            _driver.Navigate().GoToUrl("https://theconnectedshop.com/");
            _wait.Until(drv => drv.Title.Length > 0);
        }
 
        [Test]
        public void HomePage_Title_ShouldBeCorrect()
        {

            const string expected =
                "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";
                // "The Connected Shop – We sell exclusive & trending items to help people live a better life";


            Assert.Multiple(() =>
            {
                Assert.That(_driver.Url, Does.Contain("theconnectedshop.com"),
                            "Сайт не открылся.");
                Assert.That(_driver.Title, Is.EqualTo(expected),
                            "Title не совпал.");
            });
        }

        

        [TearDown]
        public void AfterEach()
        {
            _driver.Manage().Cookies.DeleteAllCookies();

        }
 
        [OneTimeTearDown]
        public void Teardown() {
            _driver.Quit();
            _driver.Dispose();
        } 
    }
}