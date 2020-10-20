using EUProvinceEditor.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EUProvinceEditor.Gui
{
    public partial class MainWnd : Form
    {
        private readonly ProvinceEditor m_provinceEditor = new ProvinceEditor();
        private Thread m_threadWorker;
        private Dlg_Zoom m_dialogZoom;

        public MainWnd()
        {
            InitializeComponent();

            m_cbShowOverlay.Enabled = false;
            m_btnClear.Enabled = false;
            m_cbMarker.Enabled = false;
            m_btnSave.Enabled = false;
            m_btnZoom.Enabled = false;

            m_btnMarkProvinces.Enabled = false;
            m_btnMarkLocation.Enabled = false;
            m_btnMarkOutgoing.Enabled = false;

            m_provinceEditor.OnInitFinished += OnInitFinished;
            m_provinceEditor.OnInitError += OnInitError;

            m_threadWorker = new Thread(m_provinceEditor.Init);

            m_threadWorker.Start();
        }

        private void OnInitFinished()
        {
            if (InvokeRequired)
            {
                Invoke(new DInitFinished(OnInitFinished)); return;
            }

            var listComboItem = m_provinceEditor.MarkerItems
                .Select(markerItem => new ComboBoxItem<MarkerItem>(markerItem, markerItem.Name))
                .ToList();

            m_cbMarker.Enabled = true;

            m_cbMarker.DisplayMember = "Label";
            m_cbMarker.ValueMember = "Value";

            m_cbMarker.DataSource = listComboItem;

            m_oldMarkerItem = (MarkerItem)m_cbMarker.SelectedValue;

            m_pbMap.Width = m_provinceEditor.BitmapMap.Width;
            m_pbMap.Height = m_provinceEditor.BitmapMap.Height;

            m_pbMap.BackgroundImage = m_provinceEditor.BitmapMap;
            m_pbMap.Image = m_provinceEditor.BitmapMapOverlay;

            m_pbMap.Click += PbMap_Click;

            m_pbMap.MouseMove += PbMap_MouseMove;

            m_cbShowOverlay.Enabled = true;
            m_btnClear.Enabled = true;
            m_btnZoom.Enabled = true;

            m_btnMarkProvinces.Enabled = true;
            m_btnMarkLocation.Enabled = true;
            m_btnMarkOutgoing.Enabled = true;

            m_btnSave.Enabled = true;

            m_btnMarkProvinces.BackColor = Color.Silver;
            m_btnMarkProvinces.UseVisualStyleBackColor = false;

            m_pbLoading.Visible = false;
            m_lblLoading.Text = string.Empty;

            m_threadWorker = null;
        }

        private void OnInitError()
        {
            m_lblLoading.Text = "Oh, something went wrong :(";

            m_pbLoading.Visible = false;

            MessageBox.Show("Oh, something went wrong :(", "Error on init...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void PbMap_MouseMove(object sender, MouseEventArgs e)
        {
            string mouseCoord = $"{e.X}:{e.Y} - ";

            try
            {
                if (m_dialogZoom != null)
                {
                    PictureBox pbZoom = m_dialogZoom.GetPictureBox();

                    m_dialogZoom.SetImage(m_provinceEditor.ZoomImage(e.X, e.Y, pbZoom.Width / 2, pbZoom.Height / 2));
                }

                Color pixelColor = m_provinceEditor.BitmapMapOverlay.GetPixel(e.X, e.Y);

                MarkerItem markerItem = m_provinceEditor.MarkerItems.Find(searchItem => (pixelColor.R == searchItem.R) && (pixelColor.G == searchItem.G) && (pixelColor.B == searchItem.B));

                if (markerItem != null)
                {
                    m_lblLoading.Text = mouseCoord + markerItem.Name;
                }
                else
                {
                    m_lblLoading.Text = mouseCoord + "";
                }
            }
            finally { }
        }

        private void PbMap_Click(object sender, EventArgs eventArgs)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)eventArgs;

            MarkerItem markerItem = (MarkerItem)m_cbMarker.SelectedValue;

            if (m_btnMarkProvinces.BackColor == Color.Silver)
            {
                m_provinceEditor.Mark(markerItem, mouseEventArgs.X, mouseEventArgs.Y);
            }
            else if (m_btnMarkLocation.BackColor == Color.Silver)
            {
                m_provinceEditor.MarkLocation(markerItem, mouseEventArgs.X, mouseEventArgs.Y);
            }
            else if (m_btnMarkOutgoing.BackColor == Color.Silver)
            {
                m_provinceEditor.MarkOutgoing(markerItem, mouseEventArgs.X, mouseEventArgs.Y);
            }

            UpdateGui();
        }

        public void UpdateGui()
        {
            m_pbMap.Image = m_cbShowOverlay.Checked ? 
                m_provinceEditor.BitmapMapOverlay : 
                null;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            m_provinceEditor.Clear();

            UpdateGui();
        }

        private void CbShowOverlay_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var dialog = new Dlg_ShowProvinces(m_provinceEditor);
            dialog.ShowDialog();
        }

        private void BtnLoadTradenodes_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "tradenode files (*.eu4tradenode)|*.eu4tradenode"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                m_provinceEditor.ReadMarker(openFileDialog.FileName);

                m_provinceEditor.MarkProvinces();

                m_provinceEditor.MarkLocation();

                var listComboItem = m_provinceEditor.MarkerItems
                    .Select(mi => new ComboBoxItem<MarkerItem>(mi, mi.Name))
                    .ToList();

                m_cbMarker.Enabled = true;

                m_cbMarker.DisplayMember = "Label";
                m_cbMarker.ValueMember = "Value";

                m_cbMarker.DataSource = listComboItem;

                UpdateGui();
            }
            catch
            {
                MessageBox.Show("Can't load tradenodes", "Can't load tradenodes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveTradenodes_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "tradenode files (*.eu4tradenode)|*.eu4tradenode"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                m_provinceEditor.SaveMarker(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant write file!" + ex.Message, "Cant write file!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void BtnZoomout_Click(object sender, EventArgs e)
        {
            if (m_dialogZoom == null)
            {
                m_dialogZoom = new Dlg_Zoom();

                m_dialogZoom.FormClosing += DialogZoom_FormClosing;
            }

            m_dialogZoom.Show();
        }

        private void DialogZoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_dialogZoom = null;
        }

        private void BtnSaveMap_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "Bitmap (*.bmp)|*.bmp" };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                m_provinceEditor.SaveMap(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant write file!" + ex.Message, "Cant write file!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportTradenodes_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "00_tradenode.txt (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                m_provinceEditor.Export(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant write file!" + ex.Message, "Cant write file!");
            }
        }

        private void BtnMarkProvinces_Click(object sender, EventArgs e)
        {
            m_btnMarkProvinces.BackColor = Color.Silver;
            m_btnMarkProvinces.UseVisualStyleBackColor = false;

            m_btnMarkLocation.BackColor = SystemColors.Control;
            m_btnMarkLocation.UseVisualStyleBackColor = true;

            m_btnMarkOutgoing.BackColor = SystemColors.Control;
            m_btnMarkOutgoing.UseVisualStyleBackColor = true;
        }

        private void BtnMarkLocation_Click(object sender, EventArgs e)
        {
            m_btnMarkLocation.BackColor = Color.Silver;
            m_btnMarkLocation.UseVisualStyleBackColor = false;

            m_btnMarkProvinces.BackColor = SystemColors.Control;
            m_btnMarkProvinces.UseVisualStyleBackColor = true;

            m_btnMarkOutgoing.BackColor = SystemColors.Control;
            m_btnMarkOutgoing.UseVisualStyleBackColor = true;
        }

        private void BtnOutgoing_Click(object sender, EventArgs e)
        {
            m_btnMarkOutgoing.BackColor = Color.Silver;
            m_btnMarkOutgoing.UseVisualStyleBackColor = false;

            m_btnMarkProvinces.BackColor = SystemColors.Control;
            m_btnMarkProvinces.UseVisualStyleBackColor = true;

            m_btnMarkLocation.BackColor = SystemColors.Control;
            m_btnMarkLocation.UseVisualStyleBackColor = true;
        }

        private MarkerItem m_oldMarkerItem;
        private void CbMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(m_cbMarker.SelectedValue is MarkerItem markerItem)) return;
            
            m_provinceEditor.ChangeMarker(m_oldMarkerItem, markerItem);
            m_oldMarkerItem = markerItem;
            UpdateGui();
        }
    }
}
