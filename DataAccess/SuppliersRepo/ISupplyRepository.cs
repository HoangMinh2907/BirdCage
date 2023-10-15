using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SupplierRepository
{
    public interface ISupplyRepository
    {
        List<Supplier> GetAll();
        Supplier GetSupplyById(int id);
    }
}
