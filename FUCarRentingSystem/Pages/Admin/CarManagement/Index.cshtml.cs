using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.CarInformationsRepo;

namespace FUCarRentingSystem.Pages.Admin.CarManagement
{
    public class IndexModel : PageModel
    {
        private readonly ICarInforRepository _carInforRepository;

        public IndexModel(ICarInforRepository carInforRepository)
        {
            _carInforRepository = carInforRepository;
        }

        public IList<CarInformation> CarInformation { get;set; } = default!;

        public async Task<IActionResult> OnGet()
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

            CarInformation = _carInforRepository.GetAll();
            return Page();
        }
    }
}
