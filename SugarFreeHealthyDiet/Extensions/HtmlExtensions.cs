using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SugarFreeHealthyDiet.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent PreserveNewLine(this IHtmlHelper htmlHelper, string value)
        {
            return new HtmlString(value == null ? value : value.Replace(Environment.NewLine, "<br/>"));
        }
    }
}
