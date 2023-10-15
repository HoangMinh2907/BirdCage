using BusinessObject.Models;
using DataAccess.CustomersRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUCarRentingSystem.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public LoginPageModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public Customer customer { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (customer is null)
            {
                ViewData["Warning"] = "Login please!";
                return Page();   
            }

            if (customer.Email is null || customer.Password is null)
            {
                ViewData["Warning"] = "Please fill your Email and Password!";
                return Page();
            }

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string emailAdmin = config["AdminAccount:email"];
            string passwordAdmin = config["AdminAccount:password"];
            if (emailAdmin == customer.Email && passwordAdmin == customer.Password)
            {
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToPage("/Admin/HomePage");
            }

            var customerAccount = _customerRepository.Login(customer.Email, customer.Password);
            if(customerAccount is not null)
            {
                HttpContext.Session.SetString("Role", "Customer");

                HttpContext.Session.SetString("ID", customerAccount.CustomerId.ToString());
                return RedirectToPage("/CustomerService/CustomerPage");
            }

            ViewData["Warning"] = "Account does not exists!";
            return Page();
        }
    }
}