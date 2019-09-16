namespace EUProvinceEditor.Gui
{
   partial class Dlg_Zoom
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
         this.m_pbZoom = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.m_pbZoom)).BeginInit();
         this.SuspendLayout();
         // 
         // m_pbZoom
         // 
         this.m_pbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.m_pbZoom.Location = new System.Drawing.Point(12, 12);
         this.m_pbZoom.Name = "m_pbZoom";
         this.m_pbZoom.Size = new System.Drawing.Size(360, 337);
         this.m_pbZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
         this.m_pbZoom.TabIndex = 0;
         this.m_pbZoom.TabStop = false;
         // 
         // Dlg_Zoom
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(384, 361);
         this.Controls.Add(this.m_pbZoom);
         this.MaximumSize = new System.Drawing.Size(400, 400);
         this.Name = "Dlg_Zoom";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Zoom";
         this.TopMost = true;
         ((System.ComponentModel.ISupportInitialize)(this.m_pbZoom)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox m_pbZoom;
   }
}