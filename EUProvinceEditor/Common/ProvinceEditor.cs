using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EUProvinceEditor.Common
{
   public delegate void DInitFinished ();
   public delegate void DInitError    ();

   public class ProvinceEditor
   {
      public event DInitFinished OnInitFinished;
      public event DInitError    OnInitError   ;

      private List<MarkerItem> m_listMarkerItem = new List<MarkerItem> ();

      private List<ProvinceDefinitionItem> m_listProvinceDefinition = new List<ProvinceDefinitionItem> ();

      private Bitmap m_bitmapMap             ;
      private Bitmap m_bitmapMapOverlay      ;
      private Bitmap m_bitmapMouseCursor     ;

      private Encoding m_encoding1252;

      private ProvinceDefinitionItem[,] m_mapProvinceDefinitionItem;

      //------------------------------------------------------------------------

      public void Init ()
      {
         DateTime start = DateTime.Now;

         try
         {
            m_encoding1252 = Encoding.GetEncoding ( 1252 );

            String pathOfMouseCursor = String.Format ( @"{0}\Gfx\{1}", Application.StartupPath, "cursor.png" );

            m_bitmapMouseCursor = new Bitmap ( Bitmap.FromFile ( pathOfMouseCursor ) );

            m_bitmapMouseCursor.MakeTransparent ( Color.White );

            String pathOfMarker = String.Format ( @"{0}\{1}", Application.StartupPath, "marker.eu4tradenode" );

            ReadProvinceDefinition ();

            ReadMarker ( pathOfMarker );

            ReadPixel ();

            MarkProvinces ();
            MarkLocation  ();

            //------------------------------------------------------------------

            OnInitFinished?.Invoke ();
         }
         catch ( Exception )
         {
            OnInitError?.Invoke ();
         }

         DateTime end = DateTime.Now;

         TimeSpan diff = end - start;

         Console.WriteLine ( "init time... " + diff.TotalSeconds );
      }

      public void ReadMarker ( String pathOfMarkerFile )
      {
         m_listMarkerItem.Clear ();

         if ( File.Exists ( pathOfMarkerFile ))
         {
            Dictionary<int, int> dictDuplicateIdProvince = new Dictionary<int, int> ();

            String[] listMarker = File.ReadAllLines ( pathOfMarkerFile, m_encoding1252 );

            foreach ( String marker in listMarker )
            {
               if ( !marker.Contains ( '#' ) )
               {
                  String[] listSplittedMarker = marker.Split ( '|' );

                  if ( listSplittedMarker.Length == 5 )
                  {
                     int r, g, b;

                     String name;

                     name = listSplittedMarker[0];

                     int.TryParse ( listSplittedMarker[1], out r );
                     int.TryParse ( listSplittedMarker[2], out g );
                     int.TryParse ( listSplittedMarker[3], out b );

                     String[] listIdProvince = listSplittedMarker[4].Split ( ',' );

                     MarkerItem markerItem = m_listMarkerItem.Find ( searchItem => (( searchItem.R == r ) && ( searchItem.G == g ) && ( searchItem.B == b )));

                     if ( markerItem == null )
                     {
                        markerItem = new MarkerItem ( name, r, g, b, 255, 0, "" );

                        foreach ( String stringIdProvince in listIdProvince )
                        {
                           int idProvince = -1;

                           int.TryParse ( stringIdProvince.Trim (), out idProvince );

                           if ( ( idProvince > 0 ) && !dictDuplicateIdProvince.ContainsKey ( idProvince ) )
                           {
                              ProvinceDefinitionItem provinceDefinitionItem = m_listProvinceDefinition.Find ( findItem => findItem.IdProvince == idProvince );

                              if ( provinceDefinitionItem != null )
                              {
                                 dictDuplicateIdProvince.Add ( idProvince, idProvince );

                                 markerItem.ListProvince.Add ( provinceDefinitionItem );
                              }
                           }
                        }

                        m_listMarkerItem.Add ( markerItem );
                     }
                  }
                  else if ( listSplittedMarker.Length == 7 )
                  {
                     int r = 0, g = 0, b = 0, location = 0;

                     String name;

                     name = listSplittedMarker[0];

                     int.TryParse ( listSplittedMarker[1], out r );
                     int.TryParse ( listSplittedMarker[2], out g );
                     int.TryParse ( listSplittedMarker[3], out b );

                     int.TryParse ( listSplittedMarker[4], out location );

                     String[] listIdProvince = listSplittedMarker[6].Split ( ',' );

                     MarkerItem markerItem = m_listMarkerItem.Find ( searchItem => (( searchItem.R == r ) && ( searchItem.G == g ) && ( searchItem.B == b )));

                     if ( markerItem == null )
                     {
                        markerItem = new MarkerItem ( name, r, g, b, 255, location, listSplittedMarker[5] );

                        foreach ( String stringIdProvince in listIdProvince )
                        {
                           int idProvince = -1;

                           int.TryParse ( stringIdProvince.Trim (), out idProvince );

                           if ( ( idProvince > 0 ) && !dictDuplicateIdProvince.ContainsKey ( idProvince ) )
                           {
                              ProvinceDefinitionItem provinceDefinitionItem = m_listProvinceDefinition.Find ( findItem => findItem.IdProvince == idProvince );

                              if ( provinceDefinitionItem != null )
                              {
                                 dictDuplicateIdProvince.Add ( idProvince, idProvince );

                                 markerItem.ListProvince.Add ( provinceDefinitionItem );
                              }
                           }
                        }

                        m_listMarkerItem.Add ( markerItem );
                     }
                     else
                     {
                        int k = 4711;
                     }
                  }
                  else
                  {
                     int k = 4711;
                  }
               }
            }

            //------------------------------------------------------------------

            foreach ( MarkerItem markerItem in m_listMarkerItem )
            {
               String[] listOutgoing = markerItem.Outgoing.Split ( ',' );

               foreach ( String outgoing in listOutgoing )
               {
                  if ( outgoing.Length > 0 )
                  {
                     markerItem.ListOutgoing.AddRange ( m_listMarkerItem.FindAll ( findItem => findItem.Name.Equals ( outgoing )));
                  }
               }
            }
         }
      }

      private void ReadProvinceDefinition ()
      {
         m_listProvinceDefinition.Clear ();

         String pathOfProvinceDefinition = String.Format ( @"{0}\{1}", Application.StartupPath, "definition.csv" );

         if ( File.Exists ( pathOfProvinceDefinition ) )
         {
            String[] listDefinition = File.ReadAllLines ( pathOfProvinceDefinition, m_encoding1252 );

            foreach ( String definition in listDefinition )
            {
               String[] listSplittedDefinition = definition.Split ( ';' );

               if ( listSplittedDefinition.Length >= 5 )
               {
                  int idProvince, r, g, b;

                  String nameOf, x;

                  int.TryParse ( listSplittedDefinition[0], out idProvince );
                  int.TryParse ( listSplittedDefinition[1], out r );
                  int.TryParse ( listSplittedDefinition[2], out g );
                  int.TryParse ( listSplittedDefinition[3], out b );

                  nameOf = listSplittedDefinition[4];
                  x = "";

                  ProvinceDefinitionItem provinceDefinitionItem = m_listProvinceDefinition.Find ( searchItem => (( searchItem.R == r ) && ( searchItem.G == g ) && ( searchItem.B == b )));

                  if ( provinceDefinitionItem == null )
                  {
                     m_listProvinceDefinition.Add ( new ProvinceDefinitionItem ( idProvince, r, g, b, nameOf, x ) );
                  }
                  else
                  {
                     int k = 4711;
                  }
               }
               else
               {
                  int k = 4711;
               }
            }
         }
      }

      private void ReadPixel ()
      {
         String pathOfBitmapMap = String.Format ( @"{0}\{1}", Application.StartupPath, @"Gfx\provinces.bmp" );

         if ( File.Exists ( pathOfBitmapMap ) )
         {
            int idProvince = GetMaxIdProvince ();

            //------------------------------------------------------------------

            StringBuilder colorKey = new StringBuilder ();

            Dictionary<String, ProvinceDefinitionItem> dictCache = new Dictionary<string, ProvinceDefinitionItem> ();

            foreach ( ProvinceDefinitionItem provinceDefinitionItemCache in m_listProvinceDefinition )
            {
               colorKey.Clear ();

               colorKey.Append ( provinceDefinitionItemCache.R );
               colorKey.Append ( provinceDefinitionItemCache.G );
               colorKey.Append ( provinceDefinitionItemCache.B );

               dictCache.Add ( colorKey.ToString (), provinceDefinitionItemCache );
            }

            //------------------------------------------------------------------

            String colorKeyString = "";

            Color pixelColor;

            m_bitmapMap = new Bitmap ( Bitmap.FromFile ( pathOfBitmapMap ) );

            m_mapProvinceDefinitionItem = new ProvinceDefinitionItem[m_bitmapMap.Width, m_bitmapMap.Height];

            Rectangle rectangle = new Rectangle ( 0, 0, m_bitmapMap.Width, m_bitmapMap.Height );

            System.Drawing.Imaging.BitmapData bitmapData = m_bitmapMap.LockBits ( rectangle, System.Drawing.Imaging.ImageLockMode.ReadOnly, m_bitmapMap.PixelFormat );

            int length = bitmapData.Stride * m_bitmapMap.Height;

            byte[] rawBytes = new byte[length];

            System.Runtime.InteropServices.Marshal.Copy ( bitmapData.Scan0, rawBytes, 0, length );

            m_bitmapMap.UnlockBits ( bitmapData );

            ProvinceDefinitionItem provinceDefinitionItem = new ProvinceDefinitionItem ();

            for ( int nXndex = 0; nXndex < m_bitmapMap.Width; nXndex++ )
            {
               for ( int nYndex = 0; nYndex < m_bitmapMap.Height; nYndex++ )
               {
                  pixelColor = Color.FromArgb ( rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 3], rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 2], rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 1], rawBytes[nYndex * bitmapData.Stride + nXndex * 4] );

                  if ( !( ( provinceDefinitionItem.R == pixelColor.R ) && ( provinceDefinitionItem.G == pixelColor.G ) && ( provinceDefinitionItem.B == pixelColor.B ) ) )
                  {
                     colorKey.Clear ();

                     colorKey.Append ( pixelColor.R );
                     colorKey.Append ( pixelColor.G );
                     colorKey.Append ( pixelColor.B );

                     colorKeyString = colorKey.ToString ();

                     if ( dictCache.ContainsKey ( colorKeyString ) )
                     {
                        provinceDefinitionItem = dictCache[colorKeyString];
                     }
                     else
                     {
                        String name = String.Format ( "NewProvince_{0}", idProvince );

                        provinceDefinitionItem = new ProvinceDefinitionItem ( ++idProvince, pixelColor.R, pixelColor.G, pixelColor.B, name, "" );
                     }
                  }

                  provinceDefinitionItem.ListPixel.Add ( new Pixel ( nXndex, nYndex ) );

                  m_mapProvinceDefinitionItem[nXndex, nYndex] = provinceDefinitionItem;
               }
            }

            m_bitmapMapOverlay = new Bitmap ( m_bitmapMap );
         }
      }

      //------------------------------------------------------------------------

      public void MarkProvinces ()
      {
         m_bitmapMapOverlay = new Bitmap ( m_bitmapMap );

         foreach ( MarkerItem markerItem in m_listMarkerItem )
         {
            foreach ( ProvinceDefinitionItem provinceDefinitionItem in markerItem.ListProvince )
            {
               foreach ( Pixel pixel in provinceDefinitionItem.ListPixel )
               {
                  m_bitmapMapOverlay.SetPixel ( pixel.XPos, pixel.YPos, markerItem.Color );
               }
            }
         }
      }

      public void MarkLocation ()
      {
         foreach ( MarkerItem markerItem in m_listMarkerItem )
         {
            ProvinceDefinitionItem provinceDefinitionItem = markerItem.ListProvince.Find ( findItem => findItem.IdProvince == markerItem.Location );

            if ( provinceDefinitionItem != null )
            {
               DrawCheckeredListPixel ( provinceDefinitionItem, Color.Black );
            }
         }
      }

      public void MarkOutgoing ( MarkerItem markerItem )
      {
         foreach ( MarkerItem outgoingMarkerItem in markerItem.ListOutgoing )
         {
            foreach ( ProvinceDefinitionItem provinceDefinitionItem in outgoingMarkerItem.ListProvince )
            {
               if ( outgoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
               {
                  DrawHorizontalListPixel ( provinceDefinitionItem, Color.White );
               }
            }
         }

         foreach ( MarkerItem ingoingMarkerItem in m_listMarkerItem )
         {
            if ( ingoingMarkerItem.ListOutgoing.Contains ( markerItem ))
            {
               foreach ( ProvinceDefinitionItem provinceDefinitionItem in ingoingMarkerItem.ListProvince )
               {
                  if ( ingoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
                  {
                     DrawVerticalListPixel ( provinceDefinitionItem, Color.White );
                  }
               }
            }
         }
      }

      //------------------------------------------------------------------------

      public void Mark ( MarkerItem markerItem, int xPos, int yPos )
      {
         if ( xPos <= m_mapProvinceDefinitionItem.GetLength ( 0 ) && yPos <= m_mapProvinceDefinitionItem.GetLength ( 1 ) )
         {
            ProvinceDefinitionItem provinceDefinitionItem = m_mapProvinceDefinitionItem[xPos,yPos];

            Color drawColor;

            if ( markerItem.ListProvince.Contains ( provinceDefinitionItem ) )
            {
               markerItem.ListProvince.Remove ( provinceDefinitionItem );

               drawColor = Color.FromArgb ( 0, 0, 0, 0 );
            }
            else
            {
               foreach ( MarkerItem clearMarkerItem in m_listMarkerItem )
               {
                  clearMarkerItem.ListProvince.Remove ( provinceDefinitionItem );
               }

               markerItem.ListProvince.Add ( provinceDefinitionItem );

               drawColor = markerItem.Color;
            }

            //------------------------------------------------------------------

            foreach ( Pixel pixel in provinceDefinitionItem.ListPixel )
            {
               m_bitmapMapOverlay.SetPixel ( pixel.XPos, pixel.YPos, drawColor );
            }
         }
         else
         {
            int k = 4711;
         }
      }

      public void MarkLocation ( MarkerItem markerItem, int xPos, int yPos )
      {
         if ( xPos <= m_mapProvinceDefinitionItem.GetLength ( 0 ) && yPos <= m_mapProvinceDefinitionItem.GetLength ( 1 ))
         {
            ProvinceDefinitionItem provinceDefinitionItem = m_mapProvinceDefinitionItem[xPos,yPos];

            if ( markerItem.ListProvince.Contains ( provinceDefinitionItem ) )
            {
               if ( markerItem.Location != provinceDefinitionItem.IdProvince )
               {
                  ProvinceDefinitionItem oldProvinceDefinitionItem = markerItem.ListProvince.Find ( findItem => ( findItem.IdProvince == markerItem.Location ));

                  if ( oldProvinceDefinitionItem != null )
                  {
                     DrawCheckeredListPixel ( oldProvinceDefinitionItem, markerItem.Color );
                  }

                  markerItem.Location = provinceDefinitionItem.IdProvince;

                  DrawCheckeredListPixel ( provinceDefinitionItem, Color.Black );
               }
               else
               {
                  // nothing todo
               }
            }
         }
      }

      public void MarkOutgoing ( MarkerItem markerItem, int xPos, int yPos )
      {
         if ( xPos <= m_mapProvinceDefinitionItem.GetLength ( 0 ) && yPos <= m_mapProvinceDefinitionItem.GetLength ( 1 ))
         {
            ProvinceDefinitionItem markedProvinceDefinitionItem = m_mapProvinceDefinitionItem[xPos, yPos];

            MarkerItem outgoingMarkterItem = m_listMarkerItem.Find ( findItem => findItem.ListProvince.Contains ( markedProvinceDefinitionItem ));

            if (( outgoingMarkterItem != null ) && ( outgoingMarkterItem != markerItem ) && !outgoingMarkterItem.ListOutgoing.Contains ( markerItem ))
            {
               if ( markerItem.ListOutgoing.Contains ( outgoingMarkterItem ))
               {
                  markerItem.ListOutgoing.Remove ( outgoingMarkterItem );

                  foreach ( ProvinceDefinitionItem provinceDefinitionItem in outgoingMarkterItem.ListProvince )
                  {
                     if ( outgoingMarkterItem.Location != provinceDefinitionItem.IdProvince )
                     {
                        DrawHorizontalListPixel ( provinceDefinitionItem, outgoingMarkterItem.Color );
                     }
                  }
               }
               else
               {
                  markerItem.ListOutgoing.Add ( outgoingMarkterItem );

                  foreach ( ProvinceDefinitionItem provinceDefinitionItem in outgoingMarkterItem.ListProvince )
                  {
                     if ( outgoingMarkterItem.Location != provinceDefinitionItem.IdProvince )
                     {
                        DrawHorizontalListPixel ( provinceDefinitionItem, Color.White );
                     }
                  }
               }
            }
            else
            {
               // nothing todo
            }
         }
      }

      public void Clear ()
      {
         m_bitmapMapOverlay = new Bitmap ( m_bitmapMap );

         foreach ( MarkerItem markerItem in m_listMarkerItem )
         {
            markerItem.ListProvince.Clear ();
         }
      }

      private Bitmap ResizeImage ( Bitmap originalBitmap )
      {
         float zoom = 1.0f;

         int width  = ( int )( originalBitmap.Width  * zoom );
         int height = ( int )( originalBitmap.Height * zoom );

         var resizedRect = new Rectangle ( 0, 0, width, height );

         var resizedImage = new Bitmap ( width, height );

         resizedImage.SetResolution ( originalBitmap.HorizontalResolution, originalBitmap.VerticalResolution );

         using ( var graphics = Graphics.FromImage ( resizedImage ) )
         {
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.PixelOffsetMode = PixelOffsetMode.None;

            using ( var wrapMode = new ImageAttributes () )
            {
               wrapMode.SetWrapMode ( WrapMode.TileFlipXY );

               graphics.DrawImage ( originalBitmap, resizedRect, 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, wrapMode );
            }
         }

         return resizedImage;
      }

      public void ChangeMarker ( MarkerItem oldMarkerItem, MarkerItem markerItem )
      {
         if ( oldMarkerItem != null )
         {
            foreach ( MarkerItem outgoingMarkerItem in oldMarkerItem.ListOutgoing )
            {
               foreach ( ProvinceDefinitionItem provinceDefinitionItem in outgoingMarkerItem.ListProvince )
               {
                  if ( outgoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
                  {
                     DrawHorizontalListPixel ( provinceDefinitionItem, outgoingMarkerItem.Color );
                  }
               }
            }

            foreach ( MarkerItem ingoingMarkerItem in m_listMarkerItem )
            {
               if ( ingoingMarkerItem.ListOutgoing.Contains ( oldMarkerItem ))
               {
                  foreach ( ProvinceDefinitionItem provinceDefinitionItem in ingoingMarkerItem.ListProvince )
                  {
                     if ( ingoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
                     {
                        DrawVerticalListPixel ( provinceDefinitionItem, ingoingMarkerItem.Color );
                     }
                  }
               }
            }
         }

         //---------------------------------------------------------------------

         foreach ( MarkerItem outgoingMarkerItem in markerItem.ListOutgoing )
         {
            foreach ( ProvinceDefinitionItem provinceDefinitionItem in outgoingMarkerItem.ListProvince )
            {
               if ( outgoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
               {
                  DrawHorizontalListPixel ( provinceDefinitionItem, Color.White );
               }
            }
         }

         foreach ( MarkerItem ingoingMarkerItem in m_listMarkerItem )
         {
            if ( ingoingMarkerItem.ListOutgoing.Contains ( markerItem ))
            {
               foreach ( ProvinceDefinitionItem provinceDefinitionItem in ingoingMarkerItem.ListProvince )
               {
                  if ( ingoingMarkerItem.Location != provinceDefinitionItem.IdProvince )
                  {
                     DrawVerticalListPixel ( provinceDefinitionItem, Color.White );
                  }
               }
            }
         }
      }

      //------------------------------------------------------------------------

      public void SaveMarker ( String pathOfMarkerFile )
      {
         List<String> listMarkerString = new List<String>();

         StringBuilder idProvinceBuilder = new StringBuilder ( 1000 );
         StringBuilder outgoingBuilder   = new StringBuilder ( 1000 );

         foreach ( MarkerItem markerItem in m_listMarkerItem )
         {
            idProvinceBuilder.Clear ();
            outgoingBuilder.Clear ();

            bool bComma = false;

            foreach ( MarkerItem outgoingMarkerItem in markerItem.ListOutgoing )
            {
               if ( bComma )
               {
                  outgoingBuilder.Append ( "," );
               }

               outgoingBuilder.Append ( outgoingMarkerItem.Name );

               bComma = true;
            }

            bComma = false;

            foreach ( ProvinceDefinitionItem provinceDefinitionItem in markerItem.ListProvince )
            {
               if ( bComma )
               {
                  idProvinceBuilder.Append ( "," );
               }

               idProvinceBuilder.Append ( provinceDefinitionItem.IdProvince );

               bComma = true;
            }

            listMarkerString.Add ( String.Format ( "{0}|{1}|{2}|{3}|{4}|{5}|{6}", markerItem.Name, markerItem.R, markerItem.G, markerItem.B, markerItem.Location, outgoingBuilder.ToString (), idProvinceBuilder.ToString () ) );
         }

         File.WriteAllLines ( pathOfMarkerFile, listMarkerString, m_encoding1252 );
      }

      public void SaveMap ( String pathOfBitmap )
      {
         int width  = m_bitmapMap.Width ;
         int height = m_bitmapMap.Height;

         Bitmap saveBitmap = new Bitmap ( width, height );

         using ( var graphics = Graphics.FromImage ( saveBitmap ) )
         {
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            using ( var wrapMode = new ImageAttributes () )
            {
               wrapMode.SetWrapMode ( WrapMode.TileFlipXY );

               Rectangle rectangle = new Rectangle ( 0, 0, width, height );

               graphics.DrawImage ( m_bitmapMap, rectangle, 0, 0, width, height, GraphicsUnit.Pixel, wrapMode );
               graphics.DrawImage ( m_bitmapMapOverlay, rectangle, 0, 0, width, height, GraphicsUnit.Pixel, wrapMode );
            }
         }

         try
         {
            saveBitmap.Save ( pathOfBitmap );
         }
         catch ( Exception )
         {
            int k = 4711;
         }
      }

      public void Export ( String pathOfExportFile )
      {
         StringBuilder template = new StringBuilder ( 1000 );

         foreach ( MarkerItem markerItem in m_listMarkerItem )
         {
            String color = markerItem.R + " " + markerItem.G + " " + markerItem.B;

            template.Append ( markerItem.Name + "={"               + Environment.NewLine );
            template.Append ( "   location=" + markerItem.Location + Environment.NewLine );
            template.Append ( ""                                   + Environment.NewLine );
            template.Append ( "   color={"                         + Environment.NewLine );
            template.Append ( "      " + color                     + Environment.NewLine );
            template.Append ( "   }"                               + Environment.NewLine );
            template.Append ( ""                                   + Environment.NewLine );
            template.Append ( "   members={"                       + Environment.NewLine );
            template.Append ( "      " + markerItem.ListMembers () + Environment.NewLine );
            template.Append ( "   }"                               + Environment.NewLine );

            foreach ( MarkerItem outgoingMarkerItem in markerItem.ListOutgoing )
            {
               template.Append ( ""                                               + Environment.NewLine );
               template.Append ( "   outgoing={"                                  + Environment.NewLine );
               template.Append ( "      name=\"" + outgoingMarkerItem.Name + "\"" + Environment.NewLine );
               template.Append ( ""                                               + Environment.NewLine );
               template.Append ( "      path={"                                   + Environment.NewLine );
               template.Append ( "      }"                                        + Environment.NewLine );
               template.Append ( ""                                               + Environment.NewLine );
               template.Append ( "      control={"                                + Environment.NewLine );
               template.Append ( "      }"                                        + Environment.NewLine );
               template.Append ( "   }"                                           + Environment.NewLine );
            }

            if ( markerItem.ListOutgoing.Count == 0 )
            {
               template.Append ( ""           + Environment.NewLine );
               template.Append ( "   end=yes" + Environment.NewLine );
            }

            template.Append ( "}" + Environment.NewLine + Environment.NewLine );
         }

         File.WriteAllText ( pathOfExportFile, template.ToString ());
      }

      //------------------------------------------------------------------------

      public Bitmap ZoomImage ( int xPos, int yPos, int width, int height )
      {
         int halfWidth  = width  / 2;
         int halfHeight = height / 2;

         if ( xPos < halfWidth )
         {
            xPos = 0;
         }
         //else if ( xPos > ( m_bitmapMap.Width - halfWidth ))
         //{
         //   xPos = ( m_bitmapMap.Width - halfWidth );
         //}
         else
         {
            xPos -= halfWidth;
         }

         if ( yPos < halfHeight )
         {
            yPos = 0;
         }
         //else if ( xPos > ( m_bitmapMap.Height - halfHeight ))
         //{
         //   xPos = ( m_bitmapMap.Height - halfHeight );
         //}
         else
         {
            yPos -= halfHeight;
         }

         var resizedRect = new Rectangle ( 0, 0, width, height );

         var resizedImage = new Bitmap ( width, height );

         using ( var graphics = Graphics.FromImage ( resizedImage ))
         {
            graphics.CompositingMode    = CompositingMode   .SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighSpeed ;
            graphics.InterpolationMode  = InterpolationMode .Low       ;
            graphics.SmoothingMode      = SmoothingMode     .HighSpeed ;
            graphics.PixelOffsetMode    = PixelOffsetMode   .HighSpeed ;

            using ( var wrapMode = new ImageAttributes () )
            {
               wrapMode.SetWrapMode ( WrapMode.TileFlipXY );

               graphics.DrawImage ( m_bitmapMap        , resizedRect, xPos, yPos, width, height, GraphicsUnit.Pixel, wrapMode );
               graphics.DrawImage ( m_bitmapMapOverlay , resizedRect, xPos, yPos, width, height, GraphicsUnit.Pixel, wrapMode );

               graphics.DrawImage ( m_bitmapMouseCursor, halfWidth, halfHeight, m_bitmapMouseCursor.Width, m_bitmapMouseCursor.Height );
            }
         }

         return resizedImage;
      }

      //------------------------------------------------------------------------
      // Helper
      //------------------------------------------------------------------------

      private void DrawCheckeredListPixel ( ProvinceDefinitionItem provinceDefinitionItem, Color drawColor )
      {
         foreach ( Pixel pixel in provinceDefinitionItem.ListPixel )
         {
            int posX = pixel.XPos % 3;
            int posY = pixel.YPos % 3;

            if (( posX == 1 ) && ( posY == 1 ))
            {
               m_bitmapMapOverlay.SetPixel ( pixel.XPos, pixel.YPos, drawColor );
            }
         }
      }

      private void DrawHorizontalListPixel ( ProvinceDefinitionItem provinceDefinitionItem, Color drawColor )
      {
         foreach ( Pixel pixel in provinceDefinitionItem.ListPixel )
         {
            int posY = pixel.YPos % 4;

            if ( posY == 1 )
            {
               m_bitmapMapOverlay.SetPixel ( pixel.XPos, pixel.YPos, drawColor );
            }
         }
      }

      private void DrawVerticalListPixel ( ProvinceDefinitionItem provinceDefinitionItem, Color drawColor )
      {
         foreach ( Pixel pixel in provinceDefinitionItem.ListPixel )
         {
            int posX = pixel.XPos % 4;

            if ( posX == 1 )
            {
               m_bitmapMapOverlay.SetPixel ( pixel.XPos, pixel.YPos, drawColor );
            }
         }
      }

      private int GetMaxIdProvince ()
      {
         int idProvince = 0;

         foreach ( ProvinceDefinitionItem provinceDefinitionItem in m_listProvinceDefinition )
         {
            if ( idProvince < provinceDefinitionItem.IdProvince )
            {
               idProvince = provinceDefinitionItem.IdProvince;
            }
         }

         return idProvince;
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public List<MarkerItem>             ListMarkerItem         { get => m_listMarkerItem        ; set => m_listMarkerItem         = value; }
      public List<ProvinceDefinitionItem> ListProvinceDefinition { get => m_listProvinceDefinition; set => m_listProvinceDefinition = value; }
      public Bitmap                       BitmapMapOverlay       { get => m_bitmapMapOverlay      ; set => m_bitmapMapOverlay       = value; }
      public Bitmap                       BitmapMap              { get => m_bitmapMap             ; set => m_bitmapMap              = value; }
   }
}
