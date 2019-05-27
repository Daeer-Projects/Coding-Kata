using System;

using FootballComponentV2.Types;

namespace FootballComponentV2.Extensions
{
    /// <summary>
    /// Some extension methods used in the football component.
    /// </summary>
    public static class FootballExtensions
    {
        /// <summary>
        /// A simple calculation to identify the point difference.
        /// </summary>
        /// <param name="football"> The football object we are using for the calculation. </param>
        /// <returns> The points difference. </returns>
        public static int CalculatePointDifference(this Football football)
        {
            return Math.Abs(football.ForPoints - football.AgainstPoints);
        }
    }
}
