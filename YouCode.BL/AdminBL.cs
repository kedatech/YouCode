using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class AdminBL
    {
        public async Task<int> CreateAsync(Admin admin)
        {
            return await AdminDAL.CreateAsync(admin);
        }

        public async Task<int> UpdateAsync(Admin admin)
        {
            return await AdminDAL.UpdateAsync(admin);
        }

        public async Task<int> DeleteAsync(Admin admin)
        {
            return await AdminDAL.DeleteAsync(admin);
        }

        public async Task<Admin> GetByIdAsync(Admin admin)
        {
            return await AdminDAL.GetByIdAsync(admin);
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await AdminDAL.GetAllAsync();
        }

        public async Task<List<Admin>> SearchAsync(Admin admin)
        {
            return await AdminDAL.SearchAsync(admin);
        }
    }
}
