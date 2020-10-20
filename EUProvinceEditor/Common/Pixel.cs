namespace EUProvinceEditor.Common
{
    public class Pixel
    {
        public Pixel() : this(0,0) { }

        public Pixel(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}
