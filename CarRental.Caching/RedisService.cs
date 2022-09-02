using CarRental.Core.Repositories;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CarRental.Core.Models;
using CarRental.Core.Services;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using System.Linq.Expressions;
using AutoMapper;
using CarRental.Core.UnitOfWorks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Caching
{
    public class RedisService:ICarService
    {
        private readonly string _redisConnection;
        private ConnectionMultiplexer _redis;
        private readonly IDatabase db;
        private const string CacheCarKey = "carsCache";
        private readonly IMapper _mapper;
        private readonly ICarRepository _repository;
        private readonly IUnitOfWork _unitOfwork;
    



        public RedisService(IConfiguration configuration, ICarRepository repository, IUnitOfWork unitOfwork, IMapper mapper)
        {

            _redisConnection = configuration.GetConnectionString("RedisConnection");
            _redis = ConnectionMultiplexer.Connect(_redisConnection);
            _repository=repository;
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            db = _redis.GetDatabase(10);

            if (db.ListLength(CacheCarKey) == 0)
            {
                var cars = _repository.GetCarsWithAllPropertiesAsync().Result;
                var carsDto = _mapper.Map<List<GetCarWithAllPropertiesDto>>(cars);
                foreach (var item in carsDto)
                {
                    string json = JsonConvert.SerializeObject(item);
                    var database=db.ListRightPushAsync(CacheCarKey, json).Result;
                }
            }
        }
        


        public async Task<CustomResponseDto<List<GetCarWithBrandDto>>> GetCarsWithBrandAsync()
        {
            var cars = await _repository.GetCarsWithBrandAsync();
            var carsDto = _mapper.Map<List<GetCarWithBrandDto>>(cars);
            return CustomResponseDto<List<GetCarWithBrandDto>>.Success(200, carsDto);
        }

        public async Task<CustomResponseDto<GetCarWithBrandAndModelDto>> GetCarWithBrandAndModelAsync(int id)
        {
            var car = await _repository.GetCarWithBrandAndModelAsync(id);
            var carDto = _mapper.Map<GetCarWithBrandAndModelDto>(car);
            return CustomResponseDto<GetCarWithBrandAndModelDto>.Success(200, carDto);
        }

        public async Task<CustomResponseDto<List<GetCarWithAllPropertiesDto>>> GetCarsWithAllPropertiesAsync()
        {
            List<Car> carList = new List<Car>();

            db.ListRange(CacheCarKey).ToList().ForEach(x =>
            {
                Car json = JsonConvert.DeserializeObject<Car>(x);
                
                carList.Add(json);
            });
            var carsDto=_mapper.Map<List<GetCarWithAllPropertiesDto>>(carList);
            return CustomResponseDto<List<GetCarWithAllPropertiesDto>>.Success(200, carsDto);

        }

        public async Task<CustomResponseDto<GetCarWithAllPropertiesDto>> GetCarWithAllPropertiesByIdAsync(int id)
        {
            List<Car> carList = new List<Car>();

            db.ListRange(CacheCarKey).ToList().ForEach(x =>
            {
                Car json = JsonConvert.DeserializeObject<Car>(x);

                carList.Add(json);
            });
            var carDto = _mapper.Map<GetCarWithAllPropertiesDto>(carList.Where(x=>x.Id==id).FirstOrDefault());
            if (carDto==null)
            {
                return CustomResponseDto<GetCarWithAllPropertiesDto>.Fail(404, "Araba Bulunamadı!");

            }
            return CustomResponseDto<GetCarWithAllPropertiesDto>.Success(200, carDto);
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {


            return await _repository.GetAll().ToListAsync();

        }

        public IQueryable<Car> Where(Expression<Func<Car, bool>> expression)
        {
            return _repository.Where(expression);

        }

        public async Task<Car> AddAsync(Car car)
        {
            await _repository.AddAsync(car);
            await _unitOfwork.CommitAsync();

            await CacheAllCarsAsync();

            return car;
        }

        public async Task<IEnumerable<Car>> AddRangeAsync(IEnumerable<Car> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
            return entities;
        }

        public async Task UpdateAsync(Car entity)
        {
            _repository.Update(entity);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
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

        public async Task CacheAllCarsAsync()
        {

            var cars = await _repository.GetCarsWithAllPropertiesAsync();
            
            var carsDto = _mapper.Map<List<GetCarWithAllPropertiesDto>>(cars);

            db.KeyDelete(CacheCarKey);

            foreach (var item in carsDto)
            {
                string json = JsonConvert.SerializeObject(item);
                await db.ListRightPushAsync(CacheCarKey, json);
            }
        }



    }
}
