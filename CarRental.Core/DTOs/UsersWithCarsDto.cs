using CarRental.Core.DTOs.CarDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs
{
    public class UsersWithCarsDto:UserDto 
    {
        public CarWithAllPropertiesDto Car { get; set; }

    }
}
