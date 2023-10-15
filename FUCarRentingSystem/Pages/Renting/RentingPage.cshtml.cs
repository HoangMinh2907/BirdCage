using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.RentingTransactionsRepo;
using DataAccess.RentingDetailsRepo;

namespace FUCarRentingSystem.Pages.RentingTransactioned
{
    public class RentingPageModel : PageModel
    {
        private readonly IRentingTransRepository _rentingTransRepository;

        public RentingPageModel(IRentingTransRepository rentingTransRepository)
        {
            _rentingTransRepository = rentingTransRepository;
        }

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string Role = HttpContext.Session.GetString("Role");
            if (Role == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if (Role == "Customer")
            {
                return RedirectToPage("/CustomerService/Information");
            }

            RentingTransaction = _rentingTransRepository.GetAll();
            return Page();
        }
    }
}
