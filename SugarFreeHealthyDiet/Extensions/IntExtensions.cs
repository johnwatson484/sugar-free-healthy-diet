using System;

namespace SugarFreeHealthyDiet.Extensions
{
    public static class IntExtensions
    {
        public static string ToPluralizableString(this int value, string singular)
        {
            return value == 1 ? string.Format("{0}, {1}", value, singular) : string.Format("{0} {1}s", value, singular);
        }
    }
}
