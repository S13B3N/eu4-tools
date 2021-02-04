using FolderCreator.Common;
using FolderCreator.Controller;
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
   public partial class MainWnd : Form
   {
      protected ControllerFolderCreator m_controllerFolderCreator;

      //------------------------------------------------------------------------

      public MainWnd ()
      {
         InitializeComponent ();

         m_controllerFolderCreator = new ControllerFolderCreator ( this );

         MinimumSize = Size;
      }

      //------------------------------------------------------------------------
      // Textbox events
      //------------------------------------------------------------------------

      private void m_txtPathOfDestination_Validating ( object sender, CancelEventArgs e )
      {
         m_controllerFolderCreator.OnTxtValidatingPathOfDirectory ();
      }

      //------------------------------------------------------------------------
      // Button events
      //------------------------------------------------------------------------

      private void m_btnBrowsePathDestination_Click ( object sender, EventArgs e )
      {
         m_controllerFolderCreator.OnClickBrowseDestination ();
      }

      private void mb_tnImportCSV_Click ( object sender, EventArgs e )
      {
         m_controllerFolderCreator.OnClickExcel ();
      }

      private void m_btnFromTo_Click ( object sender, EventArgs e )
      {
         MessageBox.Show ( "Noch nicnt implementiert :(", "Sorry!" );
      }

      private void m_btnList_Click ( object sender, EventArgs e )
      {
         MessageBox.Show ( "Noch nicnt implementiert :(", "Sorry!" );
      }

      private void m_btnCreateFolder_Click ( object sender, EventArgs e )
      {
         m_controllerFolderCreator.OnClickCreateDirectories ();
      }

      //------------------------------------------------------------------------

      public String GetPathOfDirectory ()
      {
         return m_txtPathOfDestination.Text;
      }

      public void SetPathOfDirectory ( String pathOfDirectory )
      {
         m_txtPathOfDestination.Text = pathOfDirectory;
      }

      public void SetListViewPreview ( List<ModelDirectory> listOfModelDirectory )
      {
         using ( ListViewUtil listViewUtil = new ListViewUtil ( m_lvPreview ) )
         {
            IndexNumberStringUtil indexNumberStringUtil = new IndexNumberStringUtil ();

            foreach ( ModelDirectory modelDirectory in listOfModelDirectory )
            {
               listViewUtil.Add ( new string[] 
               {
                  indexNumberStringUtil.Next (),

                  modelDirectory.GetDirectoryName (),

                  modelDirectory.State (),
               });
            }
         }
      }

      //------------------------------------------------------------------------

      public void SetFocusPathOfDirectory ()
      {
         m_txtPathOfDestination.Focus ();
      }

      public void SetFocusBrowseDirectory ()
      {
         m_btnBrowsePathDestination.Focus ();
      }

      //------------------------------------------------------------------------

      internal void ShowMessageBox ( string message )
      {
         MessageBox.Show ( message, "Hinweis" );
      }
   }
}
