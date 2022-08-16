using AutoMapper;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CarRental.Core.Models;
using Color = CarRental.Core.Models.Color;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.DTOs;

namespace CarRental.API.Controllers.CarControllers
{

    public class ColorController : CustomBaseController
    {

        private readonly IService<Color> _service;
        private readonly IMapper _mapper;

        public ColorController(IService<Color> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var colors = await _service.GetAllAsync();
            var colorsDto = _mapper.Map<List<ColorDto>>(colors);
            return CreateActionResult(CustomResponseDto<List<ColorDto>>.Success(200, colorsDto.OrderBy(x => x.Name).ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ColorDto colorDto)
        {
            var color = _mapper.Map<Color>(colorDto);
            await _service.AddAsync(color);
            return CreateActionResult(CustomResponseDto<ColorDto>.Success(201, colorDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ColorDto colorDto)
        {
            var color = _mapper.Map<Color>(colorDto);
            await _service.UpdateAsync(color);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var color = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(color);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
