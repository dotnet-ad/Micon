using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micon.Portable.Generation
{
    public class Color
    {
        private Color()
        {

        }

        #region RGB

        private double r,g,b;

        public double R
        {
            get { return r; }
            set { r = Math.Min(1,Math.Max(0,value)); }
        }
        
        public double G
        {
            get { return g; }
            set { g = Math.Min(1, Math.Max(0, value)); }
        }
        
        public double B
        {
            get { return b; }
            set { b = Math.Min(1, Math.Max(0, value)); }
        }

        public static Color FromRgb(double r, double g, double b)
        {
            return new Color()
            {
                R = r,
                G = g,
                B = b,
            };
        }
        public static Color FromRgb(byte r, byte g, byte b)
        {
            return Color.FromRgb(r/255.0, g / 255.0, b / 255.0);
        }

        #endregion

        #region HSL
        
        public double Hue
        {
            get
            {
                var r = R;
                var g = G;
                var b = B;
                var minval = Math.Min(r, Math.Min(g, b));
                var maxval =Math.Max(r, Math.Max(g, b));

                if (maxval == minval)
                    return 0.0f;

                var delta = (maxval - minval);

                var hue = 0.0;
                if (r == maxval) hue = (G - B) / delta;
                if (g == maxval) hue = 2.0 + ((B - R) / delta);
                if (b == maxval) hue = 4.0 + ((R - G) / delta);

                hue *= 60;

                if (hue > 360.0) hue = hue - 360.0;

                return hue;
            }

        }
        
        public double Saturation
        {
            get
            {
                var minval = Math.Min(R, Math.Min(G, B));
                var maxval = Math.Max(R, Math.Max(G, B));

                if (maxval == minval)
                    return 0.0f;

                var sum = maxval + minval;
                if (sum > 1.0)
                    sum = 1 - sum;

                return (double)(maxval - minval) / sum;

            }
        }

        public double Lightness
        {
            get
            {
                var min = Math.Min(Math.Min(this.R,this.G),this.B);
                var max = Math.Max(Math.Min(this.R, this.G), this.B);
                return (min + max) / 2;
            }
        }

        private static double HslTest(double v,double t1, double t2)
        {
            if (6.0 * v < 1.0)
                return t2 + (t1 - t2) * 6.0 * v;

            if(2.0 * v < 1.0)
                return t1;

            if (3.0 * v < 2.0)
                return t2 + (t1 - t2) * ((2.0 / 3.0) - v) * 6.0;

            return t2;
        }

        private static double HslClamp(double v)
        {
            while (v < 0) v += 1.0;
            while (v > 1) v -= 1.0;
            return v;
        }

        /// <summary>
        /// Creates a Color from hue, saturation and lightness.
        /// </summary>
        /// <param name="hue">The hue value.</param>
        /// <param name="saturation">The saturation value.</param>
        /// <param name="lightness">The brightness value.</param>
        /// <returns>A Color with the given values.</returns>
        public static Color FromHsl(double hue, double saturation, double lightness)
        {
            saturation = Math.Min(1, Math.Max(saturation, 0));
            lightness = Math.Min(1, Math.Max(lightness, 0));

            if (0 == saturation)
            {
                return Color.FromRgb(lightness, lightness, lightness);
            }

            double t1;

            if(lightness < 0.5)
            {
                t1 = lightness * (1 + saturation);
            }
            else
            {
                t1 = lightness + saturation - (lightness * saturation);
            }

            double t2 = 2 * lightness - t1;

            var valHue = HslClamp(hue / 360.0);

            var tr = valHue + (1.0 / 3.0);
            var tg = valHue;
            var tb = valHue - (1.0 / 3.0);

            tr = HslClamp(tr);
            tg = HslClamp(tg);
            tb = HslClamp(tb);
            
            tr = HslTest(tr, t1, t2);
            tg = HslTest(tg, t1, t2);
            tb = HslTest(tb, t1, t2);

            return Color.FromRgb(tr, tg, tb);
        }

        #endregion
        
        #region Utilities

        public Color Lerp(Color to, double amount)
        {
            double sr = this.R, sg = this.G, sb = this.B;
            double er = to.R, eg = to.G, eb = to.B;

            var r = sr.Lerp(er, amount);
            var g = sg.Lerp(eg, amount);
            var b = sb.Lerp(eb, amount);

            return Color.FromRgb(r, g, b);
        }

        public static Color FromColor(Color other)
        {
            return Color.FromRgb(other.R, other.G, other.B);
        }

        public Color CreateOpposite()
        {
            var h = this.Hue;
            var s = this.Saturation;
            var l = this.Lightness;

            return Micon.Portable.Generation.Color.FromHsl(h + 240.0, s, l);
        }

        #endregion

    }
}
