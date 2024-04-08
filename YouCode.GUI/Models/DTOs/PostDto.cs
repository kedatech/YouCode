using YouCode.BE;

namespace YouCode.GUI.Models.DTOs
{
    public class PostDto : Post
    {
        public Profile? UserProfile { get; set; }
        public List<Image>? Images { get; set; }
        public List<Favorite>? Favorites { get; set; }
        public List<Reaction>? Reactions { get; set; }
        public List<Comment>? Comments { get; set; }
        

    }
}