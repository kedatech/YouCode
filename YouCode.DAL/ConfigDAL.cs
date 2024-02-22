using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class ConfigDAL
{
    public static async Task<int> CreateAsync(Config config)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(config);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Config config)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var configDB = await bdContexto.Config.FirstOrDefaultAsync(c => c.Id == config.Id);
                if (configDB != null)
                {
                    configDB.Id = config.Id;
                    bdContexto.Update(configDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Config config)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var configDB = await bdContexto.Config.FirstOrDefaultAsync(c => c.Id == config.Id);
                if (configDB != null)
                {
                    bdContexto.Config.Remove(configDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Config> GetByIdAsync(Config config)
        {
            var configDB = new Config();
            using (var bdContexto = new ContextoDB())
            {
                configDB = await bdContexto.Config.FirstOrDefaultAsync(c => c.Id == config.Id);
            }
            return configDB;
        }
        public static async Task<List<Config>> GetAllAsync()
        {
            var config = new List<Config>();
            using (var bdContexto = new ContextoDB())
            {
                config = await bdContexto.Config.ToListAsync();
            }

            return config;
        }
        internal static IQueryable<Config> QuerySelect(IQueryable<Config> query, Config config)
        {
            if (config.Id > 0)
            {
                query = query.Where(c => c.Id == config.Id);
            }
            if (config.IdUser > 0)
            {
                query = query.Where(c => c.IdUser == config.IdUser);
            }

            return query;
        }

        public static async Task<List<Config>> SearchAsync(Config config)
        {
            var configs = new List<Config>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Config.AsQueryable();
                select = QuerySelect(select, config);
                configs = await select.ToListAsync();
            }
            return configs;
        }
    
}
