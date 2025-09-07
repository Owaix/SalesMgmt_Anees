using Lib.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;


namespace SalesMngmt.Utility
{
    public class Silent
    {
        private IList<Stream> m_streams;
        private int m_currentPageIndex;

        public Silent() { }
        public void Run<T>(ReportViewer reportViewer1, List<T> RecptLists) where T : ReportsModel
        {
            var RecptList = RecptLists.ToList();
            float Heigth = 1.5000f;
            float RHeigth = 0.21612f;

            float HightCut = 0.0f;
            for (int i = 0; i <= RecptList.Count - 1; i++)
            {
                RecptList[i].Rows = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0));
                RHeigth = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0)) == 1 ? 0.21612f : (0.17f * RecptList[i].Rows);
                RecptList[i].RowHeight = RHeigth;
                if (RecptList[i].Rows > 1)
                {
                    HightCut += (RecptList[i].Rows == 2) ? 0.018f : 0.08f;
                }
            }

            float h = RecptList.Select(x => x.RowHeight).Sum(); // All Rows Sum of Table
            Heigth += Convert.ToSingle(h);
            Heigth += 1;
            Heigth += 0.30318f; //
            Heigth += 0.90299f; // 

            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.Kot.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds", RecptList));

            string deviceInfo = string.Format(@"<DeviceInfo> 
                                                <OutputFormat>EMF</OutputFormat>
                                                <PageWidth>3.0in</PageWidth>
                                                <PageHeight>{0}in</PageHeight>
                                                <MarginTop>0.0in</MarginTop>
                                                <MarginLeft>0.0in</MarginLeft>
                                                <MarginRight>0.0in</MarginRight>
                                                <MarginBottom>0.0in</MarginBottom>
                                                </DeviceInfo>", (Heigth - HightCut));
            Warning[] warnings;
            m_streams = new List<Stream>();

            reportViewer1.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            reportViewer1.Update();
            reportViewer1.RefreshReport();

            //this.reportViewer1.Visible = true;

            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                PaperSize pkCustomSize1 = null;

                int pgHight = (410 + (14 * RecptList.Count));
                int hh = Convert.ToInt32((Heigth * 100.0f));
                pkCustomSize1 = new PaperSize("Custom", 320, hh);
                printDoc.DefaultPageSettings.PaperSize = pkCustomSize1;

                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        //public void Run<T>(ReportViewer reportViewer1, List<T> RecptLists, string val) where T : ReportsModel
        //{
        //    var RecptList = RecptLists.ToList();
        //    float Heigth = 1.5000f;
        //    float RHeigth = 0.21612f;

        //    float HightCut = 0.0f;
        //    for (int i = 0; i <= RecptList.Count - 1; i++)
        //    {
        //        RecptList[i].Rows = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0));
        //        RHeigth = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0)) == 1 ? 0.28612f : (0.17f * RecptList[i].Rows);
        //        RecptList[i].RowHeight = RHeigth;
        //        if (RecptList[i].Rows > 1)
        //        {
        //            HightCut += (RecptList[i].Rows == 2) ? 0.018f : 0.08f;
        //        }
        //    }

        //    float h = RecptList.Select(x => x.RowHeight).Sum(); // All Rows Sum of Table
        //    Heigth += Convert.ToSingle(h);
        //    Heigth += 1;
        //    Heigth += 0.30318f; //
        //    Heigth += 2.5f; // 

        //    reportViewer1.ProcessingMode = ProcessingMode.Local;
        //    reportViewer1.LocalReport.ReportEmbeddedResource = val;
        //    reportViewer1.LocalReport.EnableExternalImages = true;
        //    //ReportParameter reportParameter = new ReportParameter("Img", @"\Img\123.png");
        //    //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { reportParameter });            
        //    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds", RecptList));

        //    string deviceInfo = string.Format(@"<DeviceInfo> 
        //                                        <OutputFormat>EMF</OutputFormat>
        //                                        <PageWidth>3.0in</PageWidth>
        //                                        <PageHeight>{0}in</PageHeight>
        //                                        <MarginTop>0.0in</MarginTop>
        //                                        <MarginLeft>0.0in</MarginLeft>
        //                                        <MarginRight>0.0in</MarginRight>
        //                                        <MarginBottom>0.0in</MarginBottom>
        //                                        </DeviceInfo>", (Heigth - HightCut));
        //    Warning[] warnings;
        //    m_streams = new List<Stream>();

        //    reportViewer1.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);

        //    foreach (Stream stream in m_streams)
        //        stream.Position = 0;

        //    reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
        //    reportViewer1.ZoomMode = ZoomMode.Percent;
        //    reportViewer1.ZoomPercent = 100;

        //    reportViewer1.Update();
        //    reportViewer1.RefreshReport();

        //    //this.reportViewer1.Visible = true;

        //    PrintDocument printDoc = new PrintDocument();
        //    if (!printDoc.PrinterSettings.IsValid)
        //    {
        //        throw new Exception("Error: cannot find the default printer.");
        //    }
        //    else
        //    {
        //        PaperSize pkCustomSize1 = null;

        //        int pgHight = (410 + (14 * RecptList.Count));
        //        int hh = Convert.ToInt32((Heigth * 100.0f));
        //        pkCustomSize1 = new PaperSize("Custom", 320, hh);
        //        printDoc.DefaultPageSettings.PaperSize = pkCustomSize1;

        //        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        //        m_currentPageIndex = 0;
        //        printDoc.Print();
        //    }
        //}
        public void Run<T>(ReportViewer reportViewer1, List<T> receiptList, string reportResource) where T : ReportsModel
        {
            var RecptList = receiptList.ToList();
            float Heigth = 1.5000f;
            float RHeigth = 0.21612f;

            float HightCut = 0.0f;
            for (int i = 0; i <= RecptList.Count - 1; i++)
            {
                RecptList[i].Rows = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0));
                RHeigth = (Convert.ToInt32((RecptList[i].item.Length / 21)) + Convert.ToInt32((RecptList[i].item.Length % 21) > 0 ? 1 : 0)) == 1 ? 0.21612f : (0.17f * RecptList[i].Rows);
                RecptList[i].RowHeight = RHeigth;
                if (RecptList[i].Rows > 1)
                {
                    HightCut += (RecptList[i].Rows == 2) ? 0.018f : 0.08f;
                }
            }

            float h = RecptList.Select(x => x.RowHeight).Sum(); // All Rows Sum of Table
            Heigth += Convert.ToSingle(h);
            Heigth += 1;
            Heigth += 0.30318f; //
            Heigth += 0.90299f; // 

            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportEmbeddedResource = reportResource;
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds", RecptList));

            string deviceInfo = string.Format(@"<DeviceInfo> 
                                                <OutputFormat>EMF</OutputFormat>
                                                <PageWidth>3.0in</PageWidth>
                                                <PageHeight>{0}in</PageHeight>
                                                <MarginTop>0.0in</MarginTop>
                                                <MarginLeft>0.0in</MarginLeft>
                                                <MarginRight>0.0in</MarginRight>
                                                <MarginBottom>0.0in</MarginBottom>
                                                </DeviceInfo>", (Heigth - HightCut));
            Warning[] warnings;
            m_streams = new List<Stream>();

            reportViewer1.LocalReport.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;

            reportViewer1.Update();
            reportViewer1.RefreshReport();

            //this.reportViewer1.Visible = true;

            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                PaperSize pkCustomSize1 = null;

                int pgHight = (410 + (14 * RecptList.Count));
                int hh = Convert.ToInt32((Heigth * 100.0f));
                pkCustomSize1 = new PaperSize("Custom", 320, hh);
                printDoc.DefaultPageSettings.PaperSize = pkCustomSize1;

                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
            #region comment
            //var formattedReceipts = receiptList.ToList();
            //float baseHeight = 1.5f;
            //float rowHeight = 0.21612f;
            //float contentHeight = 0.0f;

            //foreach (var receipt in formattedReceipts)
            //{
            //    receipt.Rows = (receipt.item.Length / 21) + ((receipt.item.Length % 21 > 0) ? 1 : 0);
            //    receipt.RowHeight = (receipt.Rows == 1) ? 0.28612f : (0.17f * receipt.Rows);
            //    contentHeight += receipt.RowHeight;

            //    Console.WriteLine($"Item: {receipt.item}, Rows: {receipt.Rows}, RowHeight: {receipt.RowHeight}");
            //}

            //float totalHeight = baseHeight + contentHeight + 2.5f;
            //Console.WriteLine($"Calculated Total Height: {totalHeight}");

            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.ReportEmbeddedResource = reportResource;
            //reportViewer.LocalReport.DataSources.Clear();
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Ds", formattedReceipts));

            //string deviceInfo = string.Format(@"<DeviceInfo>
            //                  <OutputFormat>EMF</OutputFormat>
            //                  <PageWidth>3.0in</PageWidth>
            //                  <PageHeight>{0}in</PageHeight>
            //                  <MarginTop>0.0in</MarginTop>
            //                  <MarginLeft>0.0in</MarginLeft>
            //                  <MarginRight>0.0in</MarginRight>
            //                  <MarginBottom>0.0in</MarginBottom>
            //                  </DeviceInfo>", totalHeight);

            //Warning[] warnings;
            //var streams = new List<Stream>();

            //reportViewer.LocalReport.Render("Image", deviceInfo, (name, fileNameExtension, encoding, mimeType, willSeek) =>
            //{
            //    var stream = new MemoryStream();
            //    streams.Add(stream);
            //    return stream;
            //}, out warnings);

            //foreach (var stream in streams)
            //    stream.Position = 0;

            //PrintDocument printDoc = new PrintDocument();
            //if (!printDoc.PrinterSettings.IsValid)
            //    throw new Exception("Printer not found or invalid.");

            //int printableHeight = Convert.ToInt32(totalHeight * 100);
            //PaperSize customPaperSize = new PaperSize("Custom", 300, printableHeight);
            //printDoc.DefaultPageSettings.PaperSize = customPaperSize;

            //int currentPageIndex = 0;
            //printDoc.PrintPage += (sender, e) =>
            //{
            //    if (currentPageIndex < streams.Count)
            //    {
            //        using (Metafile pageImage = new Metafile(streams[currentPageIndex]))
            //        {
            //            e.Graphics.DrawImage(pageImage, new Point(0, 0));
            //        }
            //        currentPageIndex++;
            //        e.HasMorePages = currentPageIndex < streams.Count;
            //    }
            //    else
            //    {
            //        e.HasMorePages = false;
            //    }
            //};

            //printDoc.Print();
            #endregion
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}