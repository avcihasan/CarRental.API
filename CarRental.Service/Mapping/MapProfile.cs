using AutoMapper;
using CarRental.Core.DTOs;
using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.DTOs.ColorDTOs;
using CarRental.Core.DTOs.EngineDTOs;
using CarRental.Core.DTOs.ModelDTOs;
using CarRental.Core.DTOs.ModelYearDTOs;
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
            CreateMap<Car, GetCarDto>().ReverseMap();
            CreateMap<SetCarDto, Car>().ReverseMap();

            CreateMap<Brand , GetBrandDto>().ReverseMap(); 
            CreateMap<SetBrandDto, Brand>();

            CreateMap<Car, GetCarWithBrandDto>(); 

            CreateMap<Car, GetCarWithAllPropertiesDto>().ReverseMap();
            CreateMap<SetCarWithAllPropertiesDto, Car>();

            CreateMap<Color, GetColorDto>().ReverseMap(); 
            CreateMap<SetColorDto, Color>();

            CreateMap<Engine, GetEngineDto>().ReverseMap(); 
            CreateMap<SetEngineDto, Engine>();

            CreateMap<Model, GetModelDto>().ReverseMap(); 
            CreateMap<SetModelDto, Model>();

            CreateMap<ModelYear, GetModelYearDto>().ReverseMap() ;
            CreateMap<SetModelYearDto, ModelYear>();

            CreateMap<Car, GetCarWithBrandAndModelDto>() ;

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCarRental, UserCarRentalDto>().ReverseMap();
            CreateMap<UserCarRental, RentalDetailsDto>().ReverseMap();

           
            



        }
    }
}
