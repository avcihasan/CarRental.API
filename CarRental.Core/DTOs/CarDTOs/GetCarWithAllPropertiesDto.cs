using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.DTOs.ColorDTOs;
using CarRental.Core.DTOs.EngineDTOs;
using CarRental.Core.DTOs.ModelDTOs;
using CarRental.Core.DTOs.ModelYearDTOs;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class GetCarWithAllPropertiesDto : GetCarDto 
    {
        public GetBrandDto Brand { get; set; }
        public GetColorDto Color { get; set; }
        public GetEngineDto Engine { get; set; }
        public GetModelDto Model { get; set; }
        public GetModelYearDto ModelYear { get; set; }

    }
}
