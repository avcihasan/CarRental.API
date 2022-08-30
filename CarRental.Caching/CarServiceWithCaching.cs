using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Caching
{

    public class CarServiceWithCaching : ICarService
    {

        private const string CacheCarKey = "carsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICarRepository _repository;
        private readonly IUnitOfWork _unitOfwork;

        public CarServiceWithCaching(IMemoryCache memoryCache, ICarRepository repository, IUnitOfWork unitOfwork, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfwork = unitOfwork;
            _mapper = mapper;

            if (!_memoryCache.TryGetValue(CacheCarKey, out _))
            {
                _memoryCache.Set(CacheCarKey, _repository.GetCarsWithAllPropertiesAsync().Result);
            }
        }

        public async Task<Car> AddAsync(Car entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
            return entity;
        }

        public async Task<IEnumerable<Car>> AddRangeAsync(IEnumerable<Car> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
            return entities;
        }

    

        public Task<IEnumerable<Car>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Car>>(CacheCarKey));
        }

        public Task<Car> GetByIdAsync(int id)
        {
            var car = _memoryCache.Get<List<Car>>(CacheCarKey).FirstOrDefault(x => x.Id == id);
          
            return Task.FromResult(car);
        }

        public Task<CustomResponseDto<List<GetCarWithAllPropertiesDto>>> GetCarsWithAllPropertiesAsync()
        {
            var cars = _memoryCache.Get<IEnumerable<Car>>(CacheCarKey);
            var carsDto = _mapper.Map<List<GetCarWithAllPropertiesDto>>(cars);
            return Task.FromResult(CustomResponseDto<List<GetCarWithAllPropertiesDto>>.Success(200, carsDto));

        }

        public Task<CustomResponseDto<List<GetCarWithBrandDto>>> GetCarsWithBrandAsync()
        {
            var cars = _memoryCache.Get<IEnumerable<Car>>(CacheCarKey);
            var carsDto = _mapper.Map<List<GetCarWithBrandDto>>(cars);
            return Task.FromResult(CustomResponseDto<List<GetCarWithBrandDto>>.Success(200, carsDto));
        }

        public Task<CustomResponseDto<GetCarWithAllPropertiesDto>> GetCarWithAllPropertiesByIdAsync(int id)
        {
            var car = _memoryCache.Get<List<Car>>(CacheCarKey).FirstOrDefault(x => x.Id == id);

            var carDto = _mapper.Map<GetCarWithAllPropertiesDto>(car);
            return Task.FromResult(CustomResponseDto<GetCarWithAllPropertiesDto>.Success(200, carDto));
        }

        public Task<CustomResponseDto<GetCarWithBrandAndModelDto>> GetCarWithBrandAndModelAsync(int id)
        {

            var car = _memoryCache.Get<List<Car>>(CacheCarKey).FirstOrDefault(x => x.Id == id);

            var carDto = _mapper.Map<GetCarWithBrandAndModelDto>(car);
            return Task.FromResult(CustomResponseDto<GetCarWithBrandAndModelDto>.Success(200, carDto));
        }

        public async Task RemoveAsync(Car entity)
        {
            _repository.Remove(entity);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Car> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
        }

        public async Task UpdateAsync(Car entity)
        {
            _repository.Update(entity);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
        }

        public IQueryable<Car> Where(Expression<Func<Car, bool>> expression)
        {
            return (_memoryCache.Get<List<Car>>(CacheCarKey).Where(expression.Compile())).AsQueryable();
        }


        public async Task CacheAllCarsAsync()
        {
            _memoryCache.Set(CacheCarKey,await _repository.GetCarsWithAllPropertiesAsync());
        }


    }
}
