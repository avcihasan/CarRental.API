using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.DTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Core.DTOs.RentalDTOs;

namespace CarRental.Core.Services
{
    public interface IUserCarRentalService:IService<UserCarRental>
    {
        Task<UserCarRental> RentalAsync(UserCarRental userCarRental);

        Task<CustomResponseDto<List<RentalDetailsDto>>> GetAllRentalDetailsAsync();
    }
}
