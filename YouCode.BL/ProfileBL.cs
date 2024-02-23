using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YouCode.BL
{
    public class ProfileBL
    {
        public async Task<int> CreateAsync(Profile profile)
        {
            return await ProfileDAL.CreateAsync(profile);
        }

        public async Task<int> UpdateAsync(Profile profile)
        {
            return await ProfileDAL.UpdateAsync(profile);
        }

        public async Task<int> DeleteAsync(Profile profile)
        {
            return await ProfileDAL.DeleteAsync(profile);
        }

        public async Task<Profile> GetByIdAsync(Profile profile)
        {
            return await ProfileDAL.GetByIdAsync(profile);
        }

        public async Task<List<Profile>> GetAllAsync()
        {
            return await ProfileDAL.GetAllAsync();
        }

        public async Task<List<Profile>> SearchAsync(Profile profile)
        {
            return await ProfileDAL.SearchAsync(profile);
        }
    }
}
