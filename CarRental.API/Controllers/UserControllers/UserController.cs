using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.UserControllers
{
   
    public class UserController : CustomBaseController
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _service.GetAllAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, usersDto.OrderBy(x =>x.FirstName).ToList()));
        }

       [HttpPost]
       public async Task<IActionResult> Save(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _service.AddAsync(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _service.UpdateAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsersWithCars()
        {
            return CreateActionResult(await _service.GetUsersWithCarsAsync());
        }
    }
}
