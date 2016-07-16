using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micon
{
    public static class DoubleExtensions
    {
        public static double Lerp(this double start, double end, double amount)
        {
            var difference = end - start;
            var adjusted = difference * amount;
            return start + adjusted;
        }
    }
}
