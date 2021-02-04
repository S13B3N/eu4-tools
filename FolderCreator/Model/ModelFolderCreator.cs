using FolderCreator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Model
{
   public class ModelFolderCreator
   {
      public ModelFolderCreator ()
      {
         PathOfDirectory = "";

         ListOfModelDirectory = new List<ModelDirectory> ();
      }

      //------------------------------------------------------------------------

      public void SetPathOfDirectory ( String pathOfDirectory )
      {
         PathOfDirectory = pathOfDirectory;
      }

      //------------------------------------------------------------------------

      public ResultInfo<Object> ImportFromExcel ( String pathOfExcel )
      {
         ResultInfo<Object> resultInfo = new ResultInfo<Object> ();

         ListOfModelDirectory.Clear ();

         try
         {
            FactoryModelDirectory factoryModelDirectory = new FactoryModelDirectory ( this );

            CsvImporter csvImport = new CsvImporter ( pathOfExcel );

            List<int> listOfNumbers = csvImport.Import ();

            foreach ( int number in listOfNumbers )
            {
               ListOfModelDirectory.Add ( factoryModelDirectory.Construct ( number ));
            }

            if ( listOfNumbers.Count == 0 )
            {
               resultInfo.SetResult ( 1, "Es konnten keine Verzeichnisse gefunden werden", null );
            }
            else
            {
               resultInfo.SetResult ( 0, "", null );
            }
         }
         catch ( Exception )
         {
            ListOfModelDirectory.Clear ();
         }

         return resultInfo;
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public String               PathOfDirectory      { get; set; }
      public List<ModelDirectory> ListOfModelDirectory { get; set; }
   }
}
