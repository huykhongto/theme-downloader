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

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //InitComponents();
        }

        private void btnDevTool_Click(object sender, EventArgs e)
        {
            m_chromeBrowser.ShowDevTools();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_chromeBrowser.FrameLoadStart += new EventHandler<FrameLoadStartEventArgs>(ChomeBrowser_LoadStart);
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

        private void btnView_Click(object sender, EventArgs e)
        {
            InitComponents();
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
                string source = "";
                try
                {
                    string des = desFolder + url;
                    string serverPath = txtUrl.Text.Substring(0, txtUrl.Text.LastIndexOf("/") + 1);
                    source = serverPath + url;
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

                    //get all css file
                    List<string> cssFiles = getResourceUrls(html, "href=\"", ".css\"");

                    List<string> jsFiles = getResourceUrls(html, "src=\"", ".js\"");

                    List<string> imgFiles = getResourceUrls(body, "src=\"", ".png\"");
                    List<string> imgFilesJpg = getResourceUrls(body, "src=\"", ".jpg\"");
                    List<string> imgFilesGif = getResourceUrls(body, "src=\"", ".gif\"");
                    List<string> htmlFiles = getResourceUrls(body, "href=\"", ".html\"");

                    string des = txtSaveFoldler.Text;
                    if (string.IsNullOrEmpty(des))
                        des = @"D:\Temp\";
                    if (!des.EndsWith("\\"))
                        des += "\\";
                    downloadResources(cssFiles, des);
                    downloadResources(jsFiles, des);
                    downloadResources(imgFiles, des);
                    downloadResources(imgFilesJpg, des);
                    downloadResources(imgFilesGif, des);

                    downloadResources(htmlFiles, des);
                    int a = 0;

                    //get all js file

                });
            }
        }

        private void InitComponents()
        {
            if (!Cef.IsInitialized)
            {
                Cef.Initialize();

            }
            if (m_chromeBrowser == null)
            {
                m_chromeBrowser = new ChromiumWebBrowser(txtUrl.Text);
                m_chromeBrowser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(ChomeBrowser_LoadEnd);
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(m_chromeBrowser);
            }
            else
            {
                m_chromeBrowser.Load(txtUrl.Text);
            }

        }

        private void AddLog(string log)
        {
            //txtLogs.Text = log + "\r\n" +txtLogs.Text;
            txtLogs.Invoke((MethodInvoker)(() => txtLogs.Text = log + "\r\n" +txtLogs.Text));
        }

    }
}
