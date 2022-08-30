using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.DTOs.ModelDTOs;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class GetCarWithBrandAndModelDto:GetCarDto
    {
        public GetBrandDto Brand { get; set; }
        public GetModelDto Model { get; set; }

    }
}
