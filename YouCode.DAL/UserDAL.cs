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
    }
}
