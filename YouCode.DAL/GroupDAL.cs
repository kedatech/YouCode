using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class GroupDAL
{
    public static async Task<int> CreateAsync(BE.Group group)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(group);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(BE.Group group)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var groupDB = await bdContexto.Group.FirstOrDefaultAsync(c => c.Id == group.Id);
                if (groupDB != null)
                {
                    groupDB.Id = group.Id;
                    groupDB.IdUser = group.IdUser;
                    groupDB.Name = group.Name;
                    groupDB.Description = group.Description;
                    groupDB.CreatedAt = group.CreatedAt;

                    bdContexto.Update(groupDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(BE.Group group)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var groupDB = await bdContexto.Group.FirstOrDefaultAsync(c => c.Id == group.Id);
                if (groupDB != null)
                {
                    bdContexto.Group.Remove(groupDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<BE.Group> GetByIdAsync(BE.Group group)
        {
            var groupDB = new BE.Group();
            using (var bdContexto = new ContextoDB())
            {
                groupDB = await bdContexto.Group.FirstOrDefaultAsync(c => c.Id == group.Id);
            }
            return groupDB;
        }
        public static async Task<List<BE.Group>> GetAllAsync()
        {
            var group = new List<BE.Group>();
            using (var bdContexto = new ContextoDB())
            {
                group = await bdContexto.Group.ToListAsync();
            }

            return group;
        }
        internal static IQueryable<BE.Group> QuerySelect(IQueryable<BE.Group> query, BE.Group group)
        {
            if (group.Id > 0)
            {
                query = query.Where(c => c.Id == group.Id);
            }
            if (group.IdUser > 0)
            {
                query = query.Where(c => c.IdUser == group.IdUser);
            }

            if (!string.IsNullOrWhiteSpace(group.Name))
            {
                query = query.Where(c => c.Name.Contains(group.Name));
            }

            query = query.OrderByDescending(c => c.Id);
            if (group.Top_Aux > 0)
            {
                query = query.Take(group.Top_Aux).AsQueryable();
            }
            return query;
        }

        public static async Task<List<BE.Group>> SearchAsync(BE.Group group)
        {
            var groups = new List<BE.Group>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Group.AsQueryable();
                select = QuerySelect(select, group);
                groups = await select.ToListAsync();
            }
            return groups;
        }
    
}
