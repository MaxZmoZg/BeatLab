namespace BeatLab.Models.Entities
{
    public partial class Chat
    {
        public User Receiver => User;
        public User Sender => User1;
    }
}