using System;

namespace SugarFreeHealthyDiet.Extensions
{
    public static class IntExtensions
    {
        public static string ToPluralizableString(this int value, string singular)
        {
            return value == 1 ? singular : string.Format("{0}s", singular);
        }
    }
}
