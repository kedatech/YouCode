using YouCode.BE;

namespace YouCode.GUI.Models.DTOs
{
    public class PostDto : Post
    {
        public Profile? UserProfile { get; set; }
        public List<Image>? Images { get; set; }
        public int FavoriteCount { get; set; }
        public List<Reaction>? Reactions { get; set; }

    }
}