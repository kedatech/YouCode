using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class VideoBL
    {
        public async Task<int> CreateAsync(Video video)
        {
            return await VideoDAL.CreateAsync(video);
        }

        public async Task<int> UpdateAsync(Video video)
        {
            return await VideoDAL.UpdateAsync(video);
        }

        public async Task<int> DeleteAsync(Video video)
        {
            return await VideoDAL.DeleteAsync(video);
        }

        public async Task<Video> GetByIdAsync(Video video)
        {
            return await VideoDAL.GetByIdAsync(video);
        }

        public async Task<List<Video>> GetAllAsync()
        {
            return await VideoDAL.GetAllAsync();
        }

        public async Task<List<Video>> SearchAsync(Video video)
        {
            return await VideoDAL.SearchAsync(video);
        }
    }
}
