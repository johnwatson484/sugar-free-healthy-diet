using System;

namespace SugarFreeHealthyDiet.Services
{
    public class SlugService : ISlugService
    {
        public int GetIdFromSlug(string slug)
        {
            return int.Parse(slug.Substring(0, slug.IndexOf('-')));
        }
    }
}
