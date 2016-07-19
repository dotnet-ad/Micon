namespace Micon.Windows
{
    using System.Windows.Media;

    public static class ColorExtensions
    {
        public static string ToHex(this Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B);
        }

        public static Portable.Graphics.Color FromNative(this Color native)
        {
            return Portable.Graphics.Color.FromRgb(
                native.R / 255.0,
                native.G / 255.0,
                native.B / 255.0
            );
        }
        public static Color ToNative(this Portable.Graphics.Color portable)
        {
            return Color.FromRgb(
                (byte)((int)(255 * portable.R)),
                (byte)((int)(255 * portable.G)),
                (byte)((int)(255 * portable.B))
            );
        }

        
    }
}
