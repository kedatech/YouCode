using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class ReactionBL
    {
        public async Task<int> CreateAsync(Reaction reaction)
        {
            return await ReactionDAL.CreateAsync(reaction);
        }

        public async Task<int> UpdateAsync(Reaction reaction)
        {
            return await ReactionDAL.UpdateAsync(reaction);
        }

        public async Task<int> DeleteAsync(Reaction reaction)
        {
            return await ReactionDAL.DeleteAsync(reaction);
        }
        public async Task<int> DeleteOnAllPostAsync(int IdPost)
        {
            return await ReactionDAL.DeleteOnAllPostAsync(IdPost);
        }

        public async Task<Reaction> GetByIdAsync(Reaction reaction)
        {
            return await ReactionDAL.GetByIdAsync(reaction);
        }

        public async Task<List<Reaction>> GetAllAsync()
        {
            return await ReactionDAL.GetAllAsync();
        }

        public async Task<List<Reaction>> SearchAsync(Reaction reaction)
        {
            return await ReactionDAL.SearchAsync(reaction);
        }
    }
}
