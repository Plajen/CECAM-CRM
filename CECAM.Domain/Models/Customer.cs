namespace CECAM.Domain.Models
{
    public class Customer : Entity
    {
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }

        public Customer(int id, string companyName, string cNPJ)
        {
            Id = id;
            CompanyName = companyName;
            CNPJ = cNPJ;
        }

        public Customer() { }
    }
}
