using CarRentalPro.Data.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalPro.Data.Models
    {
        public class CarReservation
        {
            [Key]
            public int Id { get; set; } 
        
         [ForeignKey(nameof(UserId))]
            public int UserId { get; set; }

         [ForeignKey(nameof(CarDetailsId))]
          public int CarDetailsId { get; set; }

           public DateTime PickUpDate { get; set; }
            public DateTime DropOfDate { get; set; }
            public string ReservationCode { get; set; }


            public User User { get; set; }         
            public CarDetails CarDetails { get; set; }

            public bool IsConfirmed { get; set; }
         

    }
}

