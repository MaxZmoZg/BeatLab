using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeatLab.Models
{
    public class UserContext:DbContext
    {
        public UserContext():
            base("DefaultConnection")
        { }
        public DbSet<BeatLab.User> Users { get; set; }
        public DbSet<BeatLab.User_Type> Roles { get; set; }
    }
    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            User_Type admin = new User_Type { Name_User_Type = "admin" };
            User_Type user = new User_Type { Name_User_Type = "user" };
            db.Roles.Add(admin);
            db.Roles.Add(user);
            db.Users.Add(new BeatLab.User
            {
               
                Email_User = "somemail@gmail.com",
                Login ="admin1",
                Password = "123456",
                ID_User_Type = 1
            });;
            base.Seed(db);
          
        }
    }
}