using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Models
{
    public class UserCarRental:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int RentalDay { get; set; }


        public int DailyPrice { get; set; }
        public int TotalPrice { get; set; }
        public bool isDelivered { get; set; }=false;
        public DateTime DateOfIssue { get; set; }=DateTime.Now;
        public DateTime? RollbackDate { get; set; }

    }
}
