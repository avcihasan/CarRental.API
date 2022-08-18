using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.RentalDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.CarControllers
{

    public class UserCarRentalController : CustomBaseController
    {
        private readonly IUserCarRentalService _service;
        private readonly IMapper _mapper;

        public UserCarRentalController(IUserCarRentalService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserCarRentalDto userCarRentalDto)
        {
            var userCarRental = _mapper.Map<UserCarRental>(userCarRentalDto);
            await _service.RentalAsync(userCarRental);
            return CreateActionResult(CustomResponseDto<UserCarRentalDto>.Success(200, userCarRentalDto));

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRentalDetails()
        {
            return CreateActionResult(await _service.GetAllRentalDetailsAsync());

        }
    }
}
