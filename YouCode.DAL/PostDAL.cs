using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCode.BE;

namespace YouCode.DAL
{
    public class PostDAL
    {
        public static async Task<int> CreateAsync(Post post)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(post);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        //Busca Id de 
        public static async Task<int> UpdateAsync(Post post)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var postDB = await bdContexto.Post.FirstOrDefaultAsync(c => c.Id == post.Id);
                if (postDB != null)
                {
                    postDB.Id = post.Id;
                    postDB.IdUser = post.IdUser;
                    postDB.Title = post.Title;
                    postDB.Content = post.Content;
                    postDB.PostedAt = post.PostedAt;
                    
                    bdContexto.Update(postDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Post post)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var postDB = await bdContexto.Post.FirstOrDefaultAsync(c => c.Id == post.Id);
                if (postDB != null)
                {
                    bdContexto.Post.Remove(postDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Post> GetByIdAsync(Post post)
        {
            var postDB = new Post();
            using (var bdContexto = new ContextoDB())
            {
                postDB = await bdContexto.Post.FirstOrDefaultAsync(c => c.Id == post.Id);
            }
            return postDB;
        }
        public static async Task<List<Post>> GetAllAsync()
        {
            var post = new List<Post>();
            using (var bdContexto = new ContextoDB())
            {
                post = await bdContexto.Post.ToListAsync();
            }

            return post;
        }
        internal static IQueryable<Post> QuerySelect(IQueryable<Post> query, Post post)
        {
            if (post.Id > 0)
            {
                query = query.Where(c => c.Id == post.Id);
            }
            if (!string.IsNullOrWhiteSpace(post.Title))
            {
                query = query.Where(c => c.Title.Contains(post.Title));
            }

            query = query.OrderByDescending(c => c.Id);
            
            return query;
        }

        public static async Task<List<Post>> SearchAsync(Post post)
        {
            var posts = new List<Post>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Post.AsQueryable();
                select = QuerySelect(select, post);
                posts = await select.ToListAsync();
            }
            return posts;
        }
    }
}
