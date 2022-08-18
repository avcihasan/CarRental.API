using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Repositories
{
    public interface IUserCarRentalRepository:IGenericRepository<UserCarRental>
    {
        Task RentalAsync(UserCarRental userCarRental);
        Task<List<UserCarRental>> GetAllRentalDetailsAsync();
        
    }
}
