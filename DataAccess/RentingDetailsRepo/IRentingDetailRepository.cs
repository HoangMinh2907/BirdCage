using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RentingDetailsRepo
{
    public interface IRentingDetailRepository
    {
        List<RentingDetail> GetAll();
        List<RentingDetail> GetAllByTransactionId(int id);
    }
}
