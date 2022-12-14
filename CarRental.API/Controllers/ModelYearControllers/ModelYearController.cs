using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.ModelYearDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.ModelYearControllers
{

    public class ModelYearController : CustomBaseController
    {
        private readonly IService<ModelYear> _service;
        private readonly IMapper _mapper;



        public ModelYearController(IService<ModelYear> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var modelYears = await _service.GetAllAsync();
            var modelYearsDto = _mapper.Map<List<GetModelYearDto>>(modelYears);
            return CreateActionResult(CustomResponseDto<List<GetModelYearDto>>.Success(200, modelYearsDto.OrderBy(x => x.ModelYears).ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SetModelYearDto modelYearDto)
        {
            var modelYear = _mapper.Map<ModelYear>(modelYearDto);
            await _service.AddAsync(modelYear);
            return CreateActionResult(CustomResponseDto<SetModelYearDto>.Success(201, modelYearDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GetModelYearDto modelYearDto)
        {
            var modelYear = _mapper.Map<ModelYear>(modelYearDto);
            await _service.UpdateAsync(modelYear);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var modelYear = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(modelYear);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
