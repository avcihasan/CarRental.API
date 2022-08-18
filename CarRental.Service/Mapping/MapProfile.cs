using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.DTOs.RentalDTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Brand , BrandDto>().ReverseMap();
            CreateMap<Car, CarWithBrandDto>();
            CreateMap<Car, CarWithAllPropertiesDto>().ReverseMap();
            CreateMap<Color, ColorDto>().ReverseMap(); 
            CreateMap<Engine, EngineDto>().ReverseMap(); 
            CreateMap<Model, ModelDto>().ReverseMap(); 
            CreateMap<ModelYear, ModelYearDto>().ReverseMap(); 
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCarRental, UserCarRentalDto>().ReverseMap();
            CreateMap<UserCarRental, RentalDetailsDto>().ReverseMap();

            CreateMap<Car, CarWithBrandAndModelDto>().ReverseMap();
            



        }
    }
}
