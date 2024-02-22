using Microsoft.EntityFrameworkCore;
using YouCode.BE;

namespace YouCode.DAL;

public class ReportDAL
{
    public static async Task<int> CreateAsync(Report report)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                bdContexto.Add(report);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Report report)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var reportDB = await bdContexto.Report.FirstOrDefaultAsync(c => c.Id == report.Id);
                if (reportDB != null)
                {
                    reportDB.Id = report.Id;
                    bdContexto.Update(reportDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }

        }
        public static async Task<int> DeleteAsync(Report report)
        {
            int result = 0;
            using (var bdContexto = new ContextoDB())
            {
                var reportDB = await bdContexto.Report.FirstOrDefaultAsync(c => c.Id == report.Id);
                if (reportDB != null)
                {
                    bdContexto.Report.Remove(reportDB);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
        }
        public static async Task<Report> GetByIdAsync(Report report)
        {
            var reportDB = new Report();
            using (var bdContexto = new ContextoDB())
            {
                reportDB = await bdContexto.Report.FirstOrDefaultAsync(c => c.Id == report.Id);
            }
            return reportDB;
        }
        public static async Task<List<Report>> GetAllAsync()
        {
            var report = new List<Report>();
            using (var bdContexto = new ContextoDB())
            {
                report = await bdContexto.Report.ToListAsync();
            }

            return report;
        }
        internal static IQueryable<Report> QuerySelect(IQueryable<Report> query, Report report)
        {
            if (report.Id > 0)
            {
                query = query.Where(c => c.Id == report.Id);
            }
            if (report.IdUser > 0)
            {
                query = query.Where(c => c.IdUser == report.IdUser);
            }
            if (!string.IsNullOrWhiteSpace(report.Type))
            {
                query = query.Where(c => c.Type.Contains(report.Type));
            }

           // query = query.OrderByDescending(c => c.Id);
            
            return query;
        }

        public static async Task<List<Report>> SearchAsync(Report report)
        {
            var reports = new List<Report>();
            using (var bdContexto = new ContextoDB())
            {
                var select = bdContexto.Report.AsQueryable();
                select = QuerySelect(select, report);
                reports = await select.ToListAsync();
            }
            return reports;
        }
    
}
