using AutoMapper;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CarRental.Core.Models;
using Color = CarRental.Core.Models.Color;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.ColorDTOs;

namespace CarRental.API.Controllers.ColorControllers
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
            var colorsDto = _mapper.Map<List<GetColorDto>>(colors);
            return CreateActionResult(CustomResponseDto<List<GetColorDto>>.Success(200, colorsDto.OrderBy(x => x.Name).ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SetColorDto colorDto)
        {
            var color = _mapper.Map<Color>(colorDto);
            await _service.AddAsync(color);
            return CreateActionResult(CustomResponseDto<SetColorDto>.Success(201, colorDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GetColorDto colorDto)
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
