using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.SupplierRepository;
using System.Data;
using DataAccess.CarInformationsRepo;
using DataAccess.ManufacturersRepo;

namespace FUCarRentingSystem.Pages.Admin.CarManagement
{
    public class CreateModel : PageModel
    {
        private readonly ICarInforRepository _carInforRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISupplyRepository _supplyRepository;

        public CreateModel(ICarInforRepository carInforRepository, IManufacturerRepository manufacturerRepository, ISupplyRepository supplyRepository)
        {
            _carInforRepository = carInforRepository;
            _manufacturerRepository = manufacturerRepository;
            _supplyRepository = supplyRepository;
        }

        public IActionResult OnGet()
        {
            string Role = HttpContext.Session.GetString("Role");
            if (Role == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if(Role == "Customer")
            {
                return RedirectToPage("/CustomerService/CustomerPage");
            }
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(_supplyRepository.GetAll(), "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetAll(), "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(_supplyRepository.GetAll(), "SupplierId", "SupplierName");
            if (!ModelState.IsValid == null || CarInformation == null)
            {
                return Page();
            }

            if(_carInforRepository.GetCarById(CarInformation.CarId) is not null)
            {
                ViewData["Warning"] = "This car id has been used";
                return Page();
            }

            if(CarInformation.NumberOfDoors < 2 || CarInformation.SeatingCapacity < 2)
            {
                ViewData["Warning"] = "Number of Door and Seating higher than 2";
                return Page();
            }
            CarInformation.CarStatus = 1;

            _carInforRepository.Add(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
