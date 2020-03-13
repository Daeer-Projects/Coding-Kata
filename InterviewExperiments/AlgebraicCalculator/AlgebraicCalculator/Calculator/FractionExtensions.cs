using System;

namespace Calculator
{
    public static class FractionExtensions
    {
        /// <summary>
        /// Code here is taken from internet.
        /// I am not responsible for this code!
        /// </summary>
        /// <param name="numberToConvert"></param>
        /// <param name="denominationPrecision"></param>
        /// <returns></returns>
        public static string ToFraction(this double numberToConvert, int denominationPrecision = 4096)
        {
            /* Translated from the C version. */
            /*  a: continued fraction coefficients. */
            long numerator;
            long denominator;
            var h = new long[3] { 0, 1, 0 };
            var k = new long[3] { 1, 0, 0 };
            long n = 1;
            var neg = 0;

            if (denominationPrecision <= 1)
            {
                denominator = 1;
                numerator = (long)numberToConvert;
                return $"{numerator}/{denominator}";
            }

            if (numberToConvert < 0) { neg = 1; numberToConvert = -numberToConvert; }

            while (numberToConvert != Math.Floor(numberToConvert)) { n <<= 1; numberToConvert *= 2; }
            var d = (long)numberToConvert;

            /* continued fraction and check denominator each step */
            for (var i = 0; i < 64; i++)
            {
                var a = (n != 0) ? d / n : 0;
                if ((i != 0) && (a == 0)) break;

                var x = d; d = n; n = x % n;

                x = a;
                if (k[1] * a + k[0] >= denominationPrecision)
                {
                    x = (denominationPrecision - k[0]) / k[1];
                    if (x * 2 >= a || k[1] >= denominationPrecision)
                        i = 65;
                    else
                        break;
                }

                h[2] = x * h[1] + h[0]; h[0] = h[1]; h[1] = h[2];
                k[2] = x * k[1] + k[0]; k[0] = k[1]; k[1] = k[2];
            }
            denominator = k[1];
            numerator = neg != 0 ? -h[1] : h[1];
            return $"{numerator}/{denominator}";
        }
    }
}
