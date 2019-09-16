using System;
using System.Collections.Generic;


namespace EUProvinceEditor.Common
{
   public class ProvinceDefinitionItem
   {
      private int    m_idProvince;
      private int    m_r         ;
      private int    m_g         ;
      private int    m_b         ;
      private String m_nameOf    ;
      private String m_x         ;

      private List<Pixel> m_listPixel = new List<Pixel> ();

      //------------------------------------------------------------------------

      public ProvinceDefinitionItem ()
      {
         m_idProvince = 0 ;
         m_r          = 0 ;
         m_g          = 0 ;
         m_b          = 0 ;
         m_nameOf     = "";
         m_x          = "";
      }

      public ProvinceDefinitionItem ( int idProvince, int r, int g, int b, String nameOf, String x )
      {
         m_idProvince = idProvince;
         m_r          = r         ;
         m_g          = g         ;
         m_b          = b         ;
         m_nameOf     = nameOf    ;
         m_x          = x         ;
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public int         IdProvince { get => m_idProvince; set => m_idProvince = value; }
      public int         R          { get => m_r         ; set => m_r          = value; }
      public int         G          { get => m_g         ; set => m_g          = value; }
      public int         B          { get => m_b         ; set => m_b          = value; }
      public String      NameOf     { get => m_nameOf    ; set => m_nameOf     = value; }
      public String      X          { get => m_x         ; set => m_x          = value; }
      public List<Pixel> ListPixel  { get => m_listPixel ; set => m_listPixel  = value; }
   }
}
