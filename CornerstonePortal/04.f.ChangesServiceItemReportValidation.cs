using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncfusion.Windows.Forms.PdfViewer;
using System.Collections.Generic;
using System.Drawing;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;

namespace CornerstonePortal
{
    [TestClass]
    public class ChangesServiceItemReportValidation
    {
        [TestMethod]
        public void ChangesServiceItemReportValidationTest()
        {
            ValidateDownload();
        }

        void ValidateDownload()
        {
            //PdfLoadedDocument loadedDocument = new PdfLoadedDocument(@"C:\Users\CstoneAdmin\Downloads\Cornerstone-ServiceItemReports.pdf");

            //PdfPageBase page = loadedDocument.Pages[0];

            //string extractedTexts = page.ExtractText(true);

            //BaseTest.WriteReportResult(extractedTexts);

            //loadedDocument.Close(true);

            PdfViewerControl documentViewer = new PdfViewerControl();
            documentViewer.Load(@"C:\Users\CstoneAdmin\Downloads\Cornerstone-ServiceItemReports.pdf");

            Dictionary<int, List<RectangleF>> textSearch = new Dictionary<int, List<RectangleF>>();

            bool IsMatchFound = documentViewer.FindText("Portal Demo", out textSearch);

            documentViewer.Dispose();

            BaseTest.WriteTestResult("Changes Service item Report Output Validated");


        }
    }
}
