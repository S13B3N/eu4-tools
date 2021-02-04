using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Common
{
   public class IndexNumberUtil
   {
      private int m_number;

      //------------------------------------------------------------------------

      public IndexNumberUtil ()
      {
         m_number = 1;
      }

      public IndexNumberUtil ( int number )
      {
         m_number = number;
      }

      //------------------------------------------------------------------------

      public virtual int Next ()
      {
         int number = m_number;

         m_number++;

         return number;
      }


   }

   public class IndexNumberStringUtil : IndexNumberUtil
   {
      public new string Next ()
      {
         return  base.Next ().ToString ();
      }
   }
}
