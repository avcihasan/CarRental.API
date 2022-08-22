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

        public async Task<CustomResponseDto<List<CarWithAllPropertiesDto>>> GetCarsWithAllPropertiesAsync()
        {
            var cars = await _carRepository.GetCarsWithAllPropertiesAsync();
            var carsDto = _mapper.Map<List<CarWithAllPropertiesDto>>(cars);
            return CustomResponseDto<List<CarWithAllPropertiesDto>>.Success(200, carsDto);
        }

        public async Task<CustomResponseDto<CarWithAllPropertiesDto>> GetCarWithAllPropertiesByIdAsync(int id)
        {
            var car = await _carRepository.GetCarWithAllPropertiesByIdAsync(id);
            var carDto = _mapper.Map<CarWithAllPropertiesDto>(car);
            return CustomResponseDto<CarWithAllPropertiesDto>.Success(200, carDto);
        }

        public async Task<CustomResponseDto<List<CarWithBrandDto>>> GetCarsWithBrandAsync()
        {
            
            var cars = await _carRepository.GetCarsWithBrandAsync();
            var carsDto= _mapper.Map<List<CarWithBrandDto>>(cars);
            return CustomResponseDto<List<CarWithBrandDto>>.Success(200,carsDto);
        }

        public async Task<CustomResponseDto<CarWithBrandAndModelDto>> GetCarWithBrandAndModelAsync(int id)
        {
            var car = await _carRepository.GetCarWithBrandAndModelAsync(id);
            var carDto = _mapper.Map<CarWithBrandAndModelDto>(car);
            return CustomResponseDto<CarWithBrandAndModelDto>.Success(200, carDto);
        }

       
    }
}
