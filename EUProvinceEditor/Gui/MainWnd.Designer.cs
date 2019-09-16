namespace EUProvinceEditor.Gui
{
   partial class MainWnd
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose ( bool disposing )
      {
         if ( disposing && ( components != null ) )
         {
            components.Dispose ();
         }
         base.Dispose ( disposing );
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent ()
      {
         this.components = new System.ComponentModel.Container();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.m_lblLoading = new System.Windows.Forms.ToolStripStatusLabel();
         this.m_pbLoading = new System.Windows.Forms.ToolStripProgressBar();
         this.m_pnlContentPane = new System.Windows.Forms.Panel();
         this.m_pbMap = new System.Windows.Forms.PictureBox();
         this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_btnLoadTradenodes = new System.Windows.Forms.ToolStripMenuItem();
         this.m_btnSaveTradenodes = new System.Windows.Forms.ToolStripMenuItem();
         this.m_btnSaveMap = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.m_btnClose = new System.Windows.Forms.ToolStripMenuItem();
         this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.m_btnExportTradenodes = new System.Windows.Forms.ToolStripMenuItem();
         this.m_btnClear = new System.Windows.Forms.Button();
         this.m_cbShowOverlay = new System.Windows.Forms.CheckBox();
         this.m_cbMarker = new System.Windows.Forms.ComboBox();
         this.label2 = new System.Windows.Forms.Label();
         this.m_btnSave = new System.Windows.Forms.Button();
         this.m_ttMarkr = new System.Windows.Forms.ToolTip(this.components);
         this.m_btnZoom = new System.Windows.Forms.Button();
         this.m_btnMarkProvinces = new System.Windows.Forms.Button();
         this.m_btnMarkLocation = new System.Windows.Forms.Button();
         this.m_btnMarkOutgoing = new System.Windows.Forms.Button();
         this.statusStrip1.SuspendLayout();
         this.m_pnlContentPane.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.m_pbMap)).BeginInit();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_lblLoading,
            this.m_pbLoading});
         this.statusStrip1.Location = new System.Drawing.Point(0, 668);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(1442, 22);
         this.statusStrip1.TabIndex = 1;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // m_lblLoading
         // 
         this.m_lblLoading.Name = "m_lblLoading";
         this.m_lblLoading.Size = new System.Drawing.Size(185, 17);
         this.m_lblLoading.Text = "Loading image data please wait ...";
         // 
         // m_pbLoading
         // 
         this.m_pbLoading.Name = "m_pbLoading";
         this.m_pbLoading.Size = new System.Drawing.Size(100, 16);
         this.m_pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
         // 
         // m_pnlContentPane
         // 
         this.m_pnlContentPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_pnlContentPane.AutoScroll = true;
         this.m_pnlContentPane.Controls.Add(this.m_pbMap);
         this.m_pnlContentPane.Location = new System.Drawing.Point(12, 56);
         this.m_pnlContentPane.Name = "m_pnlContentPane";
         this.m_pnlContentPane.Size = new System.Drawing.Size(1418, 609);
         this.m_pnlContentPane.TabIndex = 2;
         // 
         // m_pbMap
         // 
         this.m_pbMap.Location = new System.Drawing.Point(3, 3);
         this.m_pbMap.Name = "m_pbMap";
         this.m_pbMap.Size = new System.Drawing.Size(1412, 603);
         this.m_pbMap.TabIndex = 0;
         this.m_pbMap.TabStop = false;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.hilfeToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(1442, 24);
         this.menuStrip1.TabIndex = 3;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // dateiToolStripMenuItem
         // 
         this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btnLoadTradenodes,
            this.m_btnSaveTradenodes,
            this.m_btnSaveMap,
            this.toolStripSeparator1,
            this.m_btnClose});
         this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
         this.dateiToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.dateiToolStripMenuItem.Text = "&File";
         // 
         // m_btnLoadTradenodes
         // 
         this.m_btnLoadTradenodes.Name = "m_btnLoadTradenodes";
         this.m_btnLoadTradenodes.Size = new System.Drawing.Size(162, 22);
         this.m_btnLoadTradenodes.Text = "&Load tradenodes";
         this.m_btnLoadTradenodes.Click += new System.EventHandler(this.M_btnLoadTradenodes_Click);
         // 
         // m_btnSaveTradenodes
         // 
         this.m_btnSaveTradenodes.Name = "m_btnSaveTradenodes";
         this.m_btnSaveTradenodes.Size = new System.Drawing.Size(162, 22);
         this.m_btnSaveTradenodes.Text = "&Save tradenodes";
         this.m_btnSaveTradenodes.Click += new System.EventHandler(this.M_btnSaveTradenodes_Click);
         // 
         // m_btnSaveMap
         // 
         this.m_btnSaveMap.Name = "m_btnSaveMap";
         this.m_btnSaveMap.Size = new System.Drawing.Size(162, 22);
         this.m_btnSaveMap.Text = "Save &Map";
         this.m_btnSaveMap.Click += new System.EventHandler(this.M_btnSaveMap_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
         // 
         // m_btnClose
         // 
         this.m_btnClose.Name = "m_btnClose";
         this.m_btnClose.Size = new System.Drawing.Size(162, 22);
         this.m_btnClose.Text = "&Exit";
         this.m_btnClose.Click += new System.EventHandler(this.M_btnClose_Click);
         // 
         // hilfeToolStripMenuItem
         // 
         this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.überToolStripMenuItem});
         this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
         this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
         this.hilfeToolStripMenuItem.Text = "&Help";
         // 
         // überToolStripMenuItem
         // 
         this.überToolStripMenuItem.Name = "überToolStripMenuItem";
         this.überToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
         this.überToolStripMenuItem.Text = "&About";
         // 
         // exportToolStripMenuItem
         // 
         this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_btnExportTradenodes});
         this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
         this.exportToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
         this.exportToolStripMenuItem.Text = "&Export";
         // 
         // m_btnExportTradenodes
         // 
         this.m_btnExportTradenodes.Name = "m_btnExportTradenodes";
         this.m_btnExportTradenodes.Size = new System.Drawing.Size(180, 22);
         this.m_btnExportTradenodes.Text = "00_tradenodes.txt";
         this.m_btnExportTradenodes.Click += new System.EventHandler(this.M_btnExportTradenodes_Click);
         // 
         // m_btnClear
         // 
         this.m_btnClear.Location = new System.Drawing.Point(287, 27);
         this.m_btnClear.Name = "m_btnClear";
         this.m_btnClear.Size = new System.Drawing.Size(120, 23);
         this.m_btnClear.TabIndex = 4;
         this.m_btnClear.Text = "Clear tradenodes";
         this.m_btnClear.UseVisualStyleBackColor = true;
         this.m_btnClear.Click += new System.EventHandler(this.M_btnClear_Click);
         // 
         // m_cbShowOverlay
         // 
         this.m_cbShowOverlay.AutoSize = true;
         this.m_cbShowOverlay.Checked = true;
         this.m_cbShowOverlay.CheckState = System.Windows.Forms.CheckState.Checked;
         this.m_cbShowOverlay.Location = new System.Drawing.Point(413, 30);
         this.m_cbShowOverlay.Name = "m_cbShowOverlay";
         this.m_cbShowOverlay.Size = new System.Drawing.Size(109, 17);
         this.m_cbShowOverlay.TabIndex = 6;
         this.m_cbShowOverlay.Text = "Show tradenodes";
         this.m_cbShowOverlay.UseVisualStyleBackColor = true;
         this.m_cbShowOverlay.CheckedChanged += new System.EventHandler(this.M_cbShowOverlay_CheckedChanged);
         // 
         // m_cbMarker
         // 
         this.m_cbMarker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.m_cbMarker.FormattingEnabled = true;
         this.m_cbMarker.Location = new System.Drawing.Point(64, 28);
         this.m_cbMarker.Name = "m_cbMarker";
         this.m_cbMarker.Size = new System.Drawing.Size(217, 21);
         this.m_cbMarker.TabIndex = 9;
         this.m_cbMarker.SelectedIndexChanged += new System.EventHandler(this.M_cbMarker_SelectedIndexChanged);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(12, 32);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(46, 13);
         this.label2.TabIndex = 10;
         this.label2.Text = "Marker :";
         // 
         // m_btnSave
         // 
         this.m_btnSave.Location = new System.Drawing.Point(1184, 27);
         this.m_btnSave.Name = "m_btnSave";
         this.m_btnSave.Size = new System.Drawing.Size(120, 23);
         this.m_btnSave.TabIndex = 11;
         this.m_btnSave.Text = "Show tradenodes";
         this.m_btnSave.UseVisualStyleBackColor = true;
         this.m_btnSave.Click += new System.EventHandler(this.M_btnSave_Click);
         // 
         // m_ttMarkr
         // 
         this.m_ttMarkr.AutomaticDelay = 150;
         // 
         // m_btnZoom
         // 
         this.m_btnZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnZoom.Location = new System.Drawing.Point(1310, 27);
         this.m_btnZoom.Name = "m_btnZoom";
         this.m_btnZoom.Size = new System.Drawing.Size(120, 23);
         this.m_btnZoom.TabIndex = 13;
         this.m_btnZoom.Text = "Zoom";
         this.m_btnZoom.UseVisualStyleBackColor = true;
         this.m_btnZoom.Click += new System.EventHandler(this.M_btnZoomout_Click);
         // 
         // m_btnMarkProvinces
         // 
         this.m_btnMarkProvinces.Location = new System.Drawing.Point(528, 27);
         this.m_btnMarkProvinces.Name = "m_btnMarkProvinces";
         this.m_btnMarkProvinces.Size = new System.Drawing.Size(120, 23);
         this.m_btnMarkProvinces.TabIndex = 14;
         this.m_btnMarkProvinces.Text = "Provinces";
         this.m_btnMarkProvinces.UseVisualStyleBackColor = true;
         this.m_btnMarkProvinces.Click += new System.EventHandler(this.M_btnMarkProvinces_Click);
         // 
         // m_btnMarkLocation
         // 
         this.m_btnMarkLocation.Location = new System.Drawing.Point(654, 27);
         this.m_btnMarkLocation.Name = "m_btnMarkLocation";
         this.m_btnMarkLocation.Size = new System.Drawing.Size(120, 23);
         this.m_btnMarkLocation.TabIndex = 15;
         this.m_btnMarkLocation.Text = "Location";
         this.m_btnMarkLocation.UseVisualStyleBackColor = true;
         this.m_btnMarkLocation.Click += new System.EventHandler(this.M_btnMarkLocation_Click);
         // 
         // m_btnMarkOutgoing
         // 
         this.m_btnMarkOutgoing.Location = new System.Drawing.Point(780, 27);
         this.m_btnMarkOutgoing.Name = "m_btnMarkOutgoing";
         this.m_btnMarkOutgoing.Size = new System.Drawing.Size(120, 23);
         this.m_btnMarkOutgoing.TabIndex = 16;
         this.m_btnMarkOutgoing.Text = "Outgoing";
         this.m_btnMarkOutgoing.UseVisualStyleBackColor = true;
         this.m_btnMarkOutgoing.Click += new System.EventHandler(this.M_btnOutgoing_Click);
         // 
         // MainWnd
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1442, 690);
         this.Controls.Add(this.m_btnMarkOutgoing);
         this.Controls.Add(this.m_btnMarkLocation);
         this.Controls.Add(this.m_btnMarkProvinces);
         this.Controls.Add(this.m_btnZoom);
         this.Controls.Add(this.m_btnSave);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.m_cbMarker);
         this.Controls.Add(this.m_cbShowOverlay);
         this.Controls.Add(this.m_btnClear);
         this.Controls.Add(this.m_pnlContentPane);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.menuStrip1);
         this.Name = "MainWnd";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "EU4 Province Editor";
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.m_pnlContentPane.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.m_pbMap)).EndInit();
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.Panel m_pnlContentPane;
      private System.Windows.Forms.PictureBox m_pbMap;
      private System.Windows.Forms.ToolTip toolTip1;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
      private System.Windows.Forms.Button m_btnClear;
      private System.Windows.Forms.CheckBox m_cbShowOverlay;
      private System.Windows.Forms.ToolStripProgressBar m_pbLoading;
      private System.Windows.Forms.ToolStripStatusLabel m_lblLoading;
      private System.Windows.Forms.ComboBox m_cbMarker;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button m_btnSave;
      private System.Windows.Forms.ToolTip m_ttMarkr;
      private System.Windows.Forms.ToolStripMenuItem m_btnLoadTradenodes;
      private System.Windows.Forms.ToolStripMenuItem m_btnSaveTradenodes;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem m_btnClose;
      private System.Windows.Forms.Button m_btnZoom;
      private System.Windows.Forms.ToolStripMenuItem m_btnSaveMap;
      private System.Windows.Forms.Button m_btnMarkProvinces;
      private System.Windows.Forms.Button m_btnMarkLocation;
      private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem m_btnExportTradenodes;
      private System.Windows.Forms.Button m_btnMarkOutgoing;
   }
}

