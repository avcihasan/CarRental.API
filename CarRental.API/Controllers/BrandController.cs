using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    public class BrandController : CustomBaseController
    {
        private readonly IService<Brand> _service;
        private readonly IMapper _mapper;

        public BrandController(IService<Brand> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var brands = await _service.GetAllAsync();
            var brandsDto = _mapper.Map<List<BrandDto>>(brands);
            return CreateActionResult(CustomResponseDto<List<BrandDto>>.Success(200, brandsDto.OrderBy(x => x.Name).ToList()));

        }

        [HttpPost]
        public async Task<IActionResult> Save(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _service.AddAsync(brand);
            return CreateActionResult(CustomResponseDto<BrandDto>.Success(201, brandDto));

        }
    }
}
