using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RentingDetailsRepo
{
    public class RentingDetailRepository : IRentingDetailRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public RentingDetailRepository(FUCarRentingManagementContext context)
        {
            _context = context;
        }
        public List<RentingDetail> GetAll()
        {
            return _context.RentingDetails.Include(c => c.Car).Include(c => c.RentingTransaction).ToList();
        }

        public List<RentingDetail> GetAllByTransactionId(int id)
        {
            var listAllDetail = _context.RentingDetails.Include(c => c.Car).Include(c => c.RentingTransaction).ToList();
            var list = new List<RentingDetail>();
            foreach (var detail in listAllDetail)
            {
                if (detail.RentingTransactionId == id)
                {
                    list.Add(detail);
                }
            }
            return list;
        }
    }
}
