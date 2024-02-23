using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class ImageDAL
{
    public static async Task<int> CreateAsync(Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(image);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var imageDB = await bdContexto.Image.FirstOrDefaultAsync(c => c.Id == image.Id);
                if (imageDB != null)
                {
                    imageDB.Id = image.Id;
                    bdContexto.Update(imageDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var imageDB = await bdContexto.Image.FirstOrDefaultAsync(c => c.Id == image.Id);
                if (imageDB != null)
                {
                    bdContexto.Image.Remove(imageDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Image> GetByIdAsync(Image image)
        {
            var imageDB = new Image();
            using (var bdContexto = new ContextoDB())
            {
                imageDB = await bdContexto.Image.FirstOrDefaultAsync(c => c.Id == image.Id);
            }
            return imageDB;
        }
        public static async Task<List<Image>> GetAllAsync()
        {
            var image = new List<Image>();
            using (var bdContexto = new ContextoDB())
            {
                image = await bdContexto.Image.ToListAsync();
            }

            return image;
        }
        internal static IQueryable<Image> QuerySelect(IQueryable<Image> query, BE.Image image)
        {
            if (image.Id > 0)
            {
                query = query.Where(c => c.Id == image.Id);
            }
            if (image.IdPost > 0)
            {
                query = query.Where(c => c.Id == image.IdPost);
            }
            if (!string.IsNullOrWhiteSpace(image.Path))
            {
                query = query.Where(c => c.Path.Contains(image.Path));
            }

            query = query.OrderByDescending(c => c.Id);
            if (image.Top_Aux > 0)
            {
                query = query.Take(image.Top_Aux).AsQueryable();
            }
            return query;
        }

        public static async Task<List<Image>> SearchAsync(Image image)
        {
            var images = new List<Image>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Image.AsQueryable();
                select = QuerySelect(select, image);
                images = await select.ToListAsync();
            }
            return images;
        }
    
}
