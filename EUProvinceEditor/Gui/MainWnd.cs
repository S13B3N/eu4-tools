using EUProvinceEditor.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUProvinceEditor.Gui
{
   public partial class MainWnd : Form
   {
      private ProvinceEditor m_provinceEditor = new ProvinceEditor ();

      private Thread m_threadWorker;

      private Dlg_Zoom m_dialogZoom;

      //------------------------------------------------------------------------

      public MainWnd ()
      {
         InitializeComponent ();

         m_cbShowOverlay.Enabled = false;
         m_btnClear     .Enabled = false;
         m_cbMarker     .Enabled = false;
         m_btnSave      .Enabled = false;
         m_btnZoom      .Enabled = false;

         m_btnMarkProvinces.Enabled = false;
         m_btnMarkLocation .Enabled = false;
         m_btnMarkOutgoing .Enabled = false;

         m_provinceEditor.OnInitFinished += OnInitFinished;
         m_provinceEditor.OnInitError    += OnInitError   ;

         m_threadWorker = new Thread ( m_provinceEditor.Init );

         m_threadWorker.Start ();
      }

      //------------------------------------------------------------------------

      private void OnInitFinished ()
      {
         if ( InvokeRequired )
         {
            Invoke ( new DInitFinished ( OnInitFinished )); return;
         }

         List<ComboBoxItem<MarkerItem>> listComboItem = new List<ComboBoxItem<MarkerItem>> ();

         foreach ( MarkerItem markerItem in m_provinceEditor.ListMarkerItem )
         {
            listComboItem.Add ( new ComboBoxItem<MarkerItem> ( markerItem, markerItem.Name ));
         }

         m_cbMarker.Enabled = true;

         m_cbMarker.DisplayMember = "Label";
         m_cbMarker.ValueMember   = "Value";

         m_cbMarker.DataSource = listComboItem;

         m_oldMarkerItem = ( MarkerItem ) m_cbMarker.SelectedValue;

         m_pbMap.Width  = m_provinceEditor.BitmapMap.Width ;
         m_pbMap.Height = m_provinceEditor.BitmapMap.Height;

         m_pbMap.BackgroundImage = m_provinceEditor.BitmapMap       ;
         m_pbMap.Image           = m_provinceEditor.BitmapMapOverlay;

         m_pbMap.Click += new EventHandler ( M_pbMap_Click );

         m_pbMap.MouseMove += M_pbMap_MouseMove;

         m_cbShowOverlay.Enabled = true;
         m_btnClear     .Enabled = true;
         m_btnZoom      .Enabled = true;

         m_btnMarkProvinces.Enabled = true;
         m_btnMarkLocation .Enabled = true;
         m_btnMarkOutgoing .Enabled = true;

         m_btnSave.Enabled = true;

         m_btnMarkProvinces.BackColor = System.Drawing.Color.Silver;
         m_btnMarkProvinces.UseVisualStyleBackColor = false;

         m_pbLoading .Visible = false;
         m_lblLoading.Text = "";

         m_threadWorker = null;
      }

      private void OnInitError ()
      {
         m_lblLoading.Text = "Oh, something went wrong :(";

         m_pbLoading .Visible = false;

         MessageBox.Show ( "Oh, something went wrong :(", "Error on init...", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }

      //------------------------------------------------------------------------
      // Picture Box Events
      //------------------------------------------------------------------------

      private void M_pbMap_MouseMove ( object sender, MouseEventArgs e )
      {
         String mouseCoord = String.Format ( "{0}:{1} - ", e.X, e.Y );

         try
         {
            if ( m_dialogZoom != null )
            {
               PictureBox pbZoom = m_dialogZoom .GetPictureBox ();

               m_dialogZoom.SetImage ( m_provinceEditor.ZoomImage ( e.X, e.Y, pbZoom.Width / 2, pbZoom.Height / 2 ));
            }

            Color pixelColor = m_provinceEditor.BitmapMapOverlay.GetPixel ( e.X, e.Y );

            MarkerItem markerItem = m_provinceEditor.ListMarkerItem.Find ( searchItem => ( pixelColor.R == searchItem.R ) && ( pixelColor.G == searchItem.G ) && ( pixelColor.B == searchItem.B ));

            if ( markerItem != null )
            {
               m_lblLoading.Text = mouseCoord + markerItem.Name;
            }
            else
            {
               m_lblLoading.Text = mouseCoord + "";
            }
         }
         catch ( Exception ex )
         {
         }
      }

      private void M_pbMap_Click ( object sender, EventArgs eventArgs )
      {
         MouseEventArgs mouseEventArgs = ( MouseEventArgs ) eventArgs;

         MarkerItem markerItem = ( MarkerItem ) m_cbMarker.SelectedValue;

         if ( m_btnMarkProvinces.BackColor == Color.Silver )
         {
            m_provinceEditor.Mark ( markerItem, mouseEventArgs.X, mouseEventArgs.Y );
         }
         else if ( m_btnMarkLocation.BackColor == Color.Silver )
         {
            m_provinceEditor.MarkLocation ( markerItem, mouseEventArgs.X, mouseEventArgs.Y );
         }
         else if ( m_btnMarkOutgoing.BackColor == Color.Silver )
         {
            m_provinceEditor.MarkOutgoing ( markerItem, mouseEventArgs.X, mouseEventArgs.Y );
         }

         UpdateGui ();
      }

      public void UpdateGui ()
      {
         if ( m_cbShowOverlay.Checked )
         {
            m_pbMap.Image = m_provinceEditor.BitmapMapOverlay;
         }
         else
         {
            m_pbMap.Image = null;
         }
      }

      //------------------------------------------------------------------------
      // Button events
      //------------------------------------------------------------------------

      private void M_btnClear_Click ( object sender, EventArgs e )
      {
         m_provinceEditor.Clear ();

         UpdateGui ();
      }

      private void M_cbShowOverlay_CheckedChanged ( object sender, EventArgs e )
      {
         UpdateGui ();
      }

      private void M_btnSave_Click ( object sender, EventArgs e )
      {
         Dlg_ShowProvinces dialog = new Dlg_ShowProvinces ( m_provinceEditor );

         dialog.ShowDialog ();
      }

      private void M_btnLoadTradenodes_Click ( object sender, EventArgs e )
      {
         OpenFileDialog dialog = new OpenFileDialog ();

         dialog.Filter = "tradenode files (*.eu4tradenode)|*.eu4tradenode";

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            try
            {
               m_provinceEditor.ReadMarker ( dialog.FileName );

               m_provinceEditor.MarkProvinces ();

               m_provinceEditor.MarkLocation ();

               List<ComboBoxItem<MarkerItem>> listComboItem = new List<ComboBoxItem<MarkerItem>> ();

               foreach ( MarkerItem markerItem in m_provinceEditor.ListMarkerItem )
               {
                  listComboItem.Add ( new ComboBoxItem<MarkerItem> ( markerItem, markerItem.Name ) );
               }

               m_cbMarker.Enabled = true;

               m_cbMarker.DisplayMember = "Label";
               m_cbMarker.ValueMember = "Value";

               m_cbMarker.DataSource = listComboItem;

               UpdateGui ();
            }
            catch ( Exception ex )
            {
               MessageBox.Show ( "Cant load tradenodes", "Cant load tradenodes", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
         }
      }

      private void M_btnSaveTradenodes_Click ( object sender, EventArgs e )
      {
         SaveFileDialog dialog = new SaveFileDialog ();

         dialog.Filter = "tradenode files (*.eu4tradenode)|*.eu4tradenode";

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            try
            {
               m_provinceEditor.SaveMarker ( dialog.FileName );
            }
            catch ( Exception ex )
            {
               MessageBox.Show ( "Cant write file!" + ex.Message, "Cant write file!", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
         }
      }

      private void M_btnClose_Click ( object sender, EventArgs e )
      {
         Close ();
      }

      //-------------

      private void M_btnZoomout_Click ( object sender, EventArgs e )
      {
         if ( m_dialogZoom == null )
         {
            m_dialogZoom = new Dlg_Zoom ();

            m_dialogZoom.FormClosing += M_dialogZoom_FormClosing;
         }

         m_dialogZoom.Show ();
      }

      private void M_dialogZoom_FormClosing ( object sender, FormClosingEventArgs e )
      {
         m_dialogZoom = null;
      }

      private void M_btnSaveMap_Click ( object sender, EventArgs e )
      {
         SaveFileDialog dialog = new SaveFileDialog ();

         dialog.Filter = "Bitmap (*.bmp)|*.bmp";

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            try
            {
               m_provinceEditor.SaveMap ( dialog.FileName );
            }
            catch ( Exception ex )
            {
               MessageBox.Show ( "Cant write file!" + ex.Message, "Cant write file!", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
         }
      }

      private void M_btnExportTradenodes_Click ( object sender, EventArgs e )
      {
         SaveFileDialog dialog = new SaveFileDialog ();

         dialog.Filter = "00_tradenode.txt (*.txt)|*.txt";

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            try
            {
               m_provinceEditor.Export ( dialog.FileName );
            }
            catch ( Exception ex )
            {
               MessageBox.Show ( "Cant write file!" + ex.Message, "Cant write file!" );
            }
         }
      }

      private void M_btnMarkProvinces_Click ( object sender, EventArgs e )
      {
         m_btnMarkProvinces.BackColor = Color.Silver;
         m_btnMarkProvinces.UseVisualStyleBackColor = false;

         m_btnMarkLocation.BackColor = SystemColors.Control;
         m_btnMarkLocation.UseVisualStyleBackColor = true;

         m_btnMarkOutgoing.BackColor = SystemColors.Control;
         m_btnMarkOutgoing.UseVisualStyleBackColor = true;
      }

      private void M_btnMarkLocation_Click ( object sender, EventArgs e )
      {
         m_btnMarkLocation.BackColor = Color.Silver;
         m_btnMarkLocation.UseVisualStyleBackColor = false;

         m_btnMarkProvinces.BackColor = SystemColors.Control;
         m_btnMarkProvinces.UseVisualStyleBackColor = true;

         m_btnMarkOutgoing.BackColor = SystemColors.Control;
         m_btnMarkOutgoing.UseVisualStyleBackColor = true;
      }

      private void M_btnOutgoing_Click(object sender, EventArgs e)
      {
         m_btnMarkOutgoing.BackColor = Color.Silver;
         m_btnMarkOutgoing.UseVisualStyleBackColor = false;

         m_btnMarkProvinces.BackColor = SystemColors.Control;
         m_btnMarkProvinces.UseVisualStyleBackColor = true;

         m_btnMarkLocation.BackColor = SystemColors.Control;
         m_btnMarkLocation.UseVisualStyleBackColor = true;
      }

      private MarkerItem m_oldMarkerItem = null;

      private void M_cbMarker_SelectedIndexChanged ( object sender, EventArgs e )
      {
         if ( m_cbMarker.SelectedValue is MarkerItem )
         {
            MarkerItem markerItem = ( MarkerItem ) m_cbMarker.SelectedValue;

            m_provinceEditor.ChangeMarker ( m_oldMarkerItem, markerItem );

            m_oldMarkerItem = markerItem;

            UpdateGui ();
         }
      }
   }
}
