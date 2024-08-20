namespace Linkout.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string body { get; set; }
        public UserModel user { get; set; }
        public int likes { get; set; }
    }
}
