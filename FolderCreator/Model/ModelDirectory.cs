using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Model
{
   public class ModelDirectory
   {
      private ModelFolderCreator m_modelFolderCreator;

      //------------------------------------------------------------------------

      public ModelDirectory ( ModelFolderCreator modelFolderCreator )
      {
         m_modelFolderCreator = modelFolderCreator;

         Number          = 0    ;
         Exists          = false;
         CountOfFiles    = 0    ;
      }

      //------------------------------------------------------------------------

      public string State ()
      {
         string state = "";

         if ( Exists )
         {
            state = string.Format ( "Verzeichnis ist Vorhanden, {0} Datei(en) im Verzeichnis", CountOfFiles );
         }
         else
         {
            state = "Verzeichnis nicht vorhanden";
         }

         return state;
      }

      //------------------------------------------------------------------------

      public string GetDirectoryName ()
      {
         return string.Format ( @"{0}\{1}", m_modelFolderCreator.PathOfDirectory, Number );
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public int  Number          { get; set; }
      public bool Exists          { get; set; }
      public int  CountOfFiles    { get; set; }
   }
}
