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
    public class DetailsModel : PageModel
    {
        private readonly ICarInforRepository _carInforRepository;

        public DetailsModel(ICarInforRepository carInforRepository)
        {
            _carInforRepository = carInforRepository;
        }

        public CarInformation CarInformation { get; set; } = default!;

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

            CarInformation = _carInforRepository.GetCarById(id);
            if(CarInformation == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
