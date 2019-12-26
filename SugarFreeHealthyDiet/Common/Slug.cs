using System.Text.RegularExpressions;
using System.Text;

namespace SugarFreeHealthyDiet.Common
{
    public class Slug
    {
        protected string phrase;
        public Slug(string phrase)
        {
            this.phrase = phrase;
        }

        public virtual string Generate()
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = CodePagesEncodingProvider.Instance.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
