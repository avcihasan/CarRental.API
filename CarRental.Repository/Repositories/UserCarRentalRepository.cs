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
    public class UserCarRentalRepository : GenericRepository<UserCarRental>, IUserCarRentalRepository
    {
        private readonly DbSet<UserCarRental> _dbSet;
        public UserCarRentalRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<UserCarRental>();
        }

        public async Task<List<UserCarRental>> GetAllRentalDetailsAsync()
        {
           
            return await _dbSet.Include(x => x.User)
                .Include(x=>x.Car)
                .ToListAsync();
        }

        public async Task RentalAsync(UserCarRental userCarRental)
        {

           var car= await _context.Cars.Where(x => x.Id == userCarRental.CarId).FirstOrDefaultAsync();
            car.IsRent = false;
            userCarRental.isDelivered = true;
            await _dbSet.AddAsync(userCarRental);

        }
    }
}
