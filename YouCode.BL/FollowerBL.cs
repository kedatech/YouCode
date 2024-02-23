using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class FollowerBL
    {
        public async Task<int> CreateAsync(Follower follower)
        {
            return await FollowerDAL.CreateAsync(follower);
        }

        public async Task<int> UpdateAsync(Follower follower)
        {
            return await FollowerDAL.UpdateAsync(follower);
        }

        public async Task<int> DeleteAsync(Follower follower)
        {
            return await FollowerDAL.DeleteAsync(follower);
        }

        public async Task<Follower> GetByIdAsync(Follower follower)
        {
            return await FollowerDAL.GetByIdAsync(follower);
        }

        public async Task<List<Follower>> GetAllAsync()
        {
            return await FollowerDAL.GetAllAsync();
        }

        public async Task<List<Follower>> SearchAsync(Follower follower)
        {
            return await FollowerDAL.SearchAsync(follower);
        }
    }
}
