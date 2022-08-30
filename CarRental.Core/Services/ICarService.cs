using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Services
{
    public interface ICarService:IService<Car>
    {
        Task<CustomResponseDto<List<GetCarWithBrandDto>>> GetCarsWithBrandAsync();
        Task<CustomResponseDto<GetCarWithBrandAndModelDto>> GetCarWithBrandAndModelAsync(int id);
        Task<CustomResponseDto<List<GetCarWithAllPropertiesDto>>> GetCarsWithAllPropertiesAsync();
        Task<CustomResponseDto<GetCarWithAllPropertiesDto>> GetCarWithAllPropertiesByIdAsync(int id);
    }
}
