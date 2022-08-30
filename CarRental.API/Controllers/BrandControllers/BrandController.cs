using AutoMapper;
using CarRental.Caching;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.BrandControllers
{
    public class BrandController : CustomBaseController
    {
        private readonly IService<Brand> _service;
        private readonly IMapper _mapper;
        private readonly RedisService _redisService;


        public BrandController(IService<Brand> service, IMapper mapper, RedisService redisService)
        {
            _service = service;
            _mapper = mapper;
            _redisService = redisService;

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var db = _redisService.GetDb(0);
            db.StringIncrement("ziyaretci", 1);


            var brands = await _service.GetAllAsync();
            var brandsDto = _mapper.Map<List<GetBrandDto>>(brands);
            return CreateActionResult(CustomResponseDto<List<GetBrandDto>>.Success(200, brandsDto.OrderBy(x => x.Name).ToList()));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            var brandDto = _mapper.Map<GetBrandDto>(brand);
            return CreateActionResult(CustomResponseDto<GetBrandDto>.Success(200, brandDto));

        }



        [HttpPost]
        public async Task<IActionResult> Save(SetBrandDto brandDto)
        {


            var db = _redisService.GetDb(3);
            db.StringSet("BrandName", brandDto.Name);


            var brand = _mapper.Map<Brand>(brandDto);
            await _service.AddAsync(brand);
            return CreateActionResult(CustomResponseDto<SetBrandDto>.Success(201, brandDto));

        }






        [HttpPut]
        public async Task<IActionResult> Update(GetBrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _service.UpdateAsync(brand);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(brand);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
