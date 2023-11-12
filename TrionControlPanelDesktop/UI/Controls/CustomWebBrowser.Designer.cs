namespace TrionControlPanelDesktop.UI
{
    partial class CustomWebBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WebViewCore = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)WebViewCore).BeginInit();
            SuspendLayout();
            // 
            // WebViewCore
            // 
            WebViewCore.AllowExternalDrop = true;
            WebViewCore.CreationProperties = null;
            WebViewCore.DefaultBackgroundColor = Color.White;
            WebViewCore.Dock = DockStyle.Fill;
            WebViewCore.Location = new Point(0, 0);
            WebViewCore.Name = "WebViewCore";
            WebViewCore.Size = new Size(165, 156);
            WebViewCore.TabIndex = 0;
            WebViewCore.ZoomFactor = 1D;
            // 
            // CustomWebBrowser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(WebViewCore);
            Name = "CustomWebBrowser";
            Size = new Size(165, 156);
            Load += CustomWebBrowser_Load;
            ((System.ComponentModel.ISupportInitialize)WebViewCore).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewCore;
    }
}
