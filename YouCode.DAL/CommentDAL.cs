using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YouCode.BE;

namespace YouCode.DAL
{
    public class CommentDAL
    {
        public static async Task<int> CreateAsync(Comment comment)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(comment);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Comment comment)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var commentDB = await bdContexto.Comment.FirstOrDefaultAsync(c => c.Id == comment.Id);
                if (commentDB != null)
                {
                    commentDB.Id = comment.Id;
                    commentDB.IdPost = comment.IdPost;
                    commentDB.Content = comment.Content;
                    
                    bdContexto.Update(commentDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Comment comment)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var commentDB = await bdContexto.Comment.FirstOrDefaultAsync(c => c.Id == comment.Id);
                if (commentDB != null)
                {
                    bdContexto.Comment.Remove(commentDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Comment> GetByIdAsync(Comment comment)
        {
            var commentDB = new Comment();
            using (var bdContexto = new ContextoDB())
            {
                commentDB = await bdContexto.Comment.FirstOrDefaultAsync(c => c.Id == comment.Id);
            }
            return commentDB;
        }
        public static async Task<List<Comment>> GetAllAsync()
        {
            var comment = new List<Comment>();
            using (var bdContexto = new ContextoDB())
            {
                comment = await bdContexto.Comment.ToListAsync();
            }

            return comment;
        }
        internal static IQueryable<Comment> QuerySelect(IQueryable<Comment> query, Comment comment)
        {
            if (comment.Id > 0)
            {
                query = query.Where(c => c.Id == comment.Id);
            }
            if (comment.IdPost > 0)
            {
                query = query.Where(c => c.IdPost == comment.IdPost);
            }
            if (!string.IsNullOrWhiteSpace(comment.Content))
            {
                query = query.Where(c => c.Content.Contains(comment.Content));
            }

            query = query.OrderByDescending(c => c.Id);
            if (comment.Top_Aux > 0)
            {
                query = query.Take(comment.Top_Aux).AsQueryable();
            }
            return query;
        }

        public static async Task<List<Comment>> SearchAsync(Comment comment)
        {
            var comments = new List<Comment>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Comment.AsQueryable();
                select = QuerySelect(select, comment);
                comments = await select.ToListAsync();
            }
            return comments;
        }
    }
}
