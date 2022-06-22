using BeatLab.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeatLab
{
    public static class Me
    {
        public static int GetId()
        {
            using (BeatLabDBEntities entities = new BeatLabDBEntities())
            {
                return entities.User
                    .First(u => u.Login == HttpContext.Current.User.Identity.Name).ID_User;
            }
        }

        public static User User
        {
            get
            {
                using (BeatLabDBEntities entities = new BeatLabDBEntities())
                {
                    return entities.User
                        .Include(u => u.User_Type)
                        .Include(u => u.Alboms)
                        .First(u => u.Login == HttpContext.Current.User.Identity.Name);
                }
            }
        }
    }
}