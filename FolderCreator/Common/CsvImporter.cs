using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Common
{
   public class CsvImporter
   {
      private String m_pathOfExcel;

      //------------------------------------------------------------------------

      public CsvImporter ( String pathOfExcel )
      {
         m_pathOfExcel = pathOfExcel;
      }

      //------------------------------------------------------------------------

      public List<int> Import ()
      {
         List<int> listOfLines = new List<int> ();

         try
         {
            if ( File.Exists ( m_pathOfExcel ))
            {
               string[] arrayOfLines = File.ReadAllLines ( m_pathOfExcel );

               foreach ( string line in arrayOfLines )
               {
                  string[] arrayOfSplittedLine = line.Split ( ',' );

                  foreach ( string value in arrayOfSplittedLine )
                  {
                     int number;

                     if ( int.TryParse ( value, out number ))
                     {
                        listOfLines.Add ( number );
                     }
                  }
               }
            }
         }
         catch ( Exception )
         {
         }

         return listOfLines;
      }
   }
}
