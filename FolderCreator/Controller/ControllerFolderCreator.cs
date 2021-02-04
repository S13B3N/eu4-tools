using FolderCreator.Common;
using FolderCreator.Gui;
using FolderCreator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCreator.Controller
{
   public class ControllerFolderCreator
   {
      protected MainWnd m_mainWnd;

      protected ModelFolderCreator m_modelFolderCreator;

      //------------------------------------------------------------------------

      public ControllerFolderCreator ( MainWnd mainWnd )
      {
         m_mainWnd = mainWnd;

         m_modelFolderCreator = new ModelFolderCreator ();
      }

      //------------------------------------------------------------------------
      // Events
      //------------------------------------------------------------------------

      public void OnTxtValidatingPathOfDirectory ()
      {
         m_modelFolderCreator.SetPathOfDirectory ( m_mainWnd.GetPathOfDirectory ());

         m_mainWnd.SetListViewPreview ( m_modelFolderCreator.ListOfModelDirectory );
      }

      public void OnClickBrowseDestination ()
      {
         FolderBrowserDialog dialog = new FolderBrowserDialog ();

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            m_modelFolderCreator.SetPathOfDirectory ( dialog.SelectedPath );

            m_mainWnd.SetPathOfDirectory ( dialog.SelectedPath );

            m_mainWnd.SetListViewPreview ( m_modelFolderCreator.ListOfModelDirectory );
         }
      }

      //------------------------------------------------------------------------

      public void OnClickExcel ()
      {
         OpenFileDialog dialog = new OpenFileDialog ();

         if ( dialog.ShowDialog () == DialogResult.OK )
         {
            ResultInfo<Object> resultInfo = m_modelFolderCreator.ImportFromExcel ( dialog.FileName );

            m_mainWnd.SetListViewPreview ( m_modelFolderCreator.ListOfModelDirectory );

            if ( resultInfo.IsNotOK () )
            {
               m_mainWnd.ShowMessageBox ( resultInfo.Message );
            }
         }
      }

      public void OnClickFromTo ()
      {
      }

      public void OnClickFromList ()
      {
      }

      public void OnClickCreateDirectories ()
      {
         FolderCreatorUtil folderCreatorUtil = new FolderCreatorUtil ();

         if ( folderCreatorUtil.CheckDirectory ( m_modelFolderCreator.PathOfDirectory ))
         {
            if ( m_modelFolderCreator.ListOfModelDirectory.Count > 0 )
            {
               List<ResultInfo<ModelDirectory>> listOfResultInfo = folderCreatorUtil.CreateFolder ( m_modelFolderCreator.ListOfModelDirectory );

               DialogResultInfo dialogResultInfo = new DialogResultInfo ();

               dialogResultInfo.SetListViewResult ( listOfResultInfo );

               dialogResultInfo.ShowDialog ();
            }
            else
            {
               m_mainWnd.ShowMessageBox ( "Es sind keine Unterverzeichnisse vorhanden!" );
            }
         }
         else
         {
            m_mainWnd.ShowMessageBox ( String.Format ( "Das Stammverzeichnis {0} ist kein gültiges Verzeichnis!", m_modelFolderCreator.PathOfDirectory ));
         }
      }
   }
}
