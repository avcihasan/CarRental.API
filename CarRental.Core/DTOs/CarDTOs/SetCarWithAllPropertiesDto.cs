using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class SetCarWithAllPropertiesDto:SetCarDto
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int EngineId { get; set; }
        public int ColorId { get; set; }
        public int ModelYearId { get; set; }
    }
}
