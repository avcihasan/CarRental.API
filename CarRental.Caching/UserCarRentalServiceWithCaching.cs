using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.RentalDTOs;
using CarRental.Core.Models;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using CarRental.Repository.Repositories;
using CarRental.Repository.UnitOfWorks;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Caching
{
    public class UserCarRentalServiceWithCaching : IUserCarRentalService
    {
        private const string CacheCarKey = "carsCache";

        private const string CacheRentalKey = "rentalCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserCarRentalRepository _repository;
        private readonly IUnitOfWork _unitOfwork;
        private readonly ICarRepository _carRepository;

        public UserCarRentalServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IUserCarRentalRepository repository, IUnitOfWork unitOfwork, ICarRepository carRepository)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfwork = unitOfwork;
            _carRepository = carRepository;



            if (!_memoryCache.TryGetValue(CacheRentalKey, out _))
            {
                _memoryCache.Set(CacheRentalKey, _repository.GetAllRentalDetailsAsync().Result);
            }
            
        }

        public async Task<UserCarRental> AddAsync(UserCarRental entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfwork.CommitAsync();
            await CacheRentalAsync();
            return entity;
        }

        public async Task<IEnumerable<UserCarRental>> AddRangeAsync(IEnumerable<UserCarRental> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfwork.CommitAsync();
            await CacheRentalAsync();
            return entities;
        }

        public Task<IEnumerable<UserCarRental>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<UserCarRental>>(CacheRentalKey));

        }

        public async Task<CustomResponseDto<List<RentalDetailsDto>>> GetAllRentalDetailsAsync()
        {
            var rentals = await _repository.GetAllRentalDetailsAsync();
            var rentalsDto = _mapper.Map<List<RentalDetailsDto>>(rentals);
            await _unitOfwork.CommitAsync();

            return CustomResponseDto<List<RentalDetailsDto>>.Success(200, rentalsDto.ToList());
        }

        public Task<UserCarRental> GetByIdAsync(int id)
        {
            var car = _memoryCache.Get<List<UserCarRental>>(CacheRentalKey).FirstOrDefault(x => x.Id == id);

            return Task.FromResult(car);
        }

        public async Task RemoveAsync(UserCarRental entity)
        {
            _repository.Remove(entity);
            await _unitOfwork.CommitAsync();
            await CacheRentalAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<UserCarRental> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfwork.CommitAsync();
            await CacheRentalAsync();
        }

        public async Task<UserCarRental> RentalAsync(UserCarRental userCarRental)
        {
            userCarRental.DateOfIssue = DateTime.Now;
            userCarRental.RollbackDate = DateTime.Now.AddDays(userCarRental.RentalDay);

            await _repository.RentalAsync(userCarRental);
            await _unitOfwork.CommitAsync();
            await CacheAllCarsAsync();
            return userCarRental;
        }

        public async Task UpdateAsync(UserCarRental entity)
        {
            _repository.Update(entity);
            await _unitOfwork.CommitAsync();
            await CacheRentalAsync();
        }

        public IQueryable<UserCarRental> Where(Expression<Func<UserCarRental, bool>> expression)
        {
            return (_memoryCache.Get<List<UserCarRental>>(CacheRentalKey).Where(expression.Compile())).AsQueryable();

        }



        public async Task CacheRentalAsync()
        {
            _memoryCache.Set(CacheRentalKey, await _repository.GetAllRentalDetailsAsync());
        }
        public async Task CacheAllCarsAsync()
        {
            _memoryCache.Set(CacheCarKey, await _carRepository.GetCarsWithAllPropertiesAsync());

        }
    }
}
