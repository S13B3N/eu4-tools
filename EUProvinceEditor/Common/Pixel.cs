using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUProvinceEditor.Common
{
   public class Pixel
   {
      private int m_xPos;
      private int m_yPos;
      //private int m_r   ;
      //private int m_g   ;
      //private int m_b   ;
      //private int m_a   ;

      //------------------------------------------------------------------------

      public Pixel ()
      {
         m_xPos = 0;
         m_yPos = 0;
         //m_r    = 0;
         //m_g    = 0;
         //m_b    = 0;
         //m_a    = 0;
      }

      public Pixel ( int xPos, int yPos )
      {
         m_xPos = xPos;
         m_yPos = yPos;
         //m_r    = 0   ;
         //m_g    = 0   ;
         //m_b    = 0   ;
         //m_a    = 0   ;
      }

      //public Pixel ( int xPos, int yPos, int r, int g, int b, int a )
      //{
      //   m_xPos = xPos;
      //   m_yPos = yPos;
      //   m_r    = r   ;
      //   m_g    = g   ;
      //   m_b    = b   ;
      //   m_a    = a   ;
      //}

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public int XPos { get => m_xPos; set => m_xPos = value; }
      public int YPos { get => m_yPos; set => m_yPos = value; }
      //public int R    { get => m_r   ; set => m_r    = value; }
      //public int G    { get => m_g   ; set => m_g    = value; }
      //public int B    { get => m_b   ; set => m_b    = value; }
      //public int A    { get => m_a   ; set => m_a    = value; }
   }
}
