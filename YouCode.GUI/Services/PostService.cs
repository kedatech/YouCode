using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Models.DTOs;

namespace YouCode.GUI.Services
{
    public class PostService
    {
        private readonly ProfileBL profileBL = new ProfileBL();
        private readonly PostBL postBL = new PostBL();
        private readonly ImageBL imageBL = new ImageBL();
        private readonly FavoriteBL favoriteBL = new FavoriteBL();
        private readonly ReactionBL reactionBL = new ReactionBL();
        private readonly CommentBL commentBL = new CommentBL();
        private readonly UserBL userBL = new UserBL();

        public async Task<List<PostDto>> GetAllAsync(string? username)
        {
            var posts = await postBL.GetAllAsync();

            var postFiltered = posts.FindAll(p => {
                if (username == null)
                {
                    return true;
                }
                return p.User.Username == username;
            });
            var postDtos = new List<PostDto>();
                
            foreach (var post in postFiltered)
            {
                var profile = await profileBL.GetByIdAsync(new Profile { Id = post.User.Id });
                var Allimages = await imageBL.GetAllAsync();
                var images = Allimages.FindAll(i => i.IdPost == post.Id);

                var allfav = await favoriteBL.GetAllAsync();
                var favorites = allfav.FindAll(f => f.IdPost == post.Id);

                var allreactions = await reactionBL.GetAllAsync();
                var allcomments = await commentBL.GetAllAsync();
                var reactions = allreactions.Where(r => r.IdPost == post.Id).ToList();
                var comments = allcomments.Where(c => c.IdPost == post.Id).ToList();

                foreach(var c in comments)
                {
                    //Obtengo el user
                    User u = await userBL.GetByUsernameAsync(c.User.Username);
                    //obtengo el perfil
                    Profile p = await profileBL.GetByIdAsync(new Profile {Id = u.Id});
                    c.AvatarUrl = p.AvatarUrl;
                }

                postDtos.Add(new PostDto
                {
                    Title = post.Title,
                    IdUser = post.IdUser,
                    User = post.User,
                    Id = post.Id,
                    UserProfile = profile,
                    Content = post.Content,
                    PostedAt = post.PostedAt,
                    Images = images,
                    Favorites = favorites,
                    Reactions = reactions,
                    Comments = comments
                });
            }
            return postDtos;
        }
    }
}