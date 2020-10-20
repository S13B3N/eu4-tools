using System.Drawing;
using System.Windows.Forms;

namespace EUProvinceEditor.Gui
{
    public partial class Dlg_Zoom : Form
    {
        public Dlg_Zoom()
        {
            InitializeComponent();
            MaximumSize = Size;
        }

        public void SetImage(Image image) => m_pbZoom.Image = image;
        public PictureBox GetPictureBox() => m_pbZoom;
    }
}
