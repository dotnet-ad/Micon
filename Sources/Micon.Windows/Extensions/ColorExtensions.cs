namespace Micon.Windows
{
    using System.Windows.Media;

    public static class ColorExtensions
    {
        public static string ToHex(this Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B);
        }

        public static NGraphics.Color FromNative(this Color native)
        {
            return NGraphics.Color.FromRGB(
                native.R / 255.0,
                native.G / 255.0,
                native.B / 255.0
            );
        }
        public static Color ToNative(this NGraphics.Color portable)
        {
            return Color.FromRgb(
                (byte)((int)(255 * portable.Red)),
                (byte)((int)(255 * portable.Green)),
                (byte)((int)(255 * portable.Blue))
            );
        }

        
    }
}
