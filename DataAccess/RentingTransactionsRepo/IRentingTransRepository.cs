using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RentingTransactionsRepo
{
    public interface IRentingTransRepository
    {
        List<RentingTransaction> GetAll();
        RentingTransaction GetRentingTransactionById(int id);
        void Add(RentingTransaction transaction);   
    }
}
