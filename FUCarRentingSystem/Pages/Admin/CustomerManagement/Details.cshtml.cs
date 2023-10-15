using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomersRepo;

namespace FUCarRentingSystem.Pages.Admin.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DetailsModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            string Role = HttpContext.Session.GetString("Role");
            if (Role == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if (Role == "Customer")
            {
                return RedirectToPage("/CustomerService/CustomerPage");
            }

            if (id == null)
            {
                return RedirectToPage("./Index");
            }

            Customer = _customerRepository.GetCustomerById(id);
            if (Customer == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
