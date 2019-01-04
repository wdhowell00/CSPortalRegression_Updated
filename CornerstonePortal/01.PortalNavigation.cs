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
    public class PortalNavigation
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
        public void PortalNavigationTest()
        {
            Login();
            Navigation();
            HeaderNavigation();
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

        void Navigation()
        {
            BaseTest.Driver.Navigate().GoToUrl("https://qa.cornerstonesupport.com/");
            BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Import'])[1]/following::span[1]")).Click();
            BaseTest.Driver.FindElement(By.LinkText("Spoof Selection")).Click();
            BaseTest.Driver.FindElement(By.Id("Company")).Click();
            new SelectElement(BaseTest.Driver.FindElement(By.Id("Company"))).SelectByText("Portal Demo");
            BaseTest.Driver.FindElement(By.Id("Company")).Click();
            BaseTest.Driver.FindElement(By.Id("CompanyUser")).Click();
            new SelectElement(BaseTest.Driver.FindElement(By.Id("CompanyUser"))).SelectByText("Test");
            BaseTest.Driver.FindElement(By.Id("CompanyUser")).Click();
            BaseTest.Driver.FindElement(By.Id("btnViewCompanyPortal")).Click();
            Thread.Sleep(1000);
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if ("Certificate Name" == BaseTest.Driver.FindElement(By.XPath("//*[@id='listView']/div/div[1]/table/thead/tr/th[1]")).Text) break; 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(1000);
            }

            

        }

        void HeaderNavigation()
        {
            BaseTest.Driver.FindElement(By.LinkText("Contact")).Click();
            Thread.Sleep(1000);
            // Validate Contact page clicked
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if ("SEND A MESSAGE REGARDING A SPECIFIC SUBJECT/SERVICE" == BaseTest.Driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cornerstone Support Contacts'])[1]/following::p[1]")).Text)
                    {
                        Console.WriteLine("Contact Link Successful");
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            BaseTest.Driver.FindElement(By.LinkText("Services")).Click();
            // Validate Services page clicked
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if ("Cornerstone Support Services" == BaseTest.Driver.FindElement(By.XPath("//*[@id='servicesMenu']/div[1]")).Text) 
                    {
                        Console.WriteLine("Service Link Successful");
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            BaseTest.WriteTestResult("HeaderNavigation Navigation Successful");
        }

        void Logout()
        {
            BaseTest.Driver.Navigate().GoToUrl(BaseTest.BaseURL + "/Home/Index");
            BaseTest.Driver.FindElement(By.XPath("//*[@id='topMenu']/li[10]/a")).Click();
            BaseTest.Driver.FindElement(By.LinkText("Sign Out")).Click();
            BaseTest.Driver.Close();
        }
    }
}
