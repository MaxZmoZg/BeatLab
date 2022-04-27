namespace System.Web.Mvc.Html
{
    public static class LinkShortExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper,
                                               string linkText,
                                               string actionName,
                                               string controllerName,
                                               object htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: actionName,
                                         controllerName: controllerName,
                                         routeValues: new { },
                                         htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper,
                                               string linkText,
                                               string controllerAndActionName,
                                               string @class)
        {
            string[] controllerAndActionArray = controllerAndActionName.Split('.');
            return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: controllerAndActionArray[1],
                                         controllerName: controllerAndActionArray[0],
                                         routeValues: new { },
                                         htmlAttributes: new { @class });
        }
    }
}