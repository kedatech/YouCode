using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class ImageBL
    {
        public async Task<int> CreateAsync(Image image)
        {
            return await ImageDAL.CreateAsync(image);
        }

        public async Task<int> UpdateAsync(Image image)
        {
            return await ImageDAL.UpdateAsync(image);
        }

        public async Task<int> DeleteAsync(Image image)
        {
            return await ImageDAL.DeleteAsync(image);
        }

        public async Task<int> DeleteOnAllPostAsync(int IdPost)
        {
            return await ImageDAL.DeleteOnAllPostAsync(IdPost);
        }

        public async Task<Image> GetByIdAsync(Image image)
        {
            return await ImageDAL.GetByIdAsync(image); 
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await ImageDAL.GetAllAsync();
        }

        public async Task<List<Image>> GetAllOnAPostAsync(int IdPost)
        {
            return await ImageDAL.GetAllOnAPostAsync(IdPost);
        }

        public async Task<List<Image>> SearchAsync(Image image)
        {
            return await ImageDAL.SearchAsync(image);
        }
    }
}
