using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class CarWithAllPropertiesDto : CarDto 
    {
        public BrandDto Brand { get; set; }
        public ColorDto Color { get; set; }
        public EngineDto Engine { get; set; }
        public ModelDto Model { get; set; }
        public ModelYearDto ModelYear { get; set; }

    }
}
