using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.RentingDetailsRepo;
using DataAccess.RentingTransactionsRepo;

namespace FUCarRentingSystem.Pages.Renting
{
    public class RentingDetailModel : PageModel
    {
        private readonly IRentingTransRepository _transactionRepository;
        private readonly IRentingDetailRepository _rentingDetailRepository;

        public RentingDetailModel(IRentingTransRepository transactionRepository, IRentingDetailRepository rentingDetailRepository)
        {
            _transactionRepository = transactionRepository;
            _rentingDetailRepository = rentingDetailRepository;
        }

        public IList<RentingDetail> RentingDetail { get; set; } = default!;
        public RentingTransaction Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return RedirectToPage("./RentingPage");
            }
            Transaction = _transactionRepository.GetRentingTransactionById(id);
            RentingDetail = _rentingDetailRepository.GetAllByTransactionId(id);
            if (Transaction is null || RentingDetail is null)
            {
                return RedirectToPage("./RentingPage");
            }
            return Page();
        }
    }
}
