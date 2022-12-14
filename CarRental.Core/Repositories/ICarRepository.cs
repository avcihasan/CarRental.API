using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Repositories
{
    public interface ICarRepository:IGenericRepository<Car>
    {
        Task<List<Car>> GetCarsWithBrandAsync();
        Task<Car> GetCarWithBrandAndModelAsync(int id);
        Task<List<Car>> GetCarsWithAllPropertiesAsync();
        Task<Car> GetCarWithAllPropertiesByIdAsync(int id);
    }
}
