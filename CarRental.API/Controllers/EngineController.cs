﻿using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
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
            var enginesDto = _mapper.Map<List<EngineDto>>(engines);
            return CreateActionResult(CustomResponseDto<List<EngineDto>>.Success(200, enginesDto.OrderBy(x=>x.Name).ToList()));

        }


        [HttpPost]
        public async Task<IActionResult> Save(EngineDto engineDto)
        {
            var engine = _mapper.Map<Engine>(engineDto);
            await _service.AddAsync(engine);
            return CreateActionResult(CustomResponseDto<EngineDto>.Success(201, engineDto));
        }
    }
}