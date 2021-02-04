using FolderCreator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Common
{
   public class FolderCreatorUtil
   {
      public bool CheckDirectory ( string pathOfDirectory )
      {
         bool bValid = false;

         try
         {
            bValid = Directory.Exists ( pathOfDirectory );
         }
         catch ( Exception )
         {
         }

         return bValid;
      }

      public List<ResultInfo<ModelDirectory>> CreateFolder ( List<ModelDirectory> listOfModelDirectory )
      {
         List<ResultInfo<ModelDirectory>> listOfResultInfo = new List<ResultInfo<ModelDirectory>> ();

         foreach ( ModelDirectory modelDirectory in listOfModelDirectory )
         {
            ResultInfo<ModelDirectory> resultInfo = new ResultInfo<ModelDirectory> ();

            try
            {
               if ( Directory.Exists ( modelDirectory.GetDirectoryName ()))
               {
                  resultInfo.SetResult ( 0, "Ordner ist bereits angelegt", modelDirectory, "", 0, "", "" );
               }
               else
               {
                  Directory.CreateDirectory ( modelDirectory.GetDirectoryName ());

                  resultInfo.SetResult ( 0, "Ordner erfolgreich angelegt", modelDirectory, "", 0, "", "" );
               }
            }
            catch ( Exception ex )
            {
               resultInfo.SetResult ( 1, ex.Message, modelDirectory, "", 0, "", "" );
            }

            listOfResultInfo.Add ( resultInfo );
         }

         return listOfResultInfo;
      }
   }
}
