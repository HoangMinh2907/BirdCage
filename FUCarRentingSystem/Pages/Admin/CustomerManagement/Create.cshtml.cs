using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.CustomersRepo;

namespace FUCarRentingSystem.Pages.Admin.CustomerPage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult OnGet()
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
            return Page();
        }

        [BindProperty]
        public Customer customerCreate { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid == null || customerCreate == null)
            {
                return Page();
            }

            if(_customerRepository.GetCustomerById(customerCreate.CustomerId) is not null)
            {
                ViewData["Warning"] = "This id has been used!";
                return Page();
            }

            if (customerCreate.Telephone.Length != 10)
            {
                ViewData["Warning"] = "The phone number must have 10 letters!";
                return Page();
            }

            if (_customerRepository.GetCustomerByEmail(customerCreate.Email) is not null)
            {
                ViewData["Warning"] = "The email has been used!";
                return Page();
            }
            if (!customerCreate.Email.Contains("@"))
            {
                ViewData["Warning"] = "The email is wrong format!";
                return Page();
            }

            customerCreate.CustomerStatus = 1;

            _customerRepository.Register(customerCreate);

            return RedirectToPage("./Index");
        }
    }
}
