using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUProvinceEditor.Common
{
   [Serializable()]
   public class ComboBoxItem<T>
   {
      protected T      m_value;
      protected String m_label;

      //------------------------------------------------------------------------

      public ComboBoxItem ()
      {
      }

      public ComboBoxItem ( T value, String label )
      {
         m_value = value;
         m_label = label;
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public T      Value { get { return m_value; } set { m_value = value; }}
      public String Label { get { return m_label; } set { m_label = value; }}
   }
}
