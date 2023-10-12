using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Projeto_SalesMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required"), StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string  Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {2} to {1}")]
        [Display(Name = "Base Salary"), DataType(DataType.Currency)]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void Add_Sales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void Remove_Sales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double Total_Sales(DateTime Initial,DateTime Final)
        {
            return Sales.Where(sr => sr.Date >= Initial && sr.Date <= Final).Sum(sr => sr.Amount);
        }
    }
}
