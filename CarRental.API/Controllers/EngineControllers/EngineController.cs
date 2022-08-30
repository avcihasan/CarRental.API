using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.EngineDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers.EngineControllers
{

    public class EngineController : CustomBaseController
    {
        private readonly IService<Engine> _service;
        private readonly IMapper _mapper;

        public EngineController(IService<Engine> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var engines = await _service.GetAllAsync();
            var enginesDto = _mapper.Map<List<GetEngineDto>>(engines);
            return CreateActionResult(CustomResponseDto<List<GetEngineDto>>.Success(200, enginesDto.OrderBy(x => x.Name).ToList()));

        }


        [HttpPost]
        public async Task<IActionResult> Save(SetEngineDto engineDto)
        {
            var engine = _mapper.Map<Engine>(engineDto);
            await _service.AddAsync(engine);
            return CreateActionResult(CustomResponseDto<SetEngineDto>.Success(201, engineDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GetEngineDto engineDto)
        {
            var engine = _mapper.Map<Engine>(engineDto);
            await _service.UpdateAsync(engine);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var engine = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(engine);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
