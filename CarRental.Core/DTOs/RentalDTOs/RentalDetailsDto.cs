using CarRental.Core.DTOs.CarDTOs;
using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.RentalDTOs
{
    public class RentalDetailsDto
    {

        public User User { get; set; }

        public CarWithAllPropertiesDto Car { get; set; }
   
        public bool isDelivered { get; set; } = false;
        public DateTime DateOfIssue { get; set; } 
        public DateTime? RollbackDate { get; set; }
        public int RentalDay { get; set; }
        public int DailyPrice { get; set; }
        public int TotalPrice { get; set; }
    }
}
