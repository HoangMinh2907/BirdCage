using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ManufacturersRepo
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetAll();
        Manufacturer GetByManufacturerId(int id);
    }
}
