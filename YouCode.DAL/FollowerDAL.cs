using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class FollowerDAL
{
    public static async Task<int> CreateAsync(Follower follower)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(follower);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Follower follower)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var followerDB = await bdContexto.Follower.FirstOrDefaultAsync(c => c.Id == follower.Id);
                if (followerDB != null)
                {
                    followerDB.Id = follower.Id;
                    followerDB.IdFollower = follower.IdFollower;
                    followerDB.IdFollow = follower.IdFollow;
                    followerDB.FollowedAt = follower.FollowedAt;
                    bdContexto.Update(followerDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Follower follower)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var followerDB = await bdContexto.Follower.FirstOrDefaultAsync(c => c.Id == follower.Id);
                if (followerDB != null)
                {
                    bdContexto.Follower.Remove(followerDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Follower> GetByIdAsync(Follower follower)
        {
            var followerDB = new Follower();
            using (var bdContexto = new ContextoDB())
            {
                followerDB = await bdContexto.Follower.FirstOrDefaultAsync(c => c.Id == follower.Id);
            }
            return followerDB;
        }
        public static async Task<List<Follower>> GetAllAsync()
        {
            var follower = new List<Follower>();
            using (var bdContexto = new ContextoDB())
            {
                follower = await bdContexto.Follower.ToListAsync();
            }

            return follower;
        }
        internal static IQueryable<Follower> QuerySelect(IQueryable<Follower> query, Follower follower)
        {
            if (follower.Id > 0)
            {
                query = query.Where(c => c.Id == follower.Id);
            }
            if (follower.IdFollower > 0)
            {
                query = query.Where(c => c.IdFollower == follower.IdFollower);
            }
            if (follower.IdFollow > 0)
            {
                query = query.Where(c => c.IdFollow == follower.IdFollow);
            }

            query = query.OrderByDescending(c => c.Id);
            if (follower.Top_Aux > 0)
            {
                query = query.Take(follower.Top_Aux).AsQueryable();
            }
            return query;
        }

        public static async Task<List<Follower>> SearchAsync(Follower follower)
        {
            var followers = new List<Follower>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Follower.AsQueryable();
                select = QuerySelect(select, follower);
                followers = await select.ToListAsync();
            }
            return followers;
        }
}
