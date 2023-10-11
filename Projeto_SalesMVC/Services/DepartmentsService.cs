using Projeto_SalesMVC.Data;
using Projeto_SalesMVC.Models;
using System.Linq;

namespace Projeto_SalesMVC.Services
{
    public class DepartmentsService
    {
        private readonly SalesMVCContext _context;

        public DepartmentsService(SalesMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
