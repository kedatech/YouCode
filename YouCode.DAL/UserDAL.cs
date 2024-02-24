using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCode.BE;

namespace YouCode.DAL
{
    public class UserDAL
    {
        //agregar un nuevo usuario a la base de datos 
        public static async Task<int> CreateAsync(User user)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(user);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        //actualiza el usuario 
        public static async Task<int> UpdateAsync(User user)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var userDB = await bdContexto.User.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (userDB != null)
                {
                    userDB.Id = user.Id;
                    userDB.Name = user.Name;
                    userDB.Username = user.Username;
                    userDB.Email = user.Email;
                    userDB.Password = user.Password;
                    userDB.CreatedAt = user.CreatedAt;
                    
                    bdContexto.Update(userDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        // eliminar un usuario 
        public static async Task<int> DeleteAsync(User user)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var userDB = await bdContexto.User.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (userDB != null)
                {
                    bdContexto.User.Remove(userDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        // para obtener un usuario
        public static async Task<User> GetByIdAsync(User user)
        {
            var userDB = new User();
            using (var bdContexto = new ContextoDB())
            {
                userDB = await bdContexto.User.FirstOrDefaultAsync(u => u.Id == user.Id);
            }
            return userDB;
        }
        //obtener todos los usuarios de la base de datos
        public static async Task<List<User>> GetAllAsync()
        {
            var user = new List<User>();
            using (var bdContexto = new ContextoDB())
            {
                user = await bdContexto.User.ToListAsync();
            }

            return user;
        }

        public static async Task<List<User>> SearchAsync(User user)
        {
            List<User> users = new List<User>();
            using(var ContextDb = new ContextoDB())
            {
                var select = ContextDb.User.AsQueryable();
                select = QuerySelect(select, user);
                users = await select.ToListAsync();
            }
            return users;
        }

        internal static IQueryable<User> QuerySelect(IQueryable<User> query, User user)
        {
            if (user.Id > 0)
            {
                query = query.Where(c => c.Id == user.Id);
            }
            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                query = query.Where(c => c.Name.Contains(user.Name));
            }
            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                query = query.Where(c => c.Username.Contains(user.Username));
            }

            query = query.OrderByDescending(c => c.Id);
            if (user.Top_Aux > 0)
            {
                query = query.Take(user.Top_Aux).AsQueryable();
            }
            return query;
        }

    }
}
