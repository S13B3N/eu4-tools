using System.Collections.Generic;

namespace EUProvinceEditor.Common
{
    public class ProvinceDefinitionItem
    {
        public ProvinceDefinitionItem()
        {
            Id = 0;
            R = 0;
            G = 0;
            B = 0;
            Name = string.Empty;
            X = string.Empty;
        }

        public ProvinceDefinitionItem(int id, int r, int g, int b, string name, string x)
        {
            Id = id;
            R = r;
            G = g;
            B = b;
            Name = name;
            X = x;
        }

        public int Id { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public string Name { get; set; }
        public string X { get; set; }
        public List<Pixel> Pixels { get; set; } = new List<Pixel>();
    }
}
