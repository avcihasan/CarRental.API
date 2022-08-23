using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Models
{
    public class ModelYear:BaseEntity
    {
        public int ModelYears { get; set; }

        public List<Car> Cars { get; set; }

    }
}
