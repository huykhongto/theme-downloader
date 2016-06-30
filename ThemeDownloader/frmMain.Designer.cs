namespace ThemeDownloader
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDevTool = new System.Windows.Forms.Button();
            this.pnlToolBar = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSaveFoldler = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.pnlToolBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDevTool
            // 
            this.btnDevTool.Location = new System.Drawing.Point(645, 9);
            this.btnDevTool.Name = "btnDevTool";
            this.btnDevTool.Size = new System.Drawing.Size(75, 23);
            this.btnDevTool.TabIndex = 0;
            this.btnDevTool.Text = "Dev Tool";
            this.btnDevTool.UseVisualStyleBackColor = true;
            this.btnDevTool.Click += new System.EventHandler(this.btnDevTool_Click);
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.Controls.Add(this.txtSaveFoldler);
            this.pnlToolBar.Controls.Add(this.btnBrowse);
            this.pnlToolBar.Controls.Add(this.btnView);
            this.pnlToolBar.Controls.Add(this.txtUrl);
            this.pnlToolBar.Controls.Add(this.btnDevTool);
            this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolBar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolBar.Name = "pnlToolBar";
            this.pnlToolBar.Size = new System.Drawing.Size(991, 38);
            this.pnlToolBar.TabIndex = 1;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(564, 9);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "Download";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(286, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://envato.stammtec.de/themeforest/melon/";
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 38);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(587, 430);
            this.pnlMain.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(484, 9);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(74, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSaveFoldler
            // 
            this.txtSaveFoldler.Location = new System.Drawing.Point(314, 11);
            this.txtSaveFoldler.Name = "txtSaveFoldler";
            this.txtSaveFoldler.ReadOnly = true;
            this.txtSaveFoldler.Size = new System.Drawing.Size(164, 20);
            this.txtSaveFoldler.TabIndex = 5;
            this.txtSaveFoldler.Text = "D:/Temp/";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtLogs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(587, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 430);
            this.panel1.TabIndex = 0;
            // 
            // txtLogs
            // 
            this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogs.Location = new System.Drawing.Point(0, 0);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(404, 430);
            this.txtLogs.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 468);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolBar);
            this.Name = "frmMain";
            this.Text = "Them downloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlToolBar.ResumeLayout(false);
            this.pnlToolBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDevTool;
        private System.Windows.Forms.Panel pnlToolBar;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtSaveFoldler;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLogs;
    }
}

