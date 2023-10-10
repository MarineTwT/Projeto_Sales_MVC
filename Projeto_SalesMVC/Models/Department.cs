namespace Projeto_SalesMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public Department() { }

        public Department(string name) // Remova o parâmetro 'id' do construtor
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double Total_Sales(DateTime Initial, DateTime Final)
        {
            return Sellers.Sum(seller => seller.Total_Sales(Initial, Final));
        }
    }
}
