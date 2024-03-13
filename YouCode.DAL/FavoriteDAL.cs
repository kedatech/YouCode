using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class FavoriteDAL
{
    public static async Task<int> CreateAsync(Favorite favorite)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(favorite);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Favorite favorite)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var favoriteDB = await bdContexto.Favorite.FirstOrDefaultAsync(c => c.Id == favorite.Id);
                if (favoriteDB != null)
                {
                    favoriteDB.Id = favorite.Id;
                    favoriteDB.IdUser = favorite.IdUser;
                    favoriteDB.IdPost = favorite.IdPost;
                    bdContexto.Update(favoriteDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Favorite favorite)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var favoriteDB = await bdContexto.Favorite.FirstOrDefaultAsync(c => c.Id == favorite.Id);
                if (favoriteDB != null)
                {
                    bdContexto.Favorite.Remove(favoriteDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Favorite> GetByIdAsync(Favorite favorite)
        {
            var favoriteDB = new Favorite();
            using (var bdContexto = new ContextoDB())
            {
                favoriteDB = await bdContexto.Favorite.FirstOrDefaultAsync(c => c.Id == favorite.Id);
            }
            return favoriteDB;
        }
        public static async Task<List<Favorite>> GetAllAsync()
        {
            var favorite = new List<Favorite>();
            using (var bdContexto = new ContextoDB())
            {
                favorite = await bdContexto.Favorite.ToListAsync();
            }

            return favorite;
        }
        internal static IQueryable<Favorite> QuerySelect(IQueryable<Favorite> query, Favorite favorite)
        {
            if (favorite.Id > 0)
            {
                query = query.Where(c => c.Id == favorite.Id);
            }
            return query;
        }

        public static async Task<List<Favorite>> SearchAsync(Favorite favorite)
        {
            var favorites = new List<Favorite>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Favorite.AsQueryable();
                select = QuerySelect(select, favorite);
                favorites = await select.ToListAsync();
            }
            return favorites;
        }
    
}
