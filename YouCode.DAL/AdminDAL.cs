using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class AdminDAL
{
    public static async Task<int> CreateAsync(Admin admin)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(admin);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Admin admin)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var adminDB = await bdContexto.Admin.FirstOrDefaultAsync(c => c.Id == admin.Id);
                if (adminDB != null)
                {
                    adminDB.Id = admin.Id;
                    adminDB.IdUser = admin.IdUser;
                    adminDB.CreatedAt = admin.CreatedAt;
                    bdContexto.Update(adminDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Admin admin)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var adminDB = await bdContexto.Admin.FirstOrDefaultAsync(c => c.Id == admin.Id);
                if (adminDB != null)
                {
                    bdContexto.Admin.Remove(adminDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Admin> GetByIdAsync(Admin admin)
        {
            var adminDB = new Admin();
            using (var bdContexto = new ContextoDB())
            {
                adminDB = await bdContexto.Admin.FirstOrDefaultAsync(c => c.Id == admin.Id);
            }
            if(adminDB != null)
            {
                return adminDB;
            }
            else
            {
                return new Admin();
            }
        }
        public static async Task<List<Admin>> GetAllAsync()
        {
            var admin = new List<Admin>();
            using (var bdContexto = new ContextoDB())
            {
                admin = await bdContexto.Admin.ToListAsync();
            }

            return admin;
        }
        internal static IQueryable<Admin> QuerySelect(IQueryable<Admin> query, Admin admin)
        {
            if (admin.Id > 0)
            {
                query = query.Where(c => c.Id == admin.Id);
            }
            return query;
        }

        public static async Task<List<Admin>> SearchAsync(Admin admin)
        {
            var admins = new List<Admin>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Admin.AsQueryable();
                select = QuerySelect(select, admin);
                admins = await select.ToListAsync();
            }
            return admins;
        }
}

