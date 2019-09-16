namespace EUProvinceEditor.Gui
{
   partial class Dlg_ShowProvinces
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
         this.m_txtProvinces = new System.Windows.Forms.TextBox();
         this.m_btnClose = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // m_txtProvinces
         // 
         this.m_txtProvinces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_txtProvinces.Location = new System.Drawing.Point(12, 12);
         this.m_txtProvinces.Multiline = true;
         this.m_txtProvinces.Name = "m_txtProvinces";
         this.m_txtProvinces.ReadOnly = true;
         this.m_txtProvinces.Size = new System.Drawing.Size(1128, 495);
         this.m_txtProvinces.TabIndex = 0;
         // 
         // m_btnClose
         // 
         this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_btnClose.Location = new System.Drawing.Point(12, 513);
         this.m_btnClose.Name = "m_btnClose";
         this.m_btnClose.Size = new System.Drawing.Size(1128, 23);
         this.m_btnClose.TabIndex = 1;
         this.m_btnClose.Text = "Close";
         this.m_btnClose.UseVisualStyleBackColor = true;
         // 
         // Dlg_ShowProvinces
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1152, 548);
         this.Controls.Add(this.m_btnClose);
         this.Controls.Add(this.m_txtProvinces);
         this.Name = "Dlg_ShowProvinces";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Show provinces";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox m_txtProvinces;
      private System.Windows.Forms.Button m_btnClose;
   }
}