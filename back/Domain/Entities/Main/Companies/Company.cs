using System.Collections.Generic;
using Domain.Entities.Authentication;
using Domain.Entities.Main.Customers;
using Domain.Entities.Shared;

namespace Domain.Entities.Main.Companies
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
        public List<MyUser> MyUsers { get; set; }
        public List<Customer> Customers { get; set; }
    
    }


}
