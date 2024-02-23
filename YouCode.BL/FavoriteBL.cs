using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class FavoriteBL
    {
        public async Task<int> CreateAsync(Favorite favorite)
        {
            return await FavoriteDAL.CreateAsync(favorite);
        }

        public async Task<int> UpdateAsync(Favorite favorite)
        {
            return await FavoriteDAL.UpdateAsync(favorite);
        }

        public async Task<int> DeleteAsync(Favorite favorite)
        {
            return await FavoriteDAL.DeleteAsync(favorite);
        }

        public async Task<Favorite> GetByIdAsync(Favorite favorite)
        {
            return await FavoriteDAL.GetByIdAsync(favorite);
        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await FavoriteDAL.GetAllAsync();
        }

        public async Task<List<Favorite>> SearchAsync(Favorite favorite)
        {
            return await FavoriteDAL.SearchAsync(favorite);
        }

    }
}
