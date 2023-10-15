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
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            Customer = _customerRepository.GetCustomerById(id);
            _customerRepository.Delete(Customer);

            return RedirectToPage("./Index");
        }
    }
}
