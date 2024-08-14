#if UNITY_2022_2_OR_NEWER
using System;
using UnityEngine;

namespace NiGames.PlayerPrefs.Utility
{
    internal static class ResolutionUtility
    {
        private const int MAX_DENOMINATOR = 1000000;
        
        public static (uint numerator, uint denominator) HertzToRatio(double hertz, double tolerance = 1e-9)
        {
            var bestError = double.MaxValue;
            uint bestNumerator = 0;
            uint bestDenominator = 1;

            for (uint denominator = 1; denominator <= MAX_DENOMINATOR; denominator++)
            {
                var numerator = (uint)Math.Round(hertz * denominator);
                var error = Math.Abs((double)numerator / denominator - hertz);

                if (error < bestError)
                {
                    bestError = error;
                    bestNumerator = numerator;
                    bestDenominator = denominator;

                    if (error < tolerance) break;
                }
            }

            return (bestNumerator, bestDenominator);
        }

        public static RefreshRate ConvertToRefreshRateRatio(double hertz)
        {
            var (numerator, denominator) = HertzToRatio(hertz);
            
            return new RefreshRate
            {
                numerator = numerator, 
                denominator = denominator,
            };
        }
    }
}
#endif
