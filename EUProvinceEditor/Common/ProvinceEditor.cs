using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EUProvinceEditor.Common
{
    public class ProvinceEditor
    {
        public event DInitFinished OnInitFinished;
        public event DInitError OnInitError;

        private Bitmap m_bitmapMouseCursor;

        private Encoding m_encoding1252;

        private ProvinceDefinitionItem[,] m_mapProvinceDefinitionItem;

        public void Init()
        {
            DateTime start = DateTime.Now;
            
            try
            {
                m_encoding1252 = Encoding.GetEncoding(1252);

                string pathOfMouseCursor = $@"{Application.StartupPath}\Gfx\{"cursor.png"}";

                m_bitmapMouseCursor = new Bitmap(Image.FromFile(pathOfMouseCursor));

                m_bitmapMouseCursor.MakeTransparent(Color.White);

                string pathOfMarker = $@"{Application.StartupPath}\{"marker.eu4tradenode"}";

                ReadProvinceDefinition();

                ReadMarker(pathOfMarker);

                ReadPixel();

                MarkProvinces();
                MarkLocation();

                OnInitFinished?.Invoke();
            }
            catch
            {
                OnInitError?.Invoke();
            }

            DateTime end = DateTime.Now;
            TimeSpan diff = end - start;

            Console.WriteLine("init time... " + diff.TotalSeconds);
        }

        public void ReadMarker(string pathOfMarkerFile)
        {
            MarkerItems.Clear();

            if (!File.Exists(pathOfMarkerFile)) return;
            var dictDuplicateIdProvince = new Dictionary<int, int>();

            string[] markers = File.ReadAllLines(pathOfMarkerFile, m_encoding1252);

            foreach (string marker in markers)
            {
                if (marker.Contains('#')) continue;
                    
                string[] listSplittedMarker = marker.Split('|');

                switch (listSplittedMarker.Length)
                {
                    case 5:
                    {
                        var name = listSplittedMarker[0];

                        int.TryParse(listSplittedMarker[1], out var r);
                        int.TryParse(listSplittedMarker[2], out var g);
                        int.TryParse(listSplittedMarker[3], out var b);

                        string[] listIdProvince = listSplittedMarker[4].Split(',');

                        var markerItem = MarkerItems.Find(searchItem => searchItem.R == r && searchItem.G == g && searchItem.B == b);

                        if (markerItem == null)
                        {
                            markerItem = new MarkerItem(name, r, g, b, 255, 0, "");

                            foreach (string stringIdProvince in listIdProvince)
                            {
                                int.TryParse(stringIdProvince.Trim(), out var idProvince);

                                if (idProvince <= 0 || dictDuplicateIdProvince.ContainsKey(idProvince)) continue;
                                    
                                var provinceDefinitionItem = Provinces.Find(findItem => findItem.Id == idProvince);

                                if (provinceDefinitionItem == null) continue;
                                dictDuplicateIdProvince.Add(idProvince, idProvince);

                                markerItem.Provinces.Add(provinceDefinitionItem);
                            }

                            MarkerItems.Add(markerItem);
                        }

                        break;
                    }
                    case 7:
                    {
                        var name = listSplittedMarker[0];

                        int.TryParse(listSplittedMarker[1], out int r);
                        int.TryParse(listSplittedMarker[2], out int g);
                        int.TryParse(listSplittedMarker[3], out int b);

                        int.TryParse(listSplittedMarker[4], out int location);

                        string[] listIdProvince = listSplittedMarker[6].Split(',');

                        var markerItem = MarkerItems.Find(searchItem => searchItem.R == r && searchItem.G == g && searchItem.B == b);

                        if (markerItem != null) continue;

                        markerItem = new MarkerItem(name, r, g, b, 255, location, listSplittedMarker[5]);

                        var provinceIds = listIdProvince.Select(strId =>
                        {
                            int.TryParse(strId.Trim(), out var id);
                            return id;
                        }).Where(id => (id <= 0 || dictDuplicateIdProvince.ContainsKey(id)) == false).ToList();

                        var provinces = provinceIds.Select(id =>
                            Provinces.Find(province => province.Id == id));

                        foreach (var province in provinces)
                        {
                            dictDuplicateIdProvince.Add(province.Id, province.Id);
                            markerItem.Provinces.Add(province);
                        }

                        MarkerItems.Add(markerItem);
                        break;
                    }
                }
            }

            foreach (var markerItem in MarkerItems)
            {
                string[] listOutgoing = markerItem.Outgoing.Split(',');

                foreach (string outgoing in listOutgoing.Where(outgoing => outgoing.Length > 0))
                    markerItem.OutgoingItems.AddRange(MarkerItems.FindAll(findItem => findItem.Name.Equals(outgoing)));
            }
        }

        private void ReadProvinceDefinition()
        {
            Provinces.Clear();

            string pathOfProvinceDefinition = $@"{Application.StartupPath}\{"definition.csv"}";

            if (!File.Exists(pathOfProvinceDefinition)) return;
            string[] definitions = File.ReadAllLines(pathOfProvinceDefinition, m_encoding1252);

            foreach (string definition in definitions)
            {
                string[] listSplittedDefinition = definition.Split(';');

                if (listSplittedDefinition.Length < 5) continue;

                int.TryParse(listSplittedDefinition[0], out int idProvince);
                int.TryParse(listSplittedDefinition[1], out int r);
                int.TryParse(listSplittedDefinition[2], out int g);
                int.TryParse(listSplittedDefinition[3], out int b);

                string nameOf = listSplittedDefinition[4];

                var provinceDefinitionItem = Provinces.Find(searchItem => ((searchItem.R == r) && (searchItem.G == g) && (searchItem.B == b)));

                if (provinceDefinitionItem == null)
                    Provinces.Add(new ProvinceDefinitionItem(idProvince, r, g, b, nameOf, string.Empty));
            }
        }

        private void ReadPixel()
        {
            string pathOfBitmapMap = $@"{Application.StartupPath}\{@"Gfx\provinces.bmp"}";

            if (File.Exists(pathOfBitmapMap) == false) return;
            
            int idProvince = GetMaxIdProvince();

            var dictCache = Provinces.ToDictionary(p => $"{p.R}{p.G}{p.B}");

            BitmapMap = new Bitmap(Image.FromFile(pathOfBitmapMap));

            m_mapProvinceDefinitionItem = new ProvinceDefinitionItem[BitmapMap.Width, BitmapMap.Height];

            Rectangle rectangle = new Rectangle(0, 0, BitmapMap.Width, BitmapMap.Height);

            BitmapData bitmapData = BitmapMap.LockBits(rectangle, ImageLockMode.ReadOnly, BitmapMap.PixelFormat);

            int length = bitmapData.Stride * BitmapMap.Height;

            byte[] rawBytes = new byte[length];

            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, rawBytes, 0, length);

            BitmapMap.UnlockBits(bitmapData);

            var province = new ProvinceDefinitionItem();

            var colorKey = new StringBuilder();

            for (int nXndex = 0; nXndex < BitmapMap.Width; nXndex++)
            {
                for (int nYndex = 0; nYndex < BitmapMap.Height; nYndex++)
                {
                    var pixelColor = Color.FromArgb(rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 3], rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 2], rawBytes[nYndex * bitmapData.Stride + nXndex * 4 + 1], rawBytes[nYndex * bitmapData.Stride + nXndex * 4]);

                    if (!(province.R == pixelColor.R && province.G == pixelColor.G && province.B == pixelColor.B))
                    {
                        colorKey.Clear();

                        colorKey.Append(pixelColor.R);
                        colorKey.Append(pixelColor.G);
                        colorKey.Append(pixelColor.B);

                        var colorKeyString = colorKey.ToString();

                        province = dictCache.ContainsKey(colorKeyString) ? 
                            dictCache[colorKeyString] : 
                            new ProvinceDefinitionItem(++idProvince, pixelColor.R, pixelColor.G, pixelColor.B, $"NewProvince_{idProvince}", string.Empty);
                    }

                    province.Pixels.Add(new Pixel(nXndex, nYndex));

                    m_mapProvinceDefinitionItem[nXndex, nYndex] = province;
                }
            }

            BitmapMapOverlay = new Bitmap(BitmapMap);
        }

        public void MarkProvinces()
        {
            BitmapMapOverlay = new Bitmap(BitmapMap);

            foreach (var markerItem in MarkerItems)
                foreach (var pixel in markerItem.Provinces.SelectMany(province => province.Pixels))
                    BitmapMapOverlay.SetPixel(pixel.XPos, pixel.YPos, markerItem.Color);
        }

        public void MarkLocation()
        {
            var provinces = MarkerItems
                .Select(markerItem => markerItem.Provinces.Find(province => province.Id == markerItem.Location))
                .Where(province => province != null);
            
            foreach (var province in provinces)
                DrawCheckeredListPixel(province, Color.Black);
        }

        public void MarkOutgoing(MarkerItem markerItem)
        {
            var provinces = from outgoingMarkerItem in markerItem.OutgoingItems
                        from provinceDefinitionItem in outgoingMarkerItem.Provinces
                        where outgoingMarkerItem.Location != provinceDefinitionItem.Id
                        select provinceDefinitionItem;

            foreach (var province in provinces)
                DrawHorizontalListPixel(province, Color.White);

            foreach (var province in MarkerItems.Where(ingoingMarkerItem => ingoingMarkerItem.OutgoingItems.Contains(markerItem)).SelectMany(ingoingMarkerItem => ingoingMarkerItem.Provinces.Where(provinceDefinitionItem => ingoingMarkerItem.Location != provinceDefinitionItem.Id)))
                DrawVerticalListPixel(province, Color.White);
        }

        public void Mark(MarkerItem markerItem, int xPos, int yPos)
        {
            if (xPos > m_mapProvinceDefinitionItem.GetLength(0) ||
                yPos > m_mapProvinceDefinitionItem.GetLength(1)) return;
            var province = m_mapProvinceDefinitionItem[xPos, yPos];

            Color drawColor;

            if (markerItem.Provinces.Contains(province))
            {
                markerItem.Provinces.Remove(province);

                drawColor = Color.FromArgb(0, 0, 0, 0);
            }
            else
            {
                foreach (var clearMarkerItem in MarkerItems)
                    clearMarkerItem.Provinces.Remove(province);

                markerItem.Provinces.Add(province);

                drawColor = markerItem.Color;
            }

            foreach (var pixel in province.Pixels)
                BitmapMapOverlay.SetPixel(pixel.XPos, pixel.YPos, drawColor);
        }

        public void MarkLocation(MarkerItem markerItem, int xPos, int yPos)
        {
            if (xPos > m_mapProvinceDefinitionItem.GetLength(0) ||
                yPos > m_mapProvinceDefinitionItem.GetLength(1)) return;

            var provinceDefinitionItem = m_mapProvinceDefinitionItem[xPos, yPos];

            if (!markerItem.Provinces.Contains(provinceDefinitionItem)) return;
            if (markerItem.Location == provinceDefinitionItem.Id) return;
            
            var oldProvince = markerItem.Provinces.Find(findItem => findItem.Id == markerItem.Location);

            if (oldProvince != null)
                DrawCheckeredListPixel(oldProvince, markerItem.Color);

            markerItem.Location = provinceDefinitionItem.Id;

            DrawCheckeredListPixel(provinceDefinitionItem, Color.Black);
        }

        public void MarkOutgoing(MarkerItem markerItem, int xPos, int yPos)
        {
            if (xPos > m_mapProvinceDefinitionItem.GetLength(0) ||
                yPos > m_mapProvinceDefinitionItem.GetLength(1)) return;
            var markedProvince = m_mapProvinceDefinitionItem[xPos, yPos];

            var outgoingMarkerItem = MarkerItems.Find(mi => mi.Provinces.Contains(markedProvince));

            if (outgoingMarkerItem == null || outgoingMarkerItem == markerItem ||
                outgoingMarkerItem.OutgoingItems.Contains(markerItem)) return;
            
            if (markerItem.OutgoingItems.Contains(outgoingMarkerItem))
            {
                markerItem.OutgoingItems.Remove(outgoingMarkerItem);

                foreach (var provinceDefinitionItem in outgoingMarkerItem.Provinces.Where(provinceDefinitionItem => outgoingMarkerItem.Location != provinceDefinitionItem.Id))
                    DrawHorizontalListPixel(provinceDefinitionItem, outgoingMarkerItem.Color);
            }
            else
            {
                markerItem.OutgoingItems.Add(outgoingMarkerItem);

                foreach (var provinceDefinitionItem in outgoingMarkerItem.Provinces.Where(provinceDefinitionItem => outgoingMarkerItem.Location != provinceDefinitionItem.Id))
                    DrawHorizontalListPixel(provinceDefinitionItem, Color.White);
            }
        }

        public void Clear()
        {
            BitmapMapOverlay = new Bitmap(BitmapMap);

            foreach (var mi in MarkerItems)
                mi.Provinces.Clear();
        }

        public void ChangeMarker(MarkerItem oldMarkerItem, MarkerItem markerItem)
        {
            if (oldMarkerItem != null)
            {
                foreach (var outgoingMi in oldMarkerItem.OutgoingItems)
                    foreach (var province in outgoingMi.Provinces.Where(province => outgoingMi.Location != province.Id))
                        DrawHorizontalListPixel(province, outgoingMi.Color);

                foreach (var ingoingMi in MarkerItems.Where(mi => mi.OutgoingItems.Contains(oldMarkerItem)))
                    foreach (var province in ingoingMi.Provinces.Where(province => ingoingMi.Location != province.Id))
                        DrawVerticalListPixel(province, ingoingMi.Color);
            }

            foreach (var provinceDefinitionItem in
                markerItem.OutgoingItems.SelectMany(outgoingMarkerItem => outgoingMarkerItem.Provinces
                    .Where(provinceDefinitionItem => outgoingMarkerItem.Location != provinceDefinitionItem.Id)))
            {
                DrawHorizontalListPixel(provinceDefinitionItem, Color.White);
            }

            foreach (var provinceDefinitionItem in
                MarkerItems.Where(ingoingMarkerItem =>
                    ingoingMarkerItem.OutgoingItems.Contains(markerItem)).SelectMany(ingoingMarkerItem =>
                    ingoingMarkerItem.Provinces.Where(provinceDefinitionItem =>
                        ingoingMarkerItem.Location != provinceDefinitionItem.Id)))
            {
                DrawVerticalListPixel(provinceDefinitionItem, Color.White);
            }
        }

        public void SaveMarker(string pathOfMarkerFile)
        {
            var listMarkerString = new List<string>();

            var idProvinceBuilder = new StringBuilder(1000);
            var outgoingBuilder = new StringBuilder(1000);

            foreach (var markerItem in MarkerItems)
            {
                idProvinceBuilder.Clear();
                outgoingBuilder.Clear();

                bool bComma = false;

                foreach (var outgoingMarkerItem in markerItem.OutgoingItems)
                {
                    if (bComma)
                    {
                        outgoingBuilder.Append(",");
                    }

                    outgoingBuilder.Append(outgoingMarkerItem.Name);

                    bComma = true;
                }

                bComma = false;

                foreach (var province in markerItem.Provinces)
                {
                    if (bComma)
                    {
                        idProvinceBuilder.Append(",");
                    }

                    idProvinceBuilder.Append(province.Id);

                    bComma = true;
                }

                listMarkerString.Add($"{markerItem.Name}|{markerItem.R}|{markerItem.G}|{markerItem.B}|{markerItem.Location}|{outgoingBuilder}|{idProvinceBuilder}");
            }

            File.WriteAllLines(pathOfMarkerFile, listMarkerString, m_encoding1252);
        }

        public void SaveMap(string bitmapPath)
        {
            int width = BitmapMap.Width;
            int height = BitmapMap.Height;

            var saveBitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(saveBitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                    var rectangle = new Rectangle(0, 0, width, height);
                    graphics.DrawImage(BitmapMap, rectangle, 0, 0, width, height, GraphicsUnit.Pixel, wrapMode);
                    graphics.DrawImage(BitmapMapOverlay, rectangle, 0, 0, width, height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            try
            {
                saveBitmap.Save(bitmapPath);
            }
            finally{  }
        }

        public void Export(string pathOfExportFile)
        {
            var template = new StringBuilder(1000);

            foreach (var markerItem in MarkerItems)
            {
                var color = markerItem.R + " " + markerItem.G + " " + markerItem.B;

                template.Append(markerItem.Name + "={" + Environment.NewLine);
                template.Append("   location=" + markerItem.Location + Environment.NewLine);
                template.Append(Environment.NewLine);
                template.Append("   color={" + Environment.NewLine);
                template.Append("      " + color + Environment.NewLine);
                template.Append("   }" + Environment.NewLine);
                template.Append(Environment.NewLine);
                template.Append("   members={" + Environment.NewLine);
                template.Append("      " + markerItem.ListMembers() + Environment.NewLine);
                template.Append("   }" + Environment.NewLine);

                foreach (var item in markerItem.OutgoingItems)
                {
                    template.Append(Environment.NewLine);
                    template.Append("   outgoing={" + Environment.NewLine);
                    template.Append("      name=\"" + item.Name + "\"" + Environment.NewLine);
                    template.Append(Environment.NewLine);
                    template.Append("      path={" + Environment.NewLine);
                    template.Append("      }" + Environment.NewLine);
                    template.Append(Environment.NewLine);
                    template.Append("      control={" + Environment.NewLine);
                    template.Append("      }" + Environment.NewLine);
                    template.Append("   }" + Environment.NewLine);
                }

                if (markerItem.OutgoingItems.Count == 0)
                {
                    template.Append(Environment.NewLine);
                    template.Append("   end=yes" + Environment.NewLine);
                }

                template.Append("}" + Environment.NewLine + Environment.NewLine);
            }

            File.WriteAllText(pathOfExportFile, template.ToString());
        }

        public Bitmap ZoomImage(int xPos, int yPos, int width, int height)
        {
            int halfWidth = width / 2;
            int halfHeight = height / 2;

            if (xPos < halfWidth)
            {
                xPos = 0;
            }
            else
            {
                xPos -= halfWidth;
            }

            if (yPos < halfHeight)
            {
                yPos = 0;
            }
            else
            {
                yPos -= halfHeight;
            }

            var resizedRect = new Rectangle(0, 0, width, height);

            var resizedImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(BitmapMap, resizedRect, xPos, yPos, width, height, GraphicsUnit.Pixel, wrapMode);
                    graphics.DrawImage(BitmapMapOverlay, resizedRect, xPos, yPos, width, height, GraphicsUnit.Pixel, wrapMode);

                    graphics.DrawImage(m_bitmapMouseCursor, halfWidth, halfHeight, m_bitmapMouseCursor.Width, m_bitmapMouseCursor.Height);
                }
            }

            return resizedImage;
        }

        #region Helper
        private void DrawCheckeredListPixel(ProvinceDefinitionItem province, Color drawColor)
        {
            foreach (var pixel in province.Pixels.Where(pixel => pixel.XPos % 3 == 1 && pixel.YPos % 3 == 1))
                BitmapMapOverlay.SetPixel(pixel.XPos, pixel.YPos, drawColor);
        }

        private void DrawHorizontalListPixel(ProvinceDefinitionItem province, Color drawColor)
        {
            foreach (var pixel in province.Pixels.Where(pixel => pixel.YPos % 4 == 1))
                BitmapMapOverlay.SetPixel(pixel.XPos, pixel.YPos, drawColor);
        }

        private void DrawVerticalListPixel(ProvinceDefinitionItem province, Color drawColor)
        {
            foreach (var pixel in province.Pixels.Where(pixel => pixel.XPos % 4 == 1))
                BitmapMapOverlay.SetPixel(pixel.XPos, pixel.YPos, drawColor);
        }

        private int GetMaxIdProvince() => Provinces.Select(province => province.Id).Prepend(0).Max();

        #endregion

        public List<MarkerItem> MarkerItems { get; set; } = new List<MarkerItem>();
        public List<ProvinceDefinitionItem> Provinces { get; set; } = new List<ProvinceDefinitionItem>();
        public Bitmap BitmapMapOverlay { get; set; }
        public Bitmap BitmapMap { get; set; }
    }
}
