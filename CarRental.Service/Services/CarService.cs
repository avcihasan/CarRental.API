﻿using AutoMapper;
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
    public class CarService : Service<Car>, ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(IGenericRepository<Car> repository, IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper) : base(repository, unitOfWork)
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

        public async Task<CustomResponseDto<List<CarWithBrandDto>>> GetCarsWithBrandAsync()
        {
            
            var cars = await _carRepository.GetCarsWithBrandAsync();
            var carsDto= _mapper.Map<List<CarWithBrandDto>>(cars);
            return CustomResponseDto<List<CarWithBrandDto>>.Success(200,carsDto);
        }
    }
}