using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouCode.BE;
using static System.Net.Mime.MediaTypeNames;

namespace YouCode.DAL
{
    public class ImageDAL
    {
        //guarda una imagen
        public static async Task<int> CreateAsync(BE.Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(image);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        //actualiza una entidad
        public static async Task<int> UpdateAsync(BE.Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var imageDB = await bdContexto.Image.FirstOrDefaultAsync(i => i.Id == image.Id);
                if (imageDB != null)
                {
                    imageDB.Id = image.Id;
                    imageDB.Path = image.Path;
                    bdContexto.Update(imageDB);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }
        //Elimina
        public static async Task<int> DeleteAsync(BE.Image image)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var imageDB = await bdContexto.Image.FirstOrDefaultAsync(i => i.Id == image.Id);
                if (imageDB != null)
                {
                    bdContexto.Image.Remove(imageDB);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<BE.Image> GetByIdAsync(BE.Image image)
        {
            var imageDB = new BE.Image();
            using (var bdContexto = new ContextoDB())
            {
                imageDB = await bdContexto.Image.FirstOrDefaultAsync(i => i.Id == image.Id);
            }
            return imageDB;
        }
        public static async Task<List<BE.Image>> GetAllAsync()
        {
            var images = new List<BE.Image>();
            using (var bdContexto = new ContextoDB())
            {
                images = await bdContexto.Image.ToListAsync();

                return images;
            }

        }
        // este método permite filtrar y ordenar consultas de entidades BE.Image
        internal static IQueryable<BE.Image> QuerySelect(IQueryable<BE.Image> query, BE.Image image)
        {
            if (image.IdPost > 0)
                query = query.Where(s => s.Id == image.Id);
            if (image.IdAdd > 0)
                query = query.Where(s => s.IdAdd == image.IdAdd);
            if (!string.IsNullOrWhiteSpace(image.Path))
                query = query.Where(s => s.Path.Contains(image.Path));

            query = query.OrderByDescending(s => s.Id).AsQueryable();

            if (image.Top_Aux > 0)
                query = query.Take(image.Top_Aux).AsQueryable();

            return query;
        }
       // permite buscar imágenes en la base de datos
        public static async Task<List<BE.Image>> SearchAsync(BE.Image image)
        {
            var images = new List<BE.Image>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Image.AsQueryable();
                select = QuerySelect(select, image);
                images = await select.ToListAsync();
            }
            return images;
        }
        public static async Task<List<BE.Image>> SearchIncludeAdAsync(BE.Image image)
        {
            var images = new List<BE.Image>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Image.AsQueryable();
                select = QuerySelect(select, image);
                images = await select.ToListAsync();
            }
            return images;
        }

    }
}
