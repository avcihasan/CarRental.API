using CarRental.Core.Models;
using CarRental.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repository.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
      

        public CarRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<List<Car>> GetCarsWithAllPropertiesAsync()
        {
            return await _context.Cars.Include(x => x.Brand)
                .Include(x => x.Color)
                .Include(x => x.Engine)
                .Include(x => x.Model)
                .Include(x => x.ModelYear).ToListAsync();
        }

        public async Task<List<Car>> GetCarsWithBrandAsync()
        {
            return await _context.Cars.Include(x => x.Brand).ToListAsync();
        }
    }
}
