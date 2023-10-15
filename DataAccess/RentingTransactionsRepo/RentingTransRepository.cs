using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RentingTransactionsRepo
{
    public class RentingTransRepository : IRentingTransRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public RentingTransRepository(FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public void Add(RentingTransaction transaction)
        {
            _context.RentingTransactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<RentingTransaction> GetAll()
        {   
            var listAllRenting = _context.RentingTransactions.Include(c => c.Customer).ToList();
            var listActive = new List<RentingTransaction>();
            foreach (var renting in listAllRenting)
            {
                if(renting.RentingStatus == 1)
                {
                    listActive.Add(renting);
                }
            }
            return listActive;
        }

        public RentingTransaction GetRentingTransactionById(int id)
        {
            return _context.RentingTransactions.Include(c => c.Customer).FirstOrDefault(x => x.RentingTransationId == id && x.RentingStatus == 1);
        }
    }
}
