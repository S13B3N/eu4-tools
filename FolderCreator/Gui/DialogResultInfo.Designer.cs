
namespace FolderCreator.Gui
{
   partial class DialogResultInfo
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
         this.m_lvResult = new System.Windows.Forms.ListView();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.m_btnClose = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // m_lvResult
         // 
         this.m_lvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
         this.m_lvResult.FullRowSelect = true;
         this.m_lvResult.GridLines = true;
         this.m_lvResult.HideSelection = false;
         this.m_lvResult.Location = new System.Drawing.Point(12, 12);
         this.m_lvResult.Name = "m_lvResult";
         this.m_lvResult.Size = new System.Drawing.Size(776, 397);
         this.m_lvResult.TabIndex = 1;
         this.m_lvResult.UseCompatibleStateImageBehavior = false;
         this.m_lvResult.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "#";
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Verzeichnis";
         this.columnHeader2.Width = 460;
         // 
         // columnHeader3
         // 
         this.columnHeader3.Text = "Ergebnis";
         this.columnHeader3.Width = 250;
         // 
         // m_btnClose
         // 
         this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnClose.Location = new System.Drawing.Point(713, 415);
         this.m_btnClose.Name = "m_btnClose";
         this.m_btnClose.Size = new System.Drawing.Size(75, 23);
         this.m_btnClose.TabIndex = 2;
         this.m_btnClose.Text = "Schließen";
         this.m_btnClose.UseVisualStyleBackColor = true;
         this.m_btnClose.Click += new System.EventHandler(this.m_btnClose_Click);
         // 
         // DialogResultInfo
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.m_btnClose);
         this.Controls.Add(this.m_lvResult);
         this.Name = "DialogResultInfo";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Ergebnis";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ListView m_lvResult;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.ColumnHeader columnHeader3;
      private System.Windows.Forms.Button m_btnClose;
   }
}