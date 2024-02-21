using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCode.BE;

namespace YouCode.DAL
{
    internal class UserDAL
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
        //Busca el usuario 
        public static async Task<int> UpdateAsync(User user)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var userDB = await bdContexto.User.FirstOrDefaultAsync(c => c.Id == user.Id);
                if (userDB != null)
                {
                    userDB.Name = user.Name;
                    bdContexto.Update(userDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        // eliminar un usuario 
        public static async Task<int> DeletAsync(User user)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var userDB = await bdContexto.User.FirstOrDefaultAsync(c => c.Id == user.Id);
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
                userDB = await bdContexto.User.FirstOrDefaultAsync(c => c.Id == user.Id);
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

            query = query.OrderByDescending(c => c.Id);
            if (user.Top_Aux > 0)
            {
                query = query.Take(user.Top_Aux).AsQueryable();
            }
            return query;
        }

    }
}
