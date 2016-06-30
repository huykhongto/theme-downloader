using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

using CefSharp;
using CefSharp.WinForms;
using System.Text.RegularExpressions;
using System.IO;

namespace ThemeDownloader
{
    public partial class frmMain : Form
    {
        ChromiumWebBrowser m_chromeBrowser = null;
        List<string> downloadedPages;
        string currentDownloadUrl = "";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            downloadedPages = new List<string>();
            //InitComponents();
        }

        private void btnDevTool_Click(object sender, EventArgs e)
        {
            m_chromeBrowser.ShowDevTools();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            var res = dlg.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK || !string.IsNullOrEmpty(dlg.SelectedPath))
            {
                txtSaveFoldler.Text = dlg.SelectedPath;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            
            InitComponents(txtUrl.Text);
        }

        private List<string> getResourceUrls(string dataStr, string startStr, string endStr)
        {
            List<string> resourceUrls = new List<string>();
            int start = 0;
            int end = 0;

            end = dataStr.IndexOf(endStr);
            while (end != -1)
            {
                end += endStr.Length;
                start = dataStr.LastIndexOf(startStr, end);
                if (start != -1) {
                    start += startStr.Length;

                    resourceUrls.Add(dataStr.Substring(start, end - (start + 1)));
                    
                }
                start = end;
                end = dataStr.IndexOf(endStr, end);
            }

            return resourceUrls;
        }

        private void downloadResources(List<string> resUrls, string desFolder)
        {
            WebClient Client = new WebClient();
            foreach (string url in resUrls)
            {
                string tempUrl = url;
                string source = "";
                try
                {
                    if (url.Substring(0, 1) == "/")
                        tempUrl = url.Substring(1, url.Length - 1);
                    string des = desFolder + url;
                    string serverPath = currentDownloadUrl.Substring(0, currentDownloadUrl.LastIndexOf("/") + 1);
                    source = serverPath + tempUrl;
                    FileInfo file = new FileInfo(des);
                    DirectoryInfo dir = file.Directory;
                    if (!file.Exists)
                    {
                        if (!dir.Exists)
                            System.IO.Directory.CreateDirectory(dir.FullName);

                        AddLog(string.Format("Downloading: {0}",source));
                        Client.DownloadFile(source, des);

                    }
                }
                catch (Exception ex) {
                    AddLog(string.Format("Error: {0} - {1}", ex.Message, source));
                    System.Console.WriteLine(ex.Message); 
                }
            }
        }

        public void ChomeBrowser_LoadStart(object sender, EventArgs arg)
        {

        }

        public void ChomeBrowser_LoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                //m_chromeBrowser.ViewSource();
                m_chromeBrowser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    string html = taskHtml.Result;
                    int start = html.IndexOf("<head>");
                    int end = html.IndexOf("</head>");
                    string header = html.Substring(start, end - start);

                    string body = html.Substring(end, html.Length - end);
                    List<List<string>> resources = new List<List<string>>();

                    //get all css file
                    resources.Add(getResourceUrls(html, "href=\"", ".css\""));

                    resources.Add(getResourceUrls(html, "src=\"", ".js\""));

                    //resources.Add(getResourceUrls(body, "src=\"", ".png\""));
                    //resources.Add( getResourceUrls(body, "src=\"", ".jpg\""));
                    //resources.Add( getResourceUrls(body, "src=\"", ".gif\""));

                    resources.Add(getResourceUrls(body, "=\"", ".png\""));
                    resources.Add( getResourceUrls(body, "=\"", ".jpg\""));
                    resources.Add(getResourceUrls(body, "=\"", ".gif\""));

                    List<string> html1 = getResourceUrls(body, "href=\"", ".html\"");
                    List<string> html2 = getResourceUrls(body, "href=\"", ".htm\"");
                    resources.Add(html1);
                    resources.Add(html2);

                    string des = txtSaveFoldler.Text;
                    if (string.IsNullOrEmpty(des))
                        des = @"D:\Temp\";
                    if (!des.EndsWith("\\"))
                        des += "\\";

                    foreach (var source in resources)
                    {
                        downloadResources(source, des);
                    }

                    //try to download more files from another pages

                    //string serverPath = currentDownloadUrl.Substring(0, currentDownloadUrl.LastIndexOf("/") + 1);
                    //string url = "";
                    //foreach (var htmlUrl in html1)
                    //{
                    //    url = serverPath + htmlUrl;
                    //    do
                    //    {
                            
                    //    }
                    //    while (m_chromeBrowser.IsLoading);
                    //    InitComponents(url);
                        
                    //}


                });
            }
        }

        private void InitComponents(string url)
        {
            if (!Cef.IsInitialized)
            {
                Cef.Initialize();

            }

            if(downloadedPages.Count() == 0 || !downloadedPages.Contains(url))
            {
                if (m_chromeBrowser == null)
                {
                    m_chromeBrowser = new ChromiumWebBrowser(url);
                    m_chromeBrowser.FrameLoadStart += new EventHandler<FrameLoadStartEventArgs>(ChomeBrowser_LoadStart);
                    m_chromeBrowser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(ChomeBrowser_LoadEnd);
                    pnlMain.Controls.Clear();
                    pnlMain.Controls.Add(m_chromeBrowser);
                }
                else
                {
                    m_chromeBrowser.Load(url);
                }
                currentDownloadUrl = url;
                txtUrl.Invoke((MethodInvoker)(() => txtUrl.Text = url));
                downloadedPages.Add(url);
            }

        }

        private void AddLog(string log)
        {
            //txtLogs.Text = log + "\r\n" +txtLogs.Text;
            txtLogs.Invoke((MethodInvoker)(() => txtLogs.Text = log + "\r\n" +txtLogs.Text));
        }

    }
}
