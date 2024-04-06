using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class PostBL
    {
        public async Task<int> CreateAsync(Post post)
        {
            return await PostDAL.CreateAsync(post);
        }

        public async Task<Post> GetCreateAsync(Post post)
        {
            return await PostDAL.GetCreateAsync(post);
        }

        public async Task<int> UpdateAsync(Post post)
        {
            return await PostDAL.UpdateAsync(post);
        }

        public async Task<int> DeleteAsync(Post post)
        {
            return await PostDAL.DeleteAsync(post);
        }

        public async Task<Post> GetByIdAsync(Post post)
        {
            return await PostDAL.GetByIdAsync(post);
        }

        public async Task<Post> GetByUserIdAsync(int IdUser)
        {
            return await PostDAL.GetByUserIdAsync(IdUser);
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await PostDAL.GetAllAsync();
        }

        public async Task<List<Post>> SearchAsync(Post post)
        {
            return await PostDAL.SearchAsync(post);
        }

    } 
}
