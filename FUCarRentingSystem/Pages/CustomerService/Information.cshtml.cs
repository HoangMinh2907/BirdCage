using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomersRepo;

namespace FUCarRentingSystem.Pages.CustomerService
{
    public class InformationModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public InformationModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Customer { get; set; } = default!;
        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Role = HttpContext.Session.GetString("Role");
            if (Role == null)
            {
                return RedirectToPage("/LoginPage");
            }

            if (Role == "Customer")
            {
                int id = int.Parse(HttpContext.Session.GetString("ID"));
                Customer = _customerRepository.GetCustomerById(id);
            }
            return Page();
        }
    }
}
