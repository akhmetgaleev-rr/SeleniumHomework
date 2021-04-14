using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestsAtPrictice
{
    public class WikipediaTests
    {
        public ChromeDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // браузер раскрывается на весь экран
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //явные ожидания
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //неявные ожидания
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");

            var search = driver.FindElement(By.Name("search"));

            var seachButton = driver.FindElement(By.Name("go"));

            search.SendKeys("Selenium");

            seachButton.Click();

            Assert.IsTrue(driver.Title.Contains("Selenium — Википедия"), "Неверный заголовок страницы при переходе из поиска");
        }

        private By emailInputLocator = By.ClassName("email");
        private By buttonLocator = By.Id("write-to-me");
        private By emailResultLocator = By.Name("result-email");
        private By anotherEmailLinkLocator = By.LinkText("указать другой e-mail");
        private By anotherEmailLinkIdLocator = By.Id("anotherEmail");

        [Test]
        public void ComputerSite_FillFormWIthEmail_Success()

        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-lesson/");
            var expectedEmail = "test@mail.ru";

            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            Assert.AreEqual(expectedEmail, driver.FindElement(emailResultLocator).Text, "Сделали заявку не на тот e-mail");

            Thread.Sleep(2000);
        }

        [Test]
        public void ComputerSite_ClickAnotherEmail_EmailInputIsEmpty()

        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-lesson/");

            driver.FindElement(emailInputLocator).SendKeys("test@mail.ru");
            driver.FindElement(buttonLocator).Click();

            driver.FindElement(anotherEmailLinkLocator).Click();

            Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text, "После клика по ссылке поле не очистилось");
            Assert.IsFalse(driver.FindElement(anotherEmailLinkIdLocator).Displayed, "Не исчезла ссылка для ввода другого e-mail");

            Thread.Sleep(2000);
        }

        [TearDown]
        public void TeadDown()
        {
            driver.Quit();
        }

    }
}
