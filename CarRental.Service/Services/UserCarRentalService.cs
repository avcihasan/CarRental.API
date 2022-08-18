using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.DTOs.RentalDTOs;
using CarRental.Core.Models;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using CarRental.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Service.Services
{
    public class UserCarRentalService : Service<UserCarRental>, IUserCarRentalService
    {
        private readonly IUserCarRentalRepository _userCarRentalRepository;
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCarRentalService(IGenericRepository<UserCarRental> repository, IUnitOfWork unitOfWork, IUserCarRentalRepository userCarRentalRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userCarRentalRepository = userCarRentalRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<RentalDetailsDto>>> GetAllRentalDetailsAsync()
        {
            var rentals = await _userCarRentalRepository.GetAllRentalDetailsAsync();
            var rentalsDto = _mapper.Map<List<RentalDetailsDto>>(rentals);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<List<RentalDetailsDto>>.Success(200, rentalsDto.ToList());
        }

        public async Task<UserCarRental> RentalAsync(UserCarRental userCarRental)
        {
            await _userCarRentalRepository.RentalAsync(userCarRental);
            await _unitOfWork.CommitAsync();
            return userCarRental;
        }
    }
}
