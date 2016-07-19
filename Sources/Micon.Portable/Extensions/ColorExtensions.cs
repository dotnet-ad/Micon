using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micon.Portable.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Creates a suggested color that fits for gradients.
        /// </summary>
        /// <returns>The corresponding color</returns>
        public static Color CreateGradientOpposite(this Color color)
        {
            var newHue = (color.Hue + (2.0 / 3)) % 1.0;
            return color.WithHue(newHue).Lerp(color,0.70);
        }

        public static Color Lerp(this Color color, Color to, double amount)
        {
            double sr = color.Red, sg = color.Green, sb = color.Blue;
            double er = to.Red, eg = to.Green, eb = to.Blue;

            var r = sr.Lerp(er, amount);
            var g = sg.Lerp(eg, amount);
            var b = sb.Lerp(eb, amount);

            return Color.FromRGB(r, g, b);
        }
    }
}
