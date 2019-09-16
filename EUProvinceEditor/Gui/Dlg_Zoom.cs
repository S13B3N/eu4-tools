using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUProvinceEditor.Gui
{
   public partial class Dlg_Zoom : Form
   {
      public Dlg_Zoom ()
      {
         InitializeComponent ();

            MaximumSize = Size;
      }

      public void SetImage ( Image image )
      {
         m_pbZoom.Image = image;
      }

      public PictureBox GetPictureBox ()
      {
         return m_pbZoom;
      }
   }
}
