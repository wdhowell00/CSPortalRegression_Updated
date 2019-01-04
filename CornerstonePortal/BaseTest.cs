using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CornerstonePortal
{

    public static class ExtensionMethods
    {
        public static IWebElement FindElementOnPage(this IWebDriver webDriver, By by)
        {
            RemoteWebElement element = (RemoteWebElement)webDriver.FindElement(by);
            var hack = element.LocationOnScreenOnceScrolledIntoView;
            return element;
        }
    }

    public static class BaseTest
    {
        public static RemoteWebDriver Driver { get; set; }
        public static StringBuilder VerificationErrors { get; set; }
        public static string BaseURL { get; set; }
        public static bool AcceptNextAlert { get; set; }

        
        /// <summary>
        /// Selects the specified text
        /// </summary>
        /// <param name="id">Id of the list source</param>
        /// <param name="text">Text of the desired selected item</param>
        /// <param name="listIsAlphabetized"></param>
        /// <returns>Returns true if the desired text is found and selected</returns>
        public static bool SelectText(string id, string text, bool listIsAlphabetized = true)
        {

            bool retVal = false;

            IWebElement element = Driver.FindElement(By.Id(id));

            var AllDropDownList = element.FindElements(By.XPath("//option"));
            int DpListCount = AllDropDownList.Count;
            for (int i = 0; i < DpListCount; i++)
            {
                if (AllDropDownList[i].Text == text)
                {
                    AllDropDownList[i].Click();
                    retVal = true;
                    break;
                }
                else if (listIsAlphabetized && string.Compare(text, AllDropDownList[i].Text) == -1)
                {
                    break;
                }
            }

            return retVal;

        }

        //Write test reult to text file
        public static void WriteTestResult(string result)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\CornerstoneSupport\Test\PortalRegressionTest\CornerstonePortal\WIP\TestResults.txt", true))
            {
                file.WriteLine(result);
            }
        }

        public static void WriteReportResult(string result)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\CornerstoneSupport\Test\PortalRegressionTest\CornerstonePortal\WIP\ReportPage.txt", true))
            {
                file.WriteLine(result);
            }
        }



    }
}
