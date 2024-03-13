using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class GroupBL
    {
        public async Task<int> CreateAsync(Group group)
        {
            return await GroupDAL.CreateAsync(group);
        }

        public async Task<int> UpdateAsync(Group group)
        {
            return await GroupDAL.UpdateAsync(group);
        }

        public async Task<int> DeleteAsync(Group group)
        {
            return await GroupDAL.DeleteAsync(group);
        }

        public async Task<Group> GetByIdAsync(Group group)
        {
            return await GroupDAL.GetByIdAsync(group);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await GroupDAL.GetAllAsync();
        }

        public async Task<List<Group>> SearchAsync(Group group)
        {
            return await GroupDAL.SearchAsync(group);
        }
    }
}
