// using Microsoft.VisualBasic;
// using NUnit.Framework;
// using NUnit.Framework.Constraints;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Internal;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using System;
// using System.Linq;
// using System.Threading;

// namespace theConnectedShop.UiTests
// {
//     [TestFixture]
//     public class Searchgo
//     {
//         private IWebDriver _driver;
//         private WebDriverWait _wait;

//         [SetUp]
//         public void Setup()
//         {
//             var options = new ChromeOptions();
//             options.AddArgument("--start-maximized");
//             _driver = new ChromeDriver(options);
//             _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

//             _driver.Navigate().GoToUrl("https://12go.asia/en/");
//             _wait.Until(driver => driver.Url.StartsWith("https://12go.asia/en"));
//         }


//         [Test]
//         public void SearchForExistingThing()
//         {

//             var cityFrom = _driver.FindElement(By.CssSelector("[data-qa='field-select-btn']"));
//             var cityTo = _driver.FindElement(By.CssSelector("[data-qa='field-select-btn']"));

//             var searchToggle = _driver.FindElement(By.CssSelector("[data-action='toggle-search']"));

//             searchToggle.Click();

//             var searchInput = _driver.FindElement(By.CssSelector("input.Search__Input.Heading"));

//             string searchWord = "smart lock";
//             searchInput.Clear();
//             searchInput.SendKeys(searchWord);
//             searchInput.SendKeys(Keys.Enter);

//             var result = _wait.Until(driver =>
//             {
//                 var items = driver.FindElements(By.CssSelector(".ProductItem__Title.Heading a"));
//                 return items.Count > 0;
//             });

//             var resultTitles = _driver.FindElements(By.CssSelector(".ProductItem__Title.Heading a"));
//             var test = resultTitles.Select(s => s.Text).ToList();
//             var test2 = resultTitles.Select(s => s.GetAttribute("innerText")).ToList();

//             bool found = resultTitles.Any(item =>
//             item.GetAttribute("innerText").Contains("smart", StringComparison.OrdinalIgnoreCase));

//             Assert.That(resultTitles.Any(), Is.True, "No results found containing 'smart'.");
//             Assert.That(found, Is.True, "No results found containing 'smart'.");
//         }


//         [TearDown]

//             public void Teardown()
//             {
//                 _driver.Manage().Cookies.DeleteAllCookies();
//                 _driver.Quit();
//                 _driver.Dispose();
//             }
//     }
// }