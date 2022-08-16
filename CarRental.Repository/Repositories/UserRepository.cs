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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersWithCarsAsync()
        {
            return await _context.Users.Include(x => x.Car)
                .Include(x => x.Car.Brand)
                .Include(x => x.Car.Model)
                .Include(x => x.Car.ModelYear)
                .Include(x => x.Car.Color)
                .Include(x => x.Car.)
                .ToListAsync();
        }
    }
}
