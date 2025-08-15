using Domain.Entities.Customers;
using Domain.Entities.Shared;

namespace Domain.Entities.Companies
{
    public class Company
    {
        public Company()
        { }
        public Company(string name)
        {
            Name = name;
        }
        public Company(int id, string name)
        {
            Name = name;
            Id = id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public bool Deleted { get; set; }
        public List<Customer> Customers { get; set; }
    
    }


}
