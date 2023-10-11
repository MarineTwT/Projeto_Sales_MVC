using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Models;

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


    }
}
