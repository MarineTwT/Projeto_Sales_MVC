using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_SalesMVC.Models;

namespace Projeto_SalesMVC.Data
{
    public class SalesMVCContext : DbContext
    {
        public SalesMVCContext (DbContextOptions<SalesMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<SalesRecord> salesRecord { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
    }
}
