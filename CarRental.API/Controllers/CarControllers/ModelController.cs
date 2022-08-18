using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.CarControllers
{

    public class ModelController : CustomBaseController
    {
        private readonly IService<Model> _service;
        private readonly IMapper _mapper;

        public ModelController(IService<Model> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var models = await _service.GetAllAsync();
            var modelsDto = _mapper.Map<List<ModelDto>>(models);
            return CreateActionResult(CustomResponseDto<List<ModelDto>>.Success(200, modelsDto.OrderBy(x => x.Name).ToList()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _service.GetByIdAsync(id);
            var modelDto = _mapper.Map<BrandDto>(model);
            return CreateActionResult(CustomResponseDto<BrandDto>.Success(200, modelDto));

        }
        [HttpPost]
        public async Task<IActionResult> Save(ModelDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _service.AddAsync(model);
            return CreateActionResult(CustomResponseDto<ModelDto>.Success(201, modelDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ModelDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _service.UpdateAsync(model);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(model);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
