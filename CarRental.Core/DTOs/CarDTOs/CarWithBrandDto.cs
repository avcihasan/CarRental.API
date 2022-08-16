using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.CarDTOs
{
    public class CarWithBrandDto : CarDto
    {
        public BrandDto Brand { get; set; }
    }
}
