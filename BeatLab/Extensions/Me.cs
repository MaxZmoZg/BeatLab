using BeatLab.Models.Entities;
using System;
using System.Collections.Generic;
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
    }
}