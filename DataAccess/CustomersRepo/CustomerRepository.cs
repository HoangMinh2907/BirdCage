using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomersRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public CustomerRepository(FUCarRentingManagementContext context) 
        {
            _context = context;
        }
        public void Delete(Customer customer)
        {
            customer.CustomerStatus = 0;
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            var listAllCustomer = _context.Customers.ToList();
            var listActiveCustomer = new List<Customer>();
            foreach (var customer in listAllCustomer)
            {
                if(customer.CustomerStatus == 1)
                {
                    listActiveCustomer.Add(customer);
                }
            }
            return listActiveCustomer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email && x.CustomerStatus == 1);
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.CustomerId == id && x.CustomerStatus == 1);
        }

        public Customer Login(string email, string password)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email && x.Password == password && x.CustomerStatus == 1);
        }

        public void Register(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}
