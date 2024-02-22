using YouCode.DAL;
using YouCode.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouCode.BL
{
    public class ReportBL
    {
        public async Task<int> CreateAsync(Report report)
        {
            return await ReportDAL.CreateAsync(report);
        }

        public async Task<int> UpdateAsync(Report report)
        {
            return await ReportDAL.UpdateAsync(report);
        }

        public async Task<int> DeleteAsync(Report report)
        {
            return await ReportDAL.DeleteAsync(report);
        }

        public async Task<Report> GetByIdAsync(Report report)
        {
            return await ReportDAL.GetByIdAsync(report);
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await ReportDAL.GetAllAsync();
        }

        public async Task<List<Report>> SearchAsync(Report report)
        {
            return await ReportDAL.SearchAsync(report);
        }
    }
}
