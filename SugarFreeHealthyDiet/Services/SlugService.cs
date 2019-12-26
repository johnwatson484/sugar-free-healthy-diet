using System;

namespace SugarFreeHealthyDiet.Services
{
    public class SlugService : ISlugService
    {
        public int GetIdFromSlug(string slug)
        {
            int index = slug.IndexOf('-');
            if (index != -1)
            {
                int i = 0;
                if (int.TryParse(slug.Substring(0, index), out i))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
