using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace EUProvinceEditor.Common
{
    public class MarkerItem
    {
        public MarkerItem()
        {
        }

        public MarkerItem(string name, int r, int g, int b, int a, int location, string outgoing)
        {
            Name = name;
            R = r;
            G = g;
            B = b;
            A = a;
            Location = location;
            Outgoing = outgoing;

            Color = Color.FromArgb(R, G, B);
        }

        public string ListMembers() => string.Join(" ", Provinces.Select(p => p.Id.ToString()));

        public string Name { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }
        public int Location { get; set; }
        public List<MarkerItem> OutgoingItems { get; set; } = new List<MarkerItem>();
        public List<ProvinceDefinitionItem> Provinces { get; set; } = new List<ProvinceDefinitionItem>();
        public Color Color { get; set; }
        public string Outgoing { get; set; }
    }
}
