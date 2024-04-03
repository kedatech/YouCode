using YouCode.BE;

namespace YouCode.GUI.Models.DTOs
{
    public class ProfileReturnDto
    {
        public Profile Profile { get; set;} = null!;
        public List<PostDto>? Posts { get; set; }
    }
}