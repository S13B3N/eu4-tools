using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderCreator.Common
{
   public class ListViewUtil : IDisposable
   {
      private ListView m_listView;

      private List<ListViewItem> m_listListViewItem = new List<ListViewItem> ();

      //------------------------------------------------------------------------

      public ListViewUtil ( ListView listView )
      {
         m_listView = listView;
      }

      //------------------------------------------------------------------------

      public void Add ( string[] listOfText, Object tag = null )
      {
         ListViewItem listViewItem = null;

         int nIndex = 0;

         foreach ( string text in listOfText )
         {
            if ( nIndex++ == 0 )
            {
               listViewItem = new ListViewItem ( text );

               listViewItem.Tag = tag;
            }
            else
            {
               listViewItem.SubItems.Add ( text );
            }
         }

         if ( listViewItem != null )
         {
            m_listListViewItem.Add ( listViewItem );
         }
      }

      //------------------------------------------------------------------------

      public void Dispose ()
      {
         m_listView.Items.Clear ();

         m_listView.Items.AddRange ( m_listListViewItem.ToArray ());

         m_listListViewItem.Clear ();
      }
   }
}
