using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace CornerstonePortal
{
    [TestClass]
    public class AllInventoryLicenses
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
        public void AllInventoryLicenseTest()
        {
            Login();
            Navigation();
            CertificateFilter();
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
                        Console.WriteLine("Test Succeeded");
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }



        }

        void CertificateFilter()
        {
            Thread.Sleep(1000);
            //Select Alaska as Jurisdiction
            ScrollToView(By.CssSelector("#dashbdFilters > div:nth-child(1) > div"));
            BaseTest.Driver.FindElement(By.CssSelector("#dashbdFilters > div:nth-child(1) > div > i")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Jurisdiction'])[1]/following::div[2]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Filter by Jurisdiction'])[1]/following::div[2]")).Click();
            BaseTest.Driver.FindElement(By.CssSelector("#dashbdFilters > div:nth-child(1) > div > i")).Click();
            
            Thread.Sleep(1000);

            //Select Certificate of Authority as Certificate Type
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='License Type'])[1]/following::i[1]")).Click();
            Thread.Sleep(1000);
            BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdFilters']/div[2]/div/div[2]/div[1]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='AK-Certificate of Authority'])[1]/following::div[1]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Inventory Certificate'])[1]/following::h3[1]")).Click();
            Thread.Sleep(1000);
            
            //Assert the correct Certificate is loaded
            Assert.AreEqual("Alaska Biennial Report", BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Primary location'])[1]/following::h5[1]")).Text);

            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Loading'])[2]/following::button[1]")).Click();
            BaseTest.WriteTestResult("Certificate Filter Successful");

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
