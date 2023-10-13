using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Packaging.Signing;
using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Models;

namespace Projeto_SalesMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesMVCContext _context;

        public SalesRecordService(SalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result.
                Include(x => x.Seller).
                Include(x => x.Seller.Department).
                OrderByDescending(x => x.Date).
                ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var query = from obj in _context.SalesRecord
                        where (!minDate.HasValue || obj.Date >= minDate) &&
                              (!maxDate.HasValue || obj.Date <= maxDate)
                        select obj;

            var records = await query
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return records.AsEnumerable()
                .GroupBy(x => x.Seller.Department)
                .ToList();
        }

    }
}
