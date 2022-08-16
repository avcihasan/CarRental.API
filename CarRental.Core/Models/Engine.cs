using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Models
{
    public  class Engine:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

    }
}
