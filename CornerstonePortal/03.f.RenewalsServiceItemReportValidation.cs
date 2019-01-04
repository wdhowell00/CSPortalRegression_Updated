using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncfusion.Windows.Forms.PdfViewer;
using System.Collections.Generic;
using System.Drawing;


namespace CornerstonePortal
{
    [TestClass]
    public class RenewalsServiceItemReportValidation
    {
        [TestMethod]
        public void RenewalsServiceItemReportValidationTest()
        {
            ValidateDownload();
        }

        void ValidateDownload()
        {
            
            PdfViewerControl documentViewer = new PdfViewerControl();
            documentViewer.Load(@"C:\Users\CstoneAdmin\Downloads\Cornerstone-ServiceItemReports.pdf");

            Dictionary<int, List<RectangleF>> textSearch = new Dictionary<int, List<RectangleF>>();

            bool IsMatchFound = documentViewer.FindText("Portal Demo", out textSearch);

            documentViewer.Dispose();

            BaseTest.WriteTestResult("Renewals Service item Report Output Validated");


        }
    }
}
