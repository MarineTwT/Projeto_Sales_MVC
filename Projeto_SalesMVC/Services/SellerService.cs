using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Models;
using Microsoft.EntityFrameworkCore;
using Projeto_SalesMVC.Services.Exceptions;

namespace Projeto_SalesMVC.Services
{
    public class SellerService
    {
        private readonly SalesMVCContext _context;

        public SellerService(SalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task insertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task updateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("ID not found...");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }

            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
