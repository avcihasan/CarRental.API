using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Service.Services
{
    public class CarServiceWithNoCaching : Service<Car>, ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarServiceWithNoCaching(IGenericRepository<Car> repository, IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<GetCarWithAllPropertiesDto>>> GetCarsWithAllPropertiesAsync()
        {
            var cars = await _carRepository.GetCarsWithAllPropertiesAsync();
            var carsDto = _mapper.Map<List<GetCarWithAllPropertiesDto>>(cars);
            return CustomResponseDto<List<GetCarWithAllPropertiesDto>>.Success(200, carsDto);
        }

        public async Task<CustomResponseDto<GetCarWithAllPropertiesDto>> GetCarWithAllPropertiesByIdAsync(int id)
        {
            var car = await _carRepository.GetCarWithAllPropertiesByIdAsync(id);
            var carDto = _mapper.Map<GetCarWithAllPropertiesDto>(car);
            return CustomResponseDto<GetCarWithAllPropertiesDto>.Success(200, carDto);
        }

        public async Task<CustomResponseDto<List<GetCarWithBrandDto>>> GetCarsWithBrandAsync()
        {
            
            var cars = await _carRepository.GetCarsWithBrandAsync();
            var carsDto= _mapper.Map<List<GetCarWithBrandDto>>(cars);
            return CustomResponseDto<List<GetCarWithBrandDto>>.Success(200,carsDto);
        }

        public async Task<CustomResponseDto<GetCarWithBrandAndModelDto>> GetCarWithBrandAndModelAsync(int id)
        {
            var car = await _carRepository.GetCarWithBrandAndModelAsync(id);
            var carDto = _mapper.Map<GetCarWithBrandAndModelDto>(car);
            return CustomResponseDto<GetCarWithBrandAndModelDto>.Success(200, carDto);
        }

       
    }
}
