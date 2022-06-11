using System.Web;
using System.Web.Mvc; using BeatLab.Models.Entities; 

namespace BeatLab
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
