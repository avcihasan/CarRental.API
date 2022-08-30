using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.DTOs.ColorDTOs;
using CarRental.Core.DTOs.EngineDTOs;
using CarRental.Core.DTOs.ModelDTOs;
using CarRental.Core.DTOs.ModelYearDTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class GetCarDto:BaseDto
    {
        public string Photo { get; set; }
        public string Description { get; set; }
        public int DailyPrice { get; set; }
        public string LicensePlate { get; set; }

        public bool IsRent { get; set; }

        public GetBrandDto Brand { get; set; }
        public GetColorDto Color { get; set; }
        public GetEngineDto Engine { get; set; }
        public GetModelDto Model { get; set; }
        public GetModelYearDto ModelYear { get; set; }




    }
}
