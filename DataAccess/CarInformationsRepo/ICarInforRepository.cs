using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CarInformationsRepo
{
    public interface ICarInforRepository
    {
        CarInformation GetCarById(int id);
        List<CarInformation> GetAll();
        void Add(CarInformation carInformation);
        void Update(CarInformation carInformation);
        void Delete(CarInformation carInformation);
    }
}
