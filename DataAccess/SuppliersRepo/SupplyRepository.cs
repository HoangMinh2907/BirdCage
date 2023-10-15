using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SupplierRepository
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public SupplyRepository(FUCarRentingManagementContext context) 
        {
            _context = context;
        }
        public List<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplyById(int id)
        {
            return _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
        }
    }
}
