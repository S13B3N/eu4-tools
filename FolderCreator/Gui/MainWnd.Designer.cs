
namespace FolderCreator.Gui
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
         this.m_txtPathOfDestination = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.m_btnBrowsePathDestination = new System.Windows.Forms.Button();
         this.m_btnCreateFolder = new System.Windows.Forms.Button();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.m_lvPreview = new System.Windows.Forms.ListView();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.mb_tnImportCSV = new System.Windows.Forms.Button();
         this.m_btnFromTo = new System.Windows.Forms.Button();
         this.m_btnList = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.SuspendLayout();
         // 
         // m_txtPathOfDestination
         // 
         this.m_txtPathOfDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_txtPathOfDestination.Location = new System.Drawing.Point(110, 19);
         this.m_txtPathOfDestination.Name = "m_txtPathOfDestination";
         this.m_txtPathOfDestination.Size = new System.Drawing.Size(604, 20);
         this.m_txtPathOfDestination.TabIndex = 0;
         this.m_txtPathOfDestination.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtPathOfDestination_Validating);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(6, 23);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(98, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Stammverzeichnis :";
         // 
         // m_btnBrowsePathDestination
         // 
         this.m_btnBrowsePathDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnBrowsePathDestination.Location = new System.Drawing.Point(720, 19);
         this.m_btnBrowsePathDestination.Name = "m_btnBrowsePathDestination";
         this.m_btnBrowsePathDestination.Size = new System.Drawing.Size(20, 20);
         this.m_btnBrowsePathDestination.TabIndex = 2;
         this.m_btnBrowsePathDestination.Text = "?";
         this.m_btnBrowsePathDestination.UseVisualStyleBackColor = true;
         this.m_btnBrowsePathDestination.Click += new System.EventHandler(this.m_btnBrowsePathDestination_Click);
         // 
         // m_btnCreateFolder
         // 
         this.m_btnCreateFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnCreateFolder.Location = new System.Drawing.Point(12, 547);
         this.m_btnCreateFolder.Name = "m_btnCreateFolder";
         this.m_btnCreateFolder.Size = new System.Drawing.Size(746, 23);
         this.m_btnCreateFolder.TabIndex = 3;
         this.m_btnCreateFolder.Text = "Verzeichnisse erstellen";
         this.m_btnCreateFolder.UseVisualStyleBackColor = true;
         this.m_btnCreateFolder.Click += new System.EventHandler(this.m_btnCreateFolder_Click);
         // 
         // statusStrip1
         // 
         this.statusStrip1.Location = new System.Drawing.Point(0, 573);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(770, 22);
         this.statusStrip1.TabIndex = 7;
         this.statusStrip1.Text = "m_statusBar";
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.m_txtPathOfDestination);
         this.groupBox1.Controls.Add(this.m_btnBrowsePathDestination);
         this.groupBox1.Location = new System.Drawing.Point(12, 12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(746, 52);
         this.groupBox1.TabIndex = 8;
         this.groupBox1.TabStop = false;
         // 
         // groupBox2
         // 
         this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox2.Controls.Add(this.m_lvPreview);
         this.groupBox2.Location = new System.Drawing.Point(12, 185);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(746, 356);
         this.groupBox2.TabIndex = 9;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Vorschau";
         // 
         // m_lvPreview
         // 
         this.m_lvPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lvPreview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
         this.m_lvPreview.FullRowSelect = true;
         this.m_lvPreview.GridLines = true;
         this.m_lvPreview.HideSelection = false;
         this.m_lvPreview.Location = new System.Drawing.Point(6, 19);
         this.m_lvPreview.MultiSelect = false;
         this.m_lvPreview.Name = "m_lvPreview";
         this.m_lvPreview.Size = new System.Drawing.Size(734, 331);
         this.m_lvPreview.TabIndex = 0;
         this.m_lvPreview.UseCompatibleStateImageBehavior = false;
         this.m_lvPreview.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "#";
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Verzeichnis";
         this.columnHeader2.Width = 530;
         // 
         // columnHeader3
         // 
         this.columnHeader3.Text = "Status";
         this.columnHeader3.Width = 138;
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.m_btnList);
         this.groupBox3.Controls.Add(this.m_btnFromTo);
         this.groupBox3.Controls.Add(this.mb_tnImportCSV);
         this.groupBox3.Location = new System.Drawing.Point(12, 70);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(740, 109);
         this.groupBox3.TabIndex = 3;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Verzeichnisse erstellen";
         // 
         // mb_tnImportCSV
         // 
         this.mb_tnImportCSV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.mb_tnImportCSV.Location = new System.Drawing.Point(6, 19);
         this.mb_tnImportCSV.Name = "mb_tnImportCSV";
         this.mb_tnImportCSV.Size = new System.Drawing.Size(728, 23);
         this.mb_tnImportCSV.TabIndex = 0;
         this.mb_tnImportCSV.Text = "CSV Datei importieren";
         this.mb_tnImportCSV.UseVisualStyleBackColor = true;
         this.mb_tnImportCSV.Click += new System.EventHandler(this.mb_tnImportCSV_Click);
         // 
         // m_btnFromTo
         // 
         this.m_btnFromTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnFromTo.Location = new System.Drawing.Point(6, 48);
         this.m_btnFromTo.Name = "m_btnFromTo";
         this.m_btnFromTo.Size = new System.Drawing.Size(728, 23);
         this.m_btnFromTo.TabIndex = 1;
         this.m_btnFromTo.Text = "Verzeichnis Von - Bis";
         this.m_btnFromTo.UseVisualStyleBackColor = true;
         this.m_btnFromTo.Click += new System.EventHandler(this.m_btnFromTo_Click);
         // 
         // m_btnList
         // 
         this.m_btnList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnList.Location = new System.Drawing.Point(6, 77);
         this.m_btnList.Name = "m_btnList";
         this.m_btnList.Size = new System.Drawing.Size(728, 23);
         this.m_btnList.TabIndex = 2;
         this.m_btnList.Text = "Listeneingabe";
         this.m_btnList.UseVisualStyleBackColor = true;
         this.m_btnList.Click += new System.EventHandler(this.m_btnList_Click);
         // 
         // MainWnd
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(770, 595);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.m_btnCreateFolder);
         this.Name = "MainWnd";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Folder creator";
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox3.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox m_txtPathOfDestination;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button m_btnBrowsePathDestination;
      private System.Windows.Forms.Button m_btnCreateFolder;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ListView m_lvPreview;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.ColumnHeader columnHeader3;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button m_btnList;
      private System.Windows.Forms.Button m_btnFromTo;
      private System.Windows.Forms.Button mb_tnImportCSV;
   }
}

