using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.SupplierRepository;
using DataAccess.CarInformationsRepo;

namespace FUCarRentingSystem.Pages.Admin.CarManagement
{
    public class DeleteModel : PageModel
    {
        private readonly ICarInforRepository _carInforRepository;

        public DeleteModel(ICarInforRepository carInforRepository)
        {
            _carInforRepository = carInforRepository;
        }

        [BindProperty]
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

            if (CarInformation == null)
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
            CarInformation = _carInforRepository.GetCarById(id);
            _carInforRepository.Delete(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
