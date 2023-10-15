using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CustomersRepo;

namespace FUCarRentingSystem.Pages.Admin.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public EditModel(ICustomerRepository customerRepository)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Customer.Telephone.Length != 10)
            {
                ViewData["Warning"] = "The phone number must have 10 letters!";
                return Page();
            }

            var customerData = _customerRepository.GetCustomerByEmail(Customer.Email);
            if (customerData is not null)
            {
                if(customerData.CustomerId != Customer.CustomerId)
                {
                    ViewData["Warning"] = "The email has been used!";
                    return Page();
                }
            }

            if (!Customer.Email.Contains("@"))
            {
                ViewData["Warning"] = "The email is wrong format!";
                return Page();
            }
            Customer.CustomerStatus = 1;

            _customerRepository.Update(Customer);

            return RedirectToPage("./Index");
        }
    }
}
