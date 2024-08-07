using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DecimalExtensions
    {
        public static decimal Normalize(this decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }

        public static decimal? Normalize(this decimal? value)
        {
            if (!value.HasValue)
                return null;

            return value / 1.000000000000000000000000000000000m;
        }
    }
}
