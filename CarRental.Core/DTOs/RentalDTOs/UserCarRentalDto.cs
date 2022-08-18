using CarRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.DTOs.RentalDTOs
{
    public class UserCarRentalDto
    {
        public int UserId { get; set; }

        public int CarId { get; set; }

        public int RentalDay { get; set; }
        public int DailyPrice { get; set; }
        public int TotalPrice { get; set; }

     

    }
}
