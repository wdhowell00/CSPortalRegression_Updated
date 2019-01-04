using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CornerstonePortal
{
    [TestClass]
    public class AllInventorySearch
    {

        [TestInitialize]
        public void Initialize()
        {
            //Set ChomeOptions for WebDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-infobars");
            BaseTest.Driver = new ChromeDriver(@"C:\CornerstoneSupport\Test", options);
            BaseTest.Driver.Manage().Window.Maximize();
            BaseTest.BaseURL = "https://qa.cornerstonesupport.com/";
            BaseTest.VerificationErrors = new StringBuilder();
            BaseTest.AcceptNextAlert = true;
        }

        [TestMethod]
        public void AllInventorySearchTest()
        {

            Login();
            Navigation();
            SearchValidation();
            Logout();
           
        }

        void Login()
        {
            BaseTest.Driver.Navigate().GoToUrl(BaseTest.BaseURL + "/Auth/Login?ReturnUrl=%2F");
            BaseTest.Driver.FindElement(By.Id("btnShowLogin")).Click();
            BaseTest.Driver.FindElement(By.Id("txtLoginUserName")).Clear();
            BaseTest.Driver.FindElement(By.Id("txtLoginUserName")).SendKeys("CornerstoneAdmin");
            BaseTest.Driver.FindElement(By.Id("txtLoginPassword")).Clear();
            BaseTest.Driver.FindElement(By.Id("txtLoginPassword")).SendKeys("Test2017");
            BaseTest.Driver.FindElement(By.Id("btnSignIn")).Click();
            Assert.AreEqual("https://qa.cornerstonesupport.com/", BaseTest.Driver.Url);
        }

        void Logout()
        {
            BaseTest.Driver.Navigate().GoToUrl(BaseTest.BaseURL + "/Home/Index");
            BaseTest.Driver.FindElement(By.XPath("//*[@id='topMenu']/li[10]/a")).Click();
            BaseTest.Driver.FindElement(By.LinkText("Sign Out")).Click();
            BaseTest.Driver.Close();
        }

        void Navigation()
        {

            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Import'])[1]/following::span[1]")).Click();
            BaseTest.Driver.FindElement(By.LinkText("Spoof Selection")).Click();
            BaseTest.Driver.FindElement(By.Id("Company")).Click();
            new SelectElement(BaseTest.Driver.FindElement(By.Id("Company"))).SelectByText("Portal Demo");
            BaseTest.Driver.FindElement(By.Id("Company")).Click();
            BaseTest.Driver.FindElement(By.Id("CompanyUser")).Click();
            new SelectElement(BaseTest.Driver.FindElement(By.Id("CompanyUser"))).SelectByText("Test");
            BaseTest.Driver.FindElement(By.Id("CompanyUser")).Click();
            BaseTest.Driver.FindElement(By.Id("btnViewCompanyPortal")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if ("Licensing items managed by Cornerstone Support" == BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdDownloadItems']/div[1]")).Text)
                    {
                        Console.WriteLine(" Navigation Succeeded");
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        void SearchValidation()
        {
            Thread.Sleep(2000);
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Search all'])[1]/following::input[1]")).Clear();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Search all'])[1]/following::input[1]")).SendKeys("UT-Certificate");
            Thread.Sleep(1000);
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Utah'])[2]/following::span[1]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Maintenance Filings'])[1]/following::h5[1]")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("Utah Annual Report", BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Maintenance Filings'])[1]/following::h5[1]")).Text);
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Loading'])[2]/following::button[1]")).Click();

            BaseTest.WriteTestResult("Search Validation Successful");
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            BaseTest.Driver.ExecuteScript(js);
        }

        public IWebElement ScrollToView(By selector)
        {
            var element = BaseTest.Driver.FindElement(selector);
            ScrollToView(element);
            return element;
        }

        public void ScrollToView(IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }

        }

    }
}
