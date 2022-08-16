using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
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
            var modelYearsDto = _mapper.Map<List<ModelYearDto>>(modelYears);
            return CreateActionResult(CustomResponseDto<List<ModelYearDto>>.Success(200, modelYearsDto.OrderBy(x => x.Year).ToList()));
        }

        [HttpPost]  
        public async Task<IActionResult> Save(ModelYearDto modelYearDto)
        {
            var modelYear = _mapper.Map<ModelYear>(modelYearDto);
            await _service.AddAsync(modelYear);
            return CreateActionResult(CustomResponseDto<ModelYearDto>.Success(201, modelYearDto));
        }
    }
}
