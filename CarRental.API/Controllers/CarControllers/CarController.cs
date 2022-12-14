using AutoMapper;
using CarRental.Caching;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.API.Controllers.CarControllers
{
    public class CarController : CustomBaseController
    {
        private readonly ICarService _service;
        private readonly IMapper _mapper;
        private readonly RedisService _redisService;


        public CarController(ICarService service, IMapper mapper, RedisService redisService)
        {
            _service = service;
            _mapper = mapper;
            _redisService = redisService;

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            
            var cars = await _service.GetAllAsync();
          
            var carsDto = _mapper.Map<List<GetCarDto>>(cars);
            return CreateActionResult(CustomResponseDto<List<GetCarDto>>.Success(200, carsDto.ToList()));

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _service.GetByIdAsync(id);
            if (car == null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Araba Bulunamadı!"));

            var carDto = _mapper.Map<GetCarDto>(car);
            return CreateActionResult(CustomResponseDto<GetCarDto>.Success(200, carDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SetCarDto carDto)
        {
            var cars = await _service.GetAllAsync();

            var car = _mapper.Map<Car>(carDto);
            car.IsRent = true;

            foreach (var item in cars)
            {
                if (item.LicensePlate==car.LicensePlate)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(403, "Araç Sistemde Kayıtlı!"));
                }
            }

            await _service.AddAsync(car);
            return CreateActionResult(CustomResponseDto<SetCarDto>.Success(201, carDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(SetCarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            await _service.UpdateAsync(car);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var car = await _service.GetByIdAsync(id);
            if (car==null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Araba Bulunamadı!"));
            }
            await _service.RemoveAsync(car);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarsWithBrand()
        {

            return CreateActionResult(await _service.GetCarsWithBrandAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarsWithAllProperties()
        {
            return CreateActionResult(await _service.GetCarsWithAllPropertiesAsync());
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCarWithAllPropertiesById(int id)
        {
            return CreateActionResult(await _service.GetCarWithAllPropertiesByIdAsync(id));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCarWithBrandAndModel(int id)
        {
            return CreateActionResult(await _service.GetCarWithBrandAndModelAsync(id));
        }

    }
}