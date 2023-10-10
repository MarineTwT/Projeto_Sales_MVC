using Microsoft.EntityFrameworkCore;
using Projeto_SalesMVC.Models;
using Projeto_SalesMVC.Models.Enums;

namespace Projeto_SalesMVC.Data
{
    public class SeedingService
    {
        private SalesMVCContext _Context;

        public SeedingService(SalesMVCContext context) 
        {
            _Context = context;
        }

        public void Seed()
        {
            if( _Context.Department.Any() || _Context.Seller.Any() || _Context.salesRecord.Any()) 
            {
                return; //DB has been seeded
            }

                Department d1 = new Department("Computers");
                Department d2 = new Department("Electronics");   
                Department d3 = new Department("Fashion");
                Department d4 = new Department("Books");

                Seller s1 = new Seller("Bob Teddol", "bobt@gmail.com", new DateTime(1998, 4, 21), 1000.0,d1);
                Seller s2 = new Seller("Mary Lelele", "maryl@gmail.com", new DateTime(2000, 5, 10), 1200.0, d2);
                Seller s3 = new Seller("Ted Lelele", "tedl@gmail.com", new DateTime(2001, 2, 6), 900.0, d3);
                Seller s4 = new Seller("Tonny Philips", "tonnyp@gmail.com", new DateTime(1987, 6, 10), 1500.0, d4);
                Seller s5 = new Seller("Alex Deada", "alexd@gmail.com", new DateTime(2003, 9, 10), 1200.0, d1);
                Seller s6 = new Seller("Palals Lololo", "palalsl@gmail.com", new DateTime(2000, 8, 10), 1100.0, d3);

                SalesRecord r1 = new SalesRecord(new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1);
                SalesRecord r2 = new SalesRecord(new DateTime(2018, 09, 4), 7000.0, SaleStatus.Billed, s5);
                SalesRecord r3 = new SalesRecord(new DateTime(2018, 09, 13), 4000.0, SaleStatus.Canceled, s4);
                SalesRecord r4 = new SalesRecord(new DateTime(2018, 09, 1), 8000.0, SaleStatus.Billed, s1);
                SalesRecord r5 = new SalesRecord(new DateTime(2018, 09, 21), 3000.0, SaleStatus.Billed, s3);
                SalesRecord r6 = new SalesRecord(new DateTime(2018, 09, 15), 2000.0, SaleStatus.Billed, s1);
                SalesRecord r7 = new SalesRecord(new DateTime(2018, 09, 28), 13000.0, SaleStatus.Billed, s2);
                SalesRecord r8 = new SalesRecord(new DateTime(2018, 09, 11), 4000.0, SaleStatus.Billed, s4);
                SalesRecord r9 = new SalesRecord(new DateTime(2018, 09, 14), 11000.0, SaleStatus.Pending, s6);
                SalesRecord r10 = new SalesRecord(new DateTime(2018, 09, 7), 9000.0, SaleStatus.Billed, s6);
                SalesRecord r11 = new SalesRecord(new DateTime(2018, 09, 13), 6000.0, SaleStatus.Billed, s2);
                SalesRecord r12 = new SalesRecord(new DateTime(2018, 09, 25), 7000.0, SaleStatus.Pending, s3);
                SalesRecord r13 = new SalesRecord(new DateTime(2018, 09, 29), 10000.0, SaleStatus.Billed, s4);
                SalesRecord r14 = new SalesRecord(new DateTime(2018, 09, 4), 3000.0, SaleStatus.Billed, s5);
                SalesRecord r15 = new SalesRecord(new DateTime(2018, 09, 12), 4000.0, SaleStatus.Billed, s1);
                SalesRecord r16 = new SalesRecord(new DateTime(2018, 10, 5), 2000.0, SaleStatus.Billed, s4);
                SalesRecord r17 = new SalesRecord(new DateTime(2018, 10, 1), 12000.0, SaleStatus.Billed, s1);
                SalesRecord r18 = new SalesRecord(new DateTime(2018, 10, 24), 6000.0, SaleStatus.Billed, s3);
                SalesRecord r19 = new SalesRecord(new DateTime(2018, 10, 22), 8000.0, SaleStatus.Billed, s5);
                SalesRecord r20 = new SalesRecord(new DateTime(2018, 10, 15), 8000.0, SaleStatus.Billed, s6);
                SalesRecord r21 = new SalesRecord(new DateTime(2018, 10, 17), 9000.0, SaleStatus.Billed, s2);
                SalesRecord r22 = new SalesRecord(new DateTime(2018, 10, 24), 4000.0, SaleStatus.Billed, s4);
                SalesRecord r23 = new SalesRecord(new DateTime(2018, 10, 19), 11000.0, SaleStatus.Canceled, s2);
                SalesRecord r24 = new SalesRecord(new DateTime(2018, 10, 12), 8000.0, SaleStatus.Billed, s5);
                SalesRecord r25 = new SalesRecord(new DateTime(2018, 10, 31), 7000.0, SaleStatus.Billed, s3);
                SalesRecord r26 = new SalesRecord(new DateTime(2018, 10, 6), 5000.0, SaleStatus.Billed, s4);
                SalesRecord r27 = new SalesRecord(new DateTime(2018, 10, 13), 9000.0, SaleStatus.Pending, s1);
                SalesRecord r28 = new SalesRecord(new DateTime(2018, 10, 7), 4000.0, SaleStatus.Billed, s3);
                SalesRecord r29 = new SalesRecord(new DateTime(2018, 10, 23), 12000.0, SaleStatus.Billed, s5);
                SalesRecord r30 = new SalesRecord(new DateTime(2018, 10, 12), 5000.0, SaleStatus.Billed, s2);

                _Context.Department.AddRange(d1, d2, d3, d4);

                _Context.Seller.AddRange(s1, s2, s3, s4, s5, s6);

                _Context.salesRecord.AddRange(
                    r1, r2, r3, r4, r5, r6, r7, r8, r9, r10,
                    r11, r12, r13, r14, r15, r16, r17, r18, r19, r20,
                    r21, r22, r23, r24, r25, r26, r27, r28, r29, r30
                );

                _Context.SaveChanges();            
        }
    }
}
