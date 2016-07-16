using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Micon.Windows
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B);
        }

        public static Micon.Portable.Generation.Color FromNative(this Color native)
        {
            return Micon.Portable.Generation.Color.FromRgb(
                native.R / 255.0,
                native.G / 255.0,
                native.B / 255.0
            );
        }
        public static Color ToNative(this Micon.Portable.Generation.Color portable)
        {
            return Color.FromRgb(
                (byte)((int)(255 * portable.R)),
                (byte)((int)(255 * portable.G)),
                (byte)((int)(255 * portable.B))
            );
        }

        
    }
}
