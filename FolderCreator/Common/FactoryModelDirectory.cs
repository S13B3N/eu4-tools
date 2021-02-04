using FolderCreator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Common
{
   public class FactoryModelDirectory
   {
      private ModelFolderCreator m_modelFolderCreator;

      //------------------------------------------------------------------------

      public FactoryModelDirectory ( ModelFolderCreator modelFolderCreator )
      {
         m_modelFolderCreator = modelFolderCreator;
      }

      //------------------------------------------------------------------------

      public ModelDirectory Construct ( int number )
      {
         ModelDirectory modelDirectory = new ModelDirectory ( m_modelFolderCreator );

         modelDirectory.Number = number;

         try
         {
            if ( Directory.Exists ( modelDirectory.GetDirectoryName ()))
            {
               modelDirectory.Exists = true;

               modelDirectory.CountOfFiles = new DirectoryInfo (  modelDirectory.GetDirectoryName ()).GetFiles ().Length;
            }
            else
            {
               modelDirectory.Exists = false;

               modelDirectory.CountOfFiles = 0;
            }
         }
         catch ( Exception )
         {
            modelDirectory.Exists = false;

            modelDirectory.CountOfFiles = 0;
         }

         return modelDirectory;
      }
   }
}
