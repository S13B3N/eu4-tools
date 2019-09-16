using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUProvinceEditor.Common
{
   public class MarkerItem
   {
      private String m_name     = "";
      private int    m_r        = 0 ;
      private int    m_g        = 0 ;
      private int    m_b        = 0 ;
      private int    m_a        = 0 ;
      private int    m_location = 0 ;

      private String m_outgoing;

      private List<ProvinceDefinitionItem> m_listProvinces = new List<ProvinceDefinitionItem> ();

      private List<MarkerItem> m_listOutgoing = new List<MarkerItem> ();

      private Color m_color;

      //------------------------------------------------------------------------

      public MarkerItem ()
      {
      }

      public MarkerItem ( String name, int r, int g, int b, int a, int location, String outgoing )
      {
         m_name     = name    ;
         m_r        = r       ;
         m_g        = g       ;
         m_b        = b       ;
         m_a        = a       ;
         m_location = location;
         m_outgoing = outgoing;

         m_color = Color.FromArgb ( R, G, B );
      }

      public String ListMembers ()
      {
         StringBuilder memberBuilder = new StringBuilder ();

         foreach ( ProvinceDefinitionItem provinceDefinitionItem in ListProvince )
         {
            memberBuilder.Append ( " " + provinceDefinitionItem.IdProvince );
         }

         return memberBuilder.ToString ();
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public String                       Name           { get => m_name          ; set => m_name           = value; }
      public int                          R              { get => m_r             ; set => m_r              = value; }
      public int                          G              { get => m_g             ; set => m_g              = value; }
      public int                          B              { get => m_b             ; set => m_b              = value; }
      public int                          A              { get => m_a             ; set => m_a              = value; }
      public int                          Location       { get => m_location      ; set => m_location       = value; }
      public List<MarkerItem>             ListOutgoing   { get => m_listOutgoing  ; set => m_listOutgoing   = value; }
      public List<ProvinceDefinitionItem> ListProvince   { get => m_listProvinces ; set => m_listProvinces  = value; }
      public Color                        Color          { get => m_color         ; set => m_color          = value; }
      public String                       Outgoing       { get => m_outgoing      ; set => m_outgoing       = value; }
   }
}

