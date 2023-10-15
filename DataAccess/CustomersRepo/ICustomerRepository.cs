using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomersRepo
{
    public interface ICustomerRepository
    {
        Customer Login(string email, string password);
        void Register(Customer customer);
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        List<Customer> GetAll();
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
