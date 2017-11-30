using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

namespace ProduceManager
{
    public class ReportPrint : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private string _rptPath;
        private DataSet _dsDataSource;
        private string _dsTableKey;

        public ReportPrint(string rptPath, DataSet dsDataSource, string dsTableKey)
        {
            _rptPath = rptPath;
            _dsDataSource = dsDataSource;
            _dsTableKey = dsTableKey;
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              //"  <PageWidth>8.5in</PageWidth>" +
              //"  <PageHeight>11in</PageHeight>" +
              //"  <MarginTop>0.25in</MarginTop>" +
              //"  <MarginLeft>0.25in</MarginLeft>" +
              //"  <MarginRight>0.25in</MarginRight>" +
              //"  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();

            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, 0, 0);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("没有指定有效的打印机.");
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }

        public void PrintRpt()
        {
            LocalReport report = new LocalReport();
            try
            {
                string strFilePath = _rptPath;
                report.ReportPath = strFilePath;

                IList<string> lisDs = report.GetDataSourceNames();
                foreach (string strds in lisDs)
                {
                    string strTableName = _dsTableKey == string.Empty ? strds : _dsTableKey + "-" + strds;
                    if (!_dsDataSource.Tables.Contains(strTableName))
                        continue;

                    if (report.DataSources[strds] == null || report.DataSources[strds].Value == null)
                    {
                        report.DataSources.Add(new ReportDataSource(strds, _dsDataSource.Tables[strTableName]));
                    }
                    else
                    {
                        report.DataSources[strds].Value = null;
                        report.DataSources[strds].Value = _dsDataSource.Tables[strTableName];
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("打印出错：" + err.InnerException.Message);
            }

            Export(report);

            m_currentPageIndex = 0;
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
