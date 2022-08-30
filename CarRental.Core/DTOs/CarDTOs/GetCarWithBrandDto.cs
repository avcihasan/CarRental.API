using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Core.DTOs.BrandDTOs;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class GetCarWithBrandDto : GetCarDto
    {
        public GetBrandDto Brand { get; set; }
    }
}
