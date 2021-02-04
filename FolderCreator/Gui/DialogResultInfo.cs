using FolderCreator.Common;
using FolderCreator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCreator.Gui
{
   public partial class DialogResultInfo : Form
   {
      public DialogResultInfo ()
      {
         InitializeComponent ();
      }

      //------------------------------------------------------------------------

      public void SetListViewResult ( List<ResultInfo<ModelDirectory>> listOfResultInfo )
      {
         using ( ListViewUtil listViewUtil = new ListViewUtil ( m_lvResult ) )
         {
            IndexNumberStringUtil indexNumberStringUtil = new IndexNumberStringUtil ();

            foreach ( ResultInfo<ModelDirectory> resultInfo in listOfResultInfo )
            {
               listViewUtil.Add ( new string[] { indexNumberStringUtil.Next (), resultInfo.Tag.GetDirectoryName (), resultInfo.Message });
            }
         }
      }

      //------------------------------------------------------------------------

      private void m_btnClose_Click ( object sender, EventArgs e )
      {
         Close ();
      }
   }
}
