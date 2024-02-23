using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class CommentBL
    {
        public async Task<int> CreateAsync(Comment comment)
        {
            return await CommentDAL.CreateAsync(comment);
        }

        public async Task<int> UpdateAsync(Comment comment)
        {
            return await CommentDAL.UpdateAsync(comment);
        }

        public async Task<int> DeleteAsync(Comment comment)
        {
            return await CommentDAL.DeleteAsync(comment);
        }

        public async Task<Comment> GetByIdAsync(Comment comment)
        {
            return await CommentDAL.GetByIdAsync(comment);
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await CommentDAL.GetAllAsync();
        }

        public async Task<List<Comment>> SearchAsync(Comment comment)
        {
            return await CommentDAL.SearchAsync(comment);
        }
    }
}
