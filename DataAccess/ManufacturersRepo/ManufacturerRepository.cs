using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ManufacturersRepo
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public ManufacturerRepository(FUCarRentingManagementContext context) 
        {
            _context = context;
        }
        public List<Manufacturer> GetAll()
        {
            return _context.Manufacturers.ToList();
        }

        public Manufacturer GetByManufacturerId(int id)
        {
            return _context.Manufacturers.FirstOrDefault(x => x.ManufacturerId == id);
        }
    }
}
