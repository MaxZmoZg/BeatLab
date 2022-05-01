namespace System.Web.Mvc.Html
{
    public static class LinkShortExtensions
    {
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

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper,
                                              string linkText,
                                              string controllerAndActionName,
                                              object parameter,
                                              string @class)
        {
            string[] controllerAndActionArray = controllerAndActionName.Split('.');
            return htmlHelper.ActionLink(linkText: linkText,
                                         actionName: controllerAndActionArray[1],
                                         controllerName: controllerAndActionArray[0],
                                         routeValues: parameter,
                                         htmlAttributes: new { @class });
        }
    }
}