using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class ConfigBL
    {
       public async Task<int> CreateAsync(Config config)
       {
          return await ConfigDAL.CreateAsync(config);
       }

       public async Task<int> UpdateAsync(Config config)
       {
          return await ConfigDAL.UpdateAsync(config);
       }

       public async Task<int> DeleteAsync(Config config)
       {
          return await ConfigDAL.DeleteAsync(config);
       }

       public async Task<Config> GetByIdAsync(Config config)
       {
          return await ConfigDAL.GetByIdAsync(config);
       }

       public async Task<List<Config>> GetAllAsync()
       {
          return await ConfigDAL.GetAllAsync();
       }

       public async Task<List<Config>> SearchAsync(Config config)
       {
          return await ConfigDAL.SearchAsync(config);
       }
    }
}
