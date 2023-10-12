using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projeto_SalesMVC.Services
{
    public class DepartmentsService
    {
        private readonly SalesMVCContext _context;

        public DepartmentsService(SalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
