using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CarInformationsRepo
{
    public class CarInforRepository : ICarInforRepository
    {
        private readonly FUCarRentingManagementContext _context;

        public CarInforRepository(FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public void Add(CarInformation carInformation)
        {
            _context.CarInformations.Add(carInformation);
            _context.SaveChanges();
        }

        public void Delete(CarInformation carInformation)
        {
            _context.CarInformations.Remove(carInformation);
            _context.SaveChanges();
        }

        public List<CarInformation> GetAll()
        {
            var listAllCar = _context.CarInformations.Include(c => c.Manufacturer).Include(c => c.Supplier).ToList();
            var listActiveCar = new List<CarInformation>();
            foreach (var car in listAllCar)
            {
                if(car.CarStatus == 1)
                {
                    listActiveCar.Add(car);
                }
            }
            return listActiveCar;
        }

        public CarInformation GetCarById(int id)
        {
            return _context.CarInformations.Include(c => c.Manufacturer).Include(c => c.Supplier).FirstOrDefault(x => x.CarId == id && x.CarStatus == 1);
        }

        public void Update(CarInformation carInformation)
        {
            _context.CarInformations.Update(carInformation);
            _context.SaveChanges();
        }
    }
}
