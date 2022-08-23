using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Models
{
    public class Car:BaseEntity
    {
        public string Photo { get; set; }
        public string Description { get; set; }
        public int DailyPrice { get; set; }
        public string LicensePlate { get; set; }

        [DefaultValue(true)]
        public bool IsRent { get; set; } = true;


        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int ModelYearId { get; set; }
        public ModelYear ModelYear { get; set; }

        
        
    }
}
