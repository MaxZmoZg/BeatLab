namespace System.Web.Mvc.Html
{
    public static class LinkShortExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: actionName,
                                         controllerName: controllerName,
                                         routeValues: new { },
                                         htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

    }
}