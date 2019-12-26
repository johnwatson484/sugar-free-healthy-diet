using System;

namespace SugarFreeHealthyDiet.Extensions
{
    public static class StringExtensions
    {
        public static bool IsInteger(this string value)
        {
            int i;
            return int.TryParse(value, out i);
        }
    }
}
