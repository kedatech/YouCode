﻿using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class ProfileDAL
{
    public static async Task<int> CreateAsync(Profile profile)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(profile);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Profile profile)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var profileDB = await bdContexto.Profile.FirstOrDefaultAsync(c => c.Id == profile.Id);
                if (profileDB != null)
                {
                    profileDB.Id = profile.Id;
                    profileDB.IdUser = profile.IdUser;
                    profileDB.MoreInfo = profile.MoreInfo;
                    bdContexto.Update(profileDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Profile profile)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var profileDB = await bdContexto.Profile.FirstOrDefaultAsync(c => c.Id == profile.Id);
                if (profileDB != null)
                {
                    bdContexto.Profile.Remove(profileDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Profile> GetByIdAsync(Profile profile)
        {
            var profileDB = new Profile();
            using (var bdContexto = new ContextoDB())
            {
                profileDB = await bdContexto.Profile.FirstOrDefaultAsync(c => c.Id == profile.Id);
            }
            return profileDB;
        }
        public static async Task<List<Profile>> GetAllAsync()
        {
            var profile = new List<Profile>();
            using (var bdContexto = new ContextoDB())
            {
                profile = await bdContexto.Profile.ToListAsync();
            }

            return profile;
        }
        internal static IQueryable<Profile> QuerySelect(IQueryable<Profile> query, Profile profile)
        {
            if (profile.Id > 0)
            {
                query = query.Where(c => c.Id == profile.Id);
            }
            if (profile.IdUser > 0)
            {
                query = query.Where(c => c.IdUser == profile.IdUser);
            }

            return query;
        }

        public static async Task<List<Profile>> SearchAsync(Profile profile)
        {
            var profiles = new List<Profile>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Profile.AsQueryable();
                select = QuerySelect(select, profile);
                profiles = await select.ToListAsync();
            }
            return profiles;
        }
    
}
