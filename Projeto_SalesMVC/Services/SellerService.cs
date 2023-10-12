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

        public List<Seller> FindAll() 
        {
            return _context.Seller.ToList();
        }

        public void insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("ID not found...");
            }

            else
            {
                try
                {
                    _context.Update(obj);
                    _context.SaveChanges();
                }

                catch(DBConcurrencyException e)
                {
                    throw new DBConcurrencyException(e.Message);
                }
            }
        }
    }
}
