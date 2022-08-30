using CarRental.Core.DTOs.BrandDTOs;
using CarRental.Core.DTOs.ColorDTOs;
using CarRental.Core.DTOs.EngineDTOs;
using CarRental.Core.DTOs.ModelDTOs;
using CarRental.Core.DTOs.ModelYearDTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class SetCarDto :BaseDto
    {
        public string Photo { get; set; }
        public string Description { get; set; }
        public string LicensePlate { get; set; }

        public int DailyPrice { get; set; }


        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int EngineId { get; set; }
        public int ModelId { get; set; }
        public int ModelYearId { get; set; }




    }
}
