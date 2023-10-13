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

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate,DateTime? maxDate) 
        {
            var result = from obj in _context.salesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if(maxDate.HasValue) 
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result.
                Include(x => x.Seller).
                Include(x => x.Seller.Department).
                OrderByDescending(x => x.Date).
                ToListAsync();
        }
    }
}
