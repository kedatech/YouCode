using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class ReactionDAL
{
    public static async Task<int> CreateAsync(Reaction reaction)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(reaction);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Reaction reaction)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var reactionDB = await bdContexto.Reaction.FirstOrDefaultAsync(c => c.Id == reaction.Id);
                if (reactionDB != null)
                {
                    reactionDB.Id = reaction.Id;
                    bdContexto.Update(reactionDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Reaction reaction)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var reactionDB = await bdContexto.Reaction.FirstOrDefaultAsync(c => c.Id == reaction.Id);
                if (reactionDB != null)
                {
                    bdContexto.Reaction.Remove(reactionDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Reaction> GetByIdAsync(Reaction reaction)
        {
            var reactionDB = new Reaction();
            using (var bdContexto = new ContextoDB())
            {
                reactionDB = await bdContexto.Reaction.FirstOrDefaultAsync(c => c.Id == reaction.Id);
            }
            return reactionDB;
        }
        public static async Task<List<Reaction>> GetAllAsync()
        {
            var reaction = new List<Reaction>();
            using (var bdContexto = new ContextoDB())
            {
                reaction = await bdContexto.Reaction.ToListAsync();
            }

            return reaction;
        }
        internal static IQueryable<Reaction> QuerySelect(IQueryable<Reaction> query, Reaction reaction)
        {
            if (reaction.Id > 0)
            {
                query = query.Where(c => c.Id == reaction.Id);
            }
            if (reaction.IdPost > 0)
            {
                query = query.Where(c => c.IdPost == reaction.IdPost);
            }
            if (reaction.IdUser > 0)
            {
                query = query.Where(c => c.IdUser == reaction.IdUser);
            }
            if (reaction.IdComment > 0)
            {
                query = query.Where(c => c.IdComment == reaction.IdComment);
            }
            return query;
        }

        public static async Task<List<Reaction>> SearchAsync(Reaction reaction)
        {
            var reactions = new List<Reaction>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Reaction.AsQueryable();
                select = QuerySelect(select, reaction);
                reactions = await select.ToListAsync();
            }
            return reactions;
        }
    
}
