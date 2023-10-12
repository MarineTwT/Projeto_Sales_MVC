namespace Projeto_SalesMVC.Services.Exceptions
{
    public class DBConcurrencyException : ApplicationException
    {
        public DBConcurrencyException(string message) : base(message)
        {

        }
    }
}
