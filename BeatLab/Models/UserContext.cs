using System.Data.Entity;

namespace BeatLab.Models
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("DefaultConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Type> Roles { get; set; }
    }
    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            User_Type admin = new User_Type { Name_User_Type = "admin" };
            User_Type user = new User_Type { Name_User_Type = "user" };
            db.Roles.Add(admin);
            db.Roles.Add(user);
            string password = "123456";
            SaltedPasswordGenerator.GenerateHashAndSalt(password,
                                         out byte[] saltBytes,
                                         out byte[] encryptedPasswordAndSalt);
            db.Users.Add(new User
            {

                Email_User = "somemail@gmail.com",
                Login = "admin1",
                Password_Hash = encryptedPasswordAndSalt,
                Salt = saltBytes,
                ID_User_Type = 1
            });
            base.Seed(db);
        }
    }
}