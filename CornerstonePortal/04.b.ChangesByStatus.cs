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
    public class ChangesByStatus
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
        public void ChangesByStatusTest()
        {
            Login();
            Navigation();
            StatusFilters();
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

        void StatusFilters()
        {
            Thread.Sleep(1000);
            BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdNav']/div/a[3]/i")).Click();

            // Validate "Under Review at Cornerstone status"
            BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdFilters']/div[1]/div/i")).Click();
            BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdFilters']/div[1]/div/div[2]/div[3]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='()'])[1]/following::td[1]")).Click();
            Assert.AreEqual("Certificate of Authority", BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='()'])[1]/following::td[1]")).Text);
            Thread.Sleep(1000);
            Assert.AreEqual("ui fitted large teal history icon", BaseTest.Driver.FindElement(By.XPath("//*[@id='listView']/div/div[2]/table/tbody/tr[2]/td[3]/div/i")).GetAttribute("class"));
            Console.Write("Under Review at Cornerstone Status Successful");
            Thread.Sleep(500);

            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='List view'])[1]/following::button[1]")).Click();
            Thread.Sleep(1000);

            // Validate "In Process With Client status"
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Status'])[1]/following::i[1]")).Click();
            BaseTest.Driver.FindElement(By.XPath("//*[@id='dashbdFilters']/div[1]/div/div[2]/div[2]")).Click();
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='()'])[1]/following::td[1]")).Click();
            Assert.AreEqual("Debt Collection License Or Registration", BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='()'])[1]/following::td[1]")).Text);
            Thread.Sleep(1000);
            Assert.AreEqual("ui fitted large yellow wait icon", BaseTest.Driver.FindElement(By.XPath("//*[@id='listView']/div/div[2]/table/tbody/tr[2]/td[3]/div/i")).GetAttribute("class"));
            Console.Write("In Process with Client Status Successful");

            BaseTest.WriteTestResult("Changes by Status completed");
        }
    }
}
