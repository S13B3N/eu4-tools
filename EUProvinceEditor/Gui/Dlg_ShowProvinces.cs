using EUProvinceEditor.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUProvinceEditor.Gui
{
   public partial class Dlg_ShowProvinces : Form
   {
      private ProvinceEditor m_provinceEditor;

      //------------------------------------------------------------------------

      public Dlg_ShowProvinces ()
      {
         InitializeComponent ();
      }

      public Dlg_ShowProvinces ( ProvinceEditor provinceEditor )
      {
         InitializeComponent ();

         m_provinceEditor = provinceEditor;

         UpdateGui ();
      }

      //------------------------------------------------------------------------

      public void UpdateGui ()
      {
         StringBuilder text = new StringBuilder ( 100000 );

         foreach ( MarkerItem markerItem in m_provinceEditor.MarkerItems )
         {
            text.Append ( markerItem.Name + " = " );

            bool bComma = false;

            markerItem.Provinces.Sort (( sortItem1, sortItem2 ) => ( sortItem1.Id - sortItem2.Id ));

            foreach ( ProvinceDefinitionItem provinceDefinitionItem in markerItem.Provinces )
            {
               if ( bComma )
               {
                  text.Append ( " " );
               }

               text.Append ( provinceDefinitionItem.Id );

               bComma = true;
            }

            text.Append ( Environment.NewLine );
         }

         m_txtProvinces.Text = text.ToString ();
      }
   }
}
