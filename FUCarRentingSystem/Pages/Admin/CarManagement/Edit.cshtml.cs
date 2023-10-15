using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.SupplierRepository;
using DataAccess.CarInformationsRepo;
using DataAccess.ManufacturersRepo;

namespace FUCarRentingSystem.Pages.Admin.CarManagement
{
    public class EditModel : PageModel
    {
        private readonly ICarInforRepository _carInforRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplyRepository _supplyRepository;

        public EditModel(ICarInforRepository carInforRepository, IManufacturerRepository manufacturerRepository, ISupplyRepository supplyRepository)
        {
            _carInforRepository = carInforRepository;
            _manufacturerRepository = manufacturerRepository;
            _supplyRepository = supplyRepository;
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
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(_supplyRepository.GetAll(), "SupplierId", "SupplierName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(_supplyRepository.GetAll(), "SupplierId", "SupplierName");
           
            CarInformation.CarStatus = 1;

            if (CarInformation.NumberOfDoors < 2 || CarInformation.SeatingCapacity < 2)
            {
                ViewData["Warning"] = "Number of Door and Seating higher than 2";
                return Page();
            }

            _carInforRepository.Update(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
