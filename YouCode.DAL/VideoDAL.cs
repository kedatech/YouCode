using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class VideoDAL
{
    public static async Task<int> CreateAsync(Video video)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(video);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Video video)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var videoDB = await bdContexto.Video.FirstOrDefaultAsync(c => c.Id == video.Id);
                if (videoDB != null)
                {
                    videoDB.Id = video.Id;
                    videoDB.IdPost = video.IdPost;
                    videoDB.Path = video.Path;

                    bdContexto.Update(videoDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Video video)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var videoDB = await bdContexto.Video.FirstOrDefaultAsync(c => c.Id == video.Id);
                if (videoDB != null)
                {
                    bdContexto.Video.Remove(videoDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Video> GetByIdAsync(Video video)
        {
            var videoDB = new Video();
            using (var bdContexto = new ContextoDB())
            {
                videoDB = await bdContexto.Video.FirstOrDefaultAsync(c => c.Id == video.Id);
            }
            return videoDB;
        }
        public static async Task<List<Video>> GetAllAsync()
        {
            var video = new List<Video>();
            using (var bdContexto = new ContextoDB())
            {
                video = await bdContexto.Video.ToListAsync();
            }

            return video;
        }
        internal static IQueryable<Video> QuerySelect(IQueryable<Video> query, Video video)
        {
            if (video.Id > 0)
            {
                query = query.Where(c => c.Id == video.Id);
            }
            if (video.IdPost > 0)
            {
                query = query.Where(c => c.IdPost == video.IdPost);
            }
            if (!string.IsNullOrWhiteSpace(video.Path))
            {
                query = query.Where(c => c.Path.Contains(video.Path));
            }

            return query;
        }

        public static async Task<List<Video>> SearchAsync(Video video)
        {
            var comments = new List<Video>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Video.AsQueryable();
                select = QuerySelect(select, video);
                comments = await select.ToListAsync();
            }
            return comments;
        }
    
}
